properties { 
  $base_dir  = resolve-path .
  $lib_dir = "$base_dir\Lib"
  $build_dir = "$base_dir\Build" 
  $buildartifacts_dir = "$build_dir\" 
  $sln_file = "$base_dir\Source\xUnit.BDDExtensions.sln" 
  $version = "1.0.2.12"
  $tools_dir = "$base_dir\Tools"
  $release_dir = "$base_dir\Release"
} 

task default -depends Release

task Clean { 
  remove-item -force -recurse $buildartifacts_dir -ErrorAction SilentlyContinue 
  remove-item -force -recurse $release_dir -ErrorAction SilentlyContinue 
} 

task Init -depends Clean { 
    . .\psake_ext.ps1
    
    Generate-Assembly-Info `
        -file "$base_dir\Source\GlobalAssemblyInfo.cs" `
        -title "xUnit.BDDExtensions $version" `
        -description "A context specification framework based on xUnit.net" `
        -product "xUnit.BDDExtensions $version" `
        -version $version `
        -clsCompliant "false" `
        -copyright "Copyright © Björn Rochel 2008-2010"
        
    new-item $release_dir -itemType directory 
    new-item $buildartifacts_dir -itemType directory 
} 

task Compile -depends Init { 
  exec { msbuild "/p:OutDir=$buildartifacts_dir" "/p:Platform=Any CPU" "$sln_file" }
} 

task Test -depends Compile {
  exec { & $tools_dir\xUnit\xunit.console.exe $build_dir\xUnit.BDDExtensions.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.exe $build_dir\xUnit.BDDExtensions.Reporting.Specs.dll }
}

task Merge {
    $old = pwd
    cd $build_dir
    
    Remove-Item xUnit.BDDExtensions.Partial.dll -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\xUnit.BDDExtensions.dll xUnit.BDDExtensions.Partial.dll
    
    exec {
    
     & $tools_dir\ILMerge\ILMerge.exe xUnit.BDDExtensions.Partial.dll `
        xUnit.BDDExtensions.Assertions.dll `
        xUnit.BDDExtensions.Mocking.RhinoMocks.dll `
        StructureMap.dll `
        StructureMap.AutoMocking.dll `        Rhino.Mocks.dll `
        /out:xUnit.BDDExtensions.dll `
        "/internalize:$base_dir\ILMergeExcludes.txt" `
        /t:library ``
    }
    
    Remove-Item ReportGenerator.Partial.exe -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\ReportGenerator.exe ReportGenerator.Partial.exe
    
    exec { 
    
      & $tools_dir\ILMerge\ILMerge.exe ReportGenerator.Partial.exe `
        StructureMap.dll `
        NVelocity.dll `
        /out:ReportGenerator.exe `
        /t:winexe ``
    }
     
    cd $old
}

task Docu -depends Test, Merge {
   exec { & $build_dir\ReportGenerator.exe /generator:HTML /assembly:'$build_dir\xUnit.BDDExtensions.Specs.dll' /assembly:'$build_dir\xUnit.BDDExtensions.Reporting.Specs.dll' }
}

task Release -depends Test, Merge {
    
    exec {
    
      & $tools_dir\Zip\zip.exe -9 -A -j `
        $release_dir\xUnit.BDDExtensions.$version.zip `
        $build_dir\xUnit.BDDExtensions.dll `
        $build_dir\ReportGenerator.exe `
        $build_dir\xunit.dll `
        License.txt `
        acknowledgements.txt    
    }
}
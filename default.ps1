properties { 
  $base_dir  = resolve-path .
  $lib_dir = "$base_dir\Lib"
  $build_dir = "$base_dir\Build" 
  $buildartifacts_dir = "$build_dir\" 
  $sln_file = "$base_dir\Source\xUnit.BDDExtensions.sln" 
  $version = "1.5.0.1"
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
        -title "xUnit.BDDExtensions" `
        -description "A context specification framework based on xUnit.net" `
        -product "xUnit.BDDExtensions $version (alpha)" `
        -version $version `
        -clsCompliant "false" `
        -copyright "Copyright © xUnit.NET BDDExtension Team 2008-2010"
        
    new-item $release_dir -itemType directory 
    new-item $buildartifacts_dir -itemType directory 
} 

task Compile -depends Init { 
  exec { msbuild /t:rebuild "/p:OutDir=$buildartifacts_dir" "/p:Configuration=Release" "/p:Platform=Any CPU" "$sln_file" }
} 

task Test -depends Compile {
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\xUnit.BDDExtensions.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\xUnit.BDDExtensions.Faking.Moq.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\xUnit.BDDExtensions.Reporting.Specs.dll }
  exec { & $tools_dir\xUnit\xunit.console.clr4.exe $build_dir\xUnit.BDDExtensions.MVC.Specs.dll }
}

task Merge {
    . .\psake_ext.ps1
    $framework_dir = Get-FrameworkDirectory

    $old = pwd
    cd $build_dir
    
    Remove-Item xUnit.BDDExtensions.Partial.dll -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\xUnit.BDDExtensions.dll xUnit.BDDExtensions.Partial.dll

    exec {
        
     & $tools_dir\ILMerge\ILMerge.exe xUnit.BDDExtensions.Partial.dll `
        xUnit.BDDExtensions.Assertions.dll `
        StructureMap.dll `
        StructureMap.AutoMocking.dll `
        Rhino.Mocks.dll `
        /out:xUnit.BDDExtensions.dll `
        "/internalize:$base_dir\ILMergeExcludes.txt" `
        /t:library  `
        /targetplatform:"v4,$framework_dir"
    }

    Remove-Item xUnit.BDDExtensions.MVC.Partial.dll -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\xUnit.BDDExtensions.MVC.dll xUnit.BDDExtensions.MVC.Partial.dll
        
	exec {
    
     & $tools_dir\ILMerge\ILMerge.exe xUnit.BDDExtensions.MVC.Partial.dll `
        Microsoft.Web.MVC.dll `
        /out:xUnit.BDDExtensions.MVC.dll `
        "/internalize:$base_dir\ILMergeMVCExcludes.txt" `
        /t:library `
        /targetplatform:"v4,$framework_dir"
    }

    Remove-Item ReportGenerator.Partial.exe -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\ReportGenerator.exe ReportGenerator.Partial.exe
    
    exec { 
    
      & $tools_dir\ILMerge\ILMerge.exe ReportGenerator.Partial.exe `
        StructureMap.dll `
        NVelocity.dll `
        /out:ReportGenerator.exe `
        /t:winexe `
        /targetplatform:"v4,$framework_dir"
    }
     
    cd $old
}

task Docu -depends Test, Merge {
   exec { & $build_dir\ReportGenerator.exe /generator:HTML /assembly:'$build_dir\xUnit.BDDExtensions.Specs.dll' /assembly:'$build_dir\xUnit.BDDExtensions.Reporting.Specs.dll' /assembly:'$build_dir\xUnit.BDDExtensions.MVC.Specs.dll' }
}

task Release -depends Test, Merge, Docu {
    
    exec {
    
      & $tools_dir\Zip\zip.exe -9 -A -j `
        $release_dir\xUnit.BDDExtensions.$version.zip `
        $build_dir\xUnit.BDDExtensions.dll `
        $build_dir\xUnit.BDDExtensions.MVC.dll `
        $build_dir\ReportGenerator.exe `
        $build_dir\xunit.dll `
        License.txt `
        acknowledgements.txt    
    }
}

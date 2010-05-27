properties { 
  $base_dir  = resolve-path .
  $lib_dir = "$base_dir\Lib"
  $build_dir = "$base_dir\Build" 
  $buildartifacts_dir = "$build_dir\" 
  $sln_file = "$base_dir\Source\xUnit.BDDExtensions.sln" 
  $version = "1.0.2.9"
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
  exec { msbuild "/p:OutDir=""$buildartifacts_dir "" $sln_file" }
} 

task Test -depends Compile {
  $old = pwd
  cd $build_dir
  & $tools_dir\xUnit\xunit.console.exe $build_dir\xUnit.BDDExtensions.Specs.dll
  & $tools_dir\xUnit\xunit.console.exe $build_dir\xUnit.BDDExtensions.Reporting.Specs.dll
  cd $old        
}

task Merge {
    $old = pwd
    cd $build_dir
    
    Remove-Item xUnit.BDDExtensions.Partial.dll -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\xUnit.BDDExtensions.dll xUnit.BDDExtensions.Partial.dll
    
    & $tools_dir\ILMerge\ILMerge.exe xUnit.BDDExtensions.Partial.dll `
        StructureMap.dll `
        StructureMap.AutoMocking.dll `        Rhino.Mocks.dll `
        /out:xUnit.BDDExtensions.dll `
        /t:library ``
        "/internalize:'$base_dir\ILMergeExcludes.txt'"
           
    if ($lastExitCode -ne 0)
    {
        throw "Error: Failed to merge xUnit.BDDExtensions!"
    }
    
    
    Remove-Item ReportGenerator.Partial.exe -ErrorAction SilentlyContinue 
    Rename-Item $build_dir\ReportGenerator.exe ReportGenerator.Partial.exe
    
    & $tools_dir\ILMerge\ILMerge.exe ReportGenerator.Partial.exe `
        StructureMap.dll `
        NVelocity.dll `
        /out:ReportGenerator.exe `
        /t:winexe ``
           
    if ($lastExitCode -ne 0)
    {
        throw "Error: Failed to merge ReportGenerator!"
    }
     
    cd $old
}

task Docu -depends Test, Merge {
   & $build_dir\ReportGenerator.exe /generator:HTML /assembly:'$build_dir\xUnit.BDDExtensions.Specs.dll' /assembly:'$build_dir\xUnit.BDDExtensions.Reporting.Specs.dll'  
}

task Release -depends Test, Merge {
    
    & $tools_dir\Zip\zip.exe -9 -A -j `
        $release_dir\xUnit.BDDExtensions.$version.zip `
        $build_dir\xUnit.BDDExtensions.dll `
        $build_dir\ReportGenerator.exe `
        $build_dir\xunit.dll `
        License.txt `
        acknowledgements.txt
        
    if ($lastExitCode -ne 0) 
    {
        throw "Error: Failed to execute ZIP command"
    }
}
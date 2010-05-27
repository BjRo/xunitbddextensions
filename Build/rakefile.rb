require 'rake/clean'
require 'rake'
require 'rake/tasklib'
require 'erb'
require 'rake/packagetask'
require File.dirname(__FILE__) + "/utils"

CLEAN.include(FileList[File.join(Directory[:build_dir], '**/*.*')])
CLEAN.include(FileList[File.join(Directory[:deploy_dir], '**/*.*')])
CLEAN.exclude('**/*.zip')

ENV['build_number'] = "1.0.2.9"

desc "Updates the AssemblyInfo file for the project"
task :assembly_info do
  
  task_begin "Updating the global assembly info to #{ENV['build_number']}"
    
  assembly_info("GlobalAssemblyInfo.cs".in(:source_dir), 
  {
    "AssemblyTitle" => "xUnit.BDDExtensions" ,
    "AssemblyDescription" => "A context specification framework based on xUnit.net" ,
    "AssemblyProduct" => "xUnit.BDDExtensions",
    "AssemblyCopyright" => "Copyright © Björn Rochel 2008-2010",
    "AssemblyVersion" => ENV['build_number'],
    "AssemblyFileVersion" => ENV['build_number']
  })
  
end

desc "Compiles the sources"
task :build do

  task_begin "Compiling the solution"
  msbuild(:out_dir => Directory[:build_dir] , :solution => "xUnit.BDDExtensions.sln".in(:source_dir))  
  
end

desc "Runs all the tests"
task :test => :build do
  
  task_begin "Running all xUnit tests"
  xunit  all_assemblies_containing_specs
  
end

desc "Merging assemblies to xUnit.BDDExtensions.dll & ReportGenerator.exe"
task :merge do
  
  task_begin "Merging assemblies to xUnit.BDDExtensions.dll & ReportGenerator.exe"
  
  mkdir Directory[:deploy_dir] unless File.exists? Directory[:deploy_dir]
  cp "xunit.dll".in(:build_dir), "xunit.dll".in(:deploy_dir)

  puts "Merging xUnit.BDDExtensions.dll . . ."
  
  ilmerge \
      :output_name => "xUnit.BDDExtensions.dll".in(:deploy_dir),  \
      :merge_assemblies => ["xUnit.BDDExtensions.dll", "StructureMap.dll", "StructureMap.AutoMocking.dll",  "Rhino.Mocks.dll"] ,\
      :assembly_type => :library, \
      :internalize_exclude_file => File.expand_path("ILMergeExcludes.txt")
  
  puts "ReportGenerator.exe . . ."
 
  ilmerge \
      :output_name => "ReportGenerator.exe".in(:deploy_dir),  \
      :merge_assemblies =>  ["ReportGenerator.exe", "StructureMap.dll", "NVelocity.dll"],\
      :assembly_type => :exe

end

desc "Generates the specification report for the created assemblies"
task :report => :merge do
  
  task_begin "Generating the specification report for the created assemblies"
  spec_report(:generator => "Html",  :assemblies => all_assemblies_containing_specs)
  
end

task :deploy => [:merge, :report] 

task :default => [:clean,  :assembly_info, :build, :test, :deploy]

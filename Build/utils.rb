# Defintion of all relevant directories
Directory = {
  :build_dir => File.expand_path('../Bin'), 
  :source_dir => File.expand_path('../Source'), 
  :externals_dir => File.expand_path('../Externals'), 
  :deploy_dir => File.expand_path('../Deploy') 
}

class String
  
  # Escape helper
  def escape()
    "\"#{self.to_s}\""
  end
  
  # Expands the string to an absolute file name. The string is concatenated with the directory specified via dir_symbol
  def in(dir_symbol)
    File.join(Directory[dir_symbol], self.to_s)
  end
end

# Gets a collection of all spec assemblies in the build folder
def all_assemblies_containing_specs()
  file_mask = "*Specs.dll".in(:build_dir)
  FileList[file_mask]
end

def task_begin(text)
  puts ""
  puts ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"
  puts ">>"
  puts ">> #{text}"
  puts ">>"
  puts ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>"
  puts  ""
end

# Generates a new assembly info file
def  assembly_info(file , attributes)

    raise "File must be specified" if file.nil?
    raise "No assembly attributes where specified" if attributes.nil? or attributes.empty?
    
    template = %q{
    using System;
    using System.Reflection;
    using System.Runtime.InteropServices;
    
  <% attributes.each {|key, value| %>
    [assembly: <%= key %>("<%= value %>")] <% } %>
    
   }.gsub(/^\w+/, '')
                  
    erb = ERB.new(template, 0, "%<>")
    
    puts "Starting to create AssemblyInfo file at #{file}"

    File.open(file, 'w') do |file|
        file.puts erb.result(binding) 
      end
      
end

########################## TOOL INTEGRATION ########################

def msbuild (properties)
  framework_dir = File.join(ENV['windir'].dup, 'Microsoft.NET', 'Framework', 'v3.5') 
  msbuild = File.join(framework_dir, 'msbuild.exe')
  sh "#{msbuild} /property:Platform=\"Any CPU\";WarningLevel=4;OutDir=#{properties[:out_dir].escape}/ #{properties[:solution].escape}" 
end

def xunit(assemblies)
  xunit_console = tool(:xUnit)
  assemblies.each do |assembly|
    sh "#{xunit_console.escape} #{assembly.escape} /html #{assembly.escape + ".xunit.html"}"
  end
end

def spec_report(properties)
   report_tool = "ReportGenerator.exe".in(:deploy_dir)
   generator = properties[:generator].nil? ? "Html" : properties[:generator]
   args = "#{report_tool} /generator:#{generator}"
   
   properties[:assemblies].each do |assembly|
      args << " /assembly:#{assembly.escape}"
    end
    
   unless properties[:path].nil?
     args << " /path:#{properties[:path].escape}"
   end
   
   sh args
end

def ilmerge(arguments)
  
  raise "Must specify an output name" if arguments[:output_name].nil?
  raise "Must specify a set of merge assemblies" if arguments[:merge_assemblies].nil?
  raise "Must specify at least a single assembly to merge" if arguments[:merge_assemblies].empty?
  
  arguments[:assembly_type] = :library if arguments[:assembly_type].nil?
  
  arguments[:merge_assemblies].map! do |assembly|
    assembly.in(:build_dir).escape
  end
  
  types = { :library => "/t:library", :exe => "/t:exe", :win_exe => "/t:winexe" }
  
  ilmerge_call = "#{tool(:ILMerge).escape} #{types.fetch(arguments[:assembly_type])}"
  ilmerge_call << " #{"/internalize:" + arguments[:internalize_exclude_file].escape}" unless arguments[:internalize_exclude_file].nil?
  ilmerge_call << " /allowDup /out:#{arguments[:output_name].escape} #{arguments[:merge_assemblies].join(" ")}"
  
  sh  ilmerge_call
end

def tool(tool_symbol)
  case tool_symbol
    when :xUnit 
      tool = "xUnit/xunit.console.exe"
    when :ILMerge 
      tool = "ILMerge/ILMerge.exe"
    when :zip
      tool =  "7-Zip/7z.exe"
  end
  
  tool.in(:externals_dir)
end
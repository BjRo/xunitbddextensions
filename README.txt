
A little framework build on top of xUnit which enables a context/specification style of testing with xUnit.net.

Besides an emphasis on a "test case class per fixture" organization scheme for tests and AAA (Arrange, Act and Assert) style of writing tests, this framework also provides Rhino.Mocks integration out of the box.

Building xunitbddextensions requires PowerShell. After installing PowerShell run the following command in the command line to allow PowerShell to run arbitrary PowerShell scripts like the xunitbddextensions build script:

	C:\> powershell Set-ExecutionPolicy Unrestricted
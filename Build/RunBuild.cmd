@ECHO OFF
cls 
rake.bat %*
IF %ERRORLEVEL%==9009 GOTO:rake_failed
GOTO:EOF

:rake_failed
type Ruby_Not_Installed.txt
GOTO :EOF
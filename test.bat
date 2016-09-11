MSBuild.exe fold-blank.sln /p:Configuration=Debug /p:Platform="Any CPU"
if errorlevel 1 goto :eof
echo begin >test.txt
echo >test.txt
echo >test.txt
echo end >test.txt
bin\Debug\fold-blank *.txt
type test.txt

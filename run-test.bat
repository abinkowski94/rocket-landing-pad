del "tests\coverage.json" >nul 2>&1

dotnet test /p:CollectCoverage=true /p:CoverletOutput=../coverage.json /p:MergeWith=../coverage.json

pause
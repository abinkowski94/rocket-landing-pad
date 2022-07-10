rm -f tests/coverage.json
dotnet test /p:CollectCoverage=true /p:CoverletOutput=../coverage.json /p:MergeWith=../coverage.json
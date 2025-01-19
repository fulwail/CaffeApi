@echo off
set /p "name=Enter MigrationName: "
dotnet ef migrations --startup-project ../Caffe.Api/ add %name% --verbose  --context CaffeContext
 set /p "name=Migration Done"
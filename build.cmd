@ECHO OFF

echo ---- dotnet tool restore ----------------------------------------------------------------
dotnet tool restore
echo ---- dotnet paket restore ---------------------------------------------------------------- 
dotnet paket restore
echo ---- dotnet run --project .\Pinicola.Build.Run\Pinicola.Build.Run.fsproj ----------------- 
dotnet run --project .\Pinicola.Build.Run\Pinicola.Build.Run.fsproj --configuration Debug
echo ------------------------------------------------------------------------------------------


@ECHO OFF

echo ---- dotnet tool restore -------------------------------------------- 
dotnet tool restore
echo ---- dotnet paket restore ------------------------------------------- 
dotnet paket restore
echo ---- dotnet run --project .\.build\build\build.fsproj --------------- 
dotnet run --project .\.build\build\build.fsproj
echo --------------------------------------------------------------------- 

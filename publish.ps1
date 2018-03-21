dotnet publish developerService --framework netcoreapp2.0 --runtime linux-x64 --self-contained -c Release
dotnet publish messageSender --framework netcoreapp2.0 --runtime linux-x64 --self-contained -c Release
dotnet publish web --framework netcoreapp2.0 --runtime linux-x64 --self-contained -c Release
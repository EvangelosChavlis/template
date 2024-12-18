dotnet pack -o ./nupkg

dotnet nuget push ./nupkg/CleanArch.Template.NetReactTS.1.0.1.nupkg --api-key <> --source https://api.nuget.org/v3/index.json

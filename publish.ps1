param(
    [string]$version,
    [string]$github,
    [string]$nuget
)

dotnet nuget push ./src/bin/Release/"yadelib.$version.nupkg" --source https://nuget.pkg.github.com/mdddev/index.json --api-key $github
dotnet nuget push ./src/bin/Release/"yadelib.$version.nupkg" --source https://api.nuget.org/v3/index.json --api-key $nuget
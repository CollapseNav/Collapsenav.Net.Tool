rm -r .\pack
dotnet pack -o pack --include-source -c Release
dotnet nuget push pack\*.symbols* -k

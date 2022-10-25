param(
    [Switch]$CleanAll,
    $WorkspaceId = "62ffe4360e68ad2eb21f74a7",
    $ProjectId = "630cb1424a05b20faf676617",
    $ClockifyKey = "Mzg4YzZmOWQtNTAxYy00MmEwLWI0NjEtNzAzZjg4MzRhMzVh"
)

$ErrorActionPreference = "STOP"

if($CleanAll) {
    Get-ChildItem $PWD -Exclude "setup.ps1" | Remove-Item -Recurse -Force
}

# Cleanup
$packageCachePath = (Join-Path $PWD ".sitecore" "package-cache")
If (Test-Path $packageCachePath) {
    Remove-Item $packageCachePath -Recurse -Force
}

# Create local nuget.config for scoping
set-content -path (Join-Path $PWD "nuget.config") -Value '<?xml version="1.0" encoding="utf-8"?><configuration><packageSources><clear /></packageSources></configuration>'

# Install Sitecore.CLI
dotnet new tool-manifest --force
dotnet nuget add source https://sitecore.myget.org/F/sc-packages/api/v3/index.json -n Sitecore 
dotnet tool install Sitecore.CLI 

# Sitecore CLI init
dotnet sitecore init --verbose
dotnet sitecore plugin remove -n Sitecore.DevEx.Extensibility.Database
dotnet sitecore plugin list

dotnet nuget remove source Sitecore

# Install extension
Write-Host "`nInstall custom extension..." -ForegroundColor Green
dotnet nuget add source (Join-Path $PSScriptRoot "..\out\nuget" -Resolve) -n LocalExt
dotnet sitecore plugin add -n SitecoreCli.DevEx.Extensions.TimeReg.Clockify --verbose --trace
dotnet sitecore plugin list

# Call methods
Write-Host "`nAvailable commands:" -ForegroundColor Green
dotnet sitecore timelog init --help
Write-Host "`nCall commands..." -ForegroundColor Green
dotnet sitecore timelog init -w $WorkspaceId -p $ProjectId -k $ClockifyKey
dotnet sitecore timelog tasks -n "solution setup"

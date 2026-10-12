# Starts the Debug build elevated so CPU, disk and memory temperatures can be read.
# Windows asks for permission (UAC). Example: .\scripts\dev-admin.ps1 -AppArgs '--demo'
param([string[]]$AppArgs = @())

$root = Split-Path $PSScriptRoot -Parent
$exe = Join-Path $root 'src\FrameHeat\bin\Debug\net9.0-windows\FrameHeat.exe'

if (-not (Test-Path $exe)) {
    dotnet build (Join-Path $root 'FrameHeat.sln') -c Debug
}

if ($AppArgs.Count -gt 0) {
    Start-Process -FilePath $exe -ArgumentList $AppArgs -Verb RunAs
} else {
    Start-Process -FilePath $exe -Verb RunAs
}

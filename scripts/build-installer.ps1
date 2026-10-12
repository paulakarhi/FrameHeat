# Builds dist-installer\FrameHeat-Setup-<version>.exe from the current source.
#
# Needs the .NET 9 SDK and Inno Setup 6 (winget install JRSoftware.InnoSetup). The PawnIO driver
# setup that the installer bundles is downloaded on first use and checked against a pinned hash.
param([switch]$SkipPublish)

$root = Split-Path $PSScriptRoot -Parent

$driverUrl = 'https://github.com/namazso/PawnIO.Setup/releases/download/2.2.0/PawnIO_setup.exe'
$driverHash = '1F519A22E47187F70A1379A48CA604981C4FCF694F4E65B734AAA74A9FBA3032'
$driver = Join-Path $root 'installer\redist\PawnIO_setup.exe'

function Find-Compiler {
    $candidates = @(
        (Join-Path $env:LOCALAPPDATA 'Programs\Inno Setup 6\ISCC.exe'),
        (Join-Path ${env:ProgramFiles(x86)} 'Inno Setup 6\ISCC.exe'),
        (Join-Path $env:ProgramFiles 'Inno Setup 6\ISCC.exe')
    )
    foreach ($path in $candidates) {
        if (Test-Path $path) { return $path }
    }

    $onPath = Get-Command ISCC.exe -ErrorAction SilentlyContinue
    if ($onPath) { return $onPath.Source }

    throw 'Inno Setup 6 was not found. Install it (winget install JRSoftware.InnoSetup) and run this script again.'
}

$compiler = Find-Compiler

if (-not (Test-Path $driver)) {
    Write-Host 'Downloading the PawnIO driver setup...'
    New-Item -ItemType Directory -Force -Path (Split-Path $driver) | Out-Null
    Invoke-WebRequest -Uri $driverUrl -OutFile $driver -UseBasicParsing
}

if ((Get-FileHash $driver -Algorithm SHA256).Hash -ne $driverHash) {
    Remove-Item -LiteralPath $driver -Force
    throw 'PawnIO_setup.exe does not match the expected SHA-256 hash and was deleted.'
}

if (-not $SkipPublish) {
    & (Join-Path $PSScriptRoot 'publish.ps1')
}

$version = ([xml](Get-Content (Join-Path $root 'Directory.Build.props'))).Project.PropertyGroup.Version
if (-not $version) { throw 'No <Version> in Directory.Build.props.' }

& $compiler "/DAppVersion=$version" (Join-Path $root 'installer\FrameHeat.iss')
if ($LASTEXITCODE -ne 0) {
    throw "The Inno Setup compiler failed with exit code $LASTEXITCODE"
}

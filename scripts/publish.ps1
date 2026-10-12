# Builds the distributable program: one self-contained .exe that needs no .NET on the machine.
# Output: dist\FrameHeat.exe. The Release build asks for administrator rights when it starts.
$root = Split-Path $PSScriptRoot -Parent

dotnet publish (Join-Path $root 'src\FrameHeat\FrameHeat.csproj') `
    -c Release -r win-x64 --self-contained `
    -p:PublishSingleFile=true `
    -p:IncludeNativeLibrariesForSelfExtract=true `
    -o (Join-Path $root 'dist')

if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish failed with exit code $LASTEXITCODE"
}

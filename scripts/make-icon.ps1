# Draws the thermometer icon and packs it into a multi-size app.ico.
# Only needed when the icon changes: .\scripts\make-icon.ps1
Add-Type -AssemblyName System.Drawing

$sizes = 16, 24, 32, 48, 64, 128, 256
$target = Join-Path $PSScriptRoot '..\src\FrameHeat\Assets\app.ico'

function New-IconPng([int]$size) {
    $bmp = New-Object System.Drawing.Bitmap $size, $size, ([System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $g = [System.Drawing.Graphics]::FromImage($bmp)
    $g.SmoothingMode = [System.Drawing.Drawing2D.SmoothingMode]::AntiAlias
    $g.Clear([System.Drawing.Color]::Transparent)

    $s = $size / 64.0

    # Rounded dark background
    $bg = New-Object System.Drawing.Drawing2D.GraphicsPath
    $r = 14 * $s
    $bg.AddArc(0, 0, 2 * $r, 2 * $r, 180, 90)
    $bg.AddArc($size - 2 * $r - 1, 0, 2 * $r, 2 * $r, 270, 90)
    $bg.AddArc($size - 2 * $r - 1, $size - 2 * $r - 1, 2 * $r, 2 * $r, 0, 90)
    $bg.AddArc(0, $size - 2 * $r - 1, 2 * $r, 2 * $r, 90, 90)
    $bg.CloseFigure()
    $g.FillPath((New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255, 20, 24, 32))), $bg)

    # Thermometer body
    $tube = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255, 226, 232, 240))
    $g.FillEllipse($tube, 22 * $s, 8 * $s, 20 * $s, 20 * $s)
    $g.FillRectangle($tube, 22 * $s, 18 * $s, 20 * $s, 22 * $s)
    $g.FillEllipse($tube, 14 * $s, 30 * $s, 36 * $s, 36 * $s)

    # Red mercury and bulb
    $hot = New-Object System.Drawing.SolidBrush ([System.Drawing.Color]::FromArgb(255, 255, 77, 77))
    $g.FillEllipse($hot, 19 * $s, 35 * $s, 26 * $s, 26 * $s)
    $g.FillRectangle($hot, 28 * $s, 18 * $s, 8 * $s, 26 * $s)
    $g.FillEllipse($hot, 28 * $s, 14 * $s, 8 * $s, 8 * $s)

    $g.Dispose()
    $stream = New-Object System.IO.MemoryStream
    $bmp.Save($stream, [System.Drawing.Imaging.ImageFormat]::Png)
    $bmp.Dispose()
    return , $stream.ToArray()
}

$images = foreach ($size in $sizes) { , (New-IconPng $size) }

$out = New-Object System.IO.MemoryStream
$w = New-Object System.IO.BinaryWriter $out
$w.Write([uint16]0)               # reserved
$w.Write([uint16]1)               # type: icon
$w.Write([uint16]$sizes.Count)

$offset = 6 + 16 * $sizes.Count
for ($i = 0; $i -lt $sizes.Count; $i++) {
    $dim = if ($sizes[$i] -ge 256) { 0 } else { $sizes[$i] }
    $w.Write([byte]$dim); $w.Write([byte]$dim)
    $w.Write([byte]0); $w.Write([byte]0)
    $w.Write([uint16]1); $w.Write([uint16]32)
    $w.Write([uint32]$images[$i].Length)
    $w.Write([uint32]$offset)
    $offset += $images[$i].Length
}
foreach ($img in $images) { $w.Write($img) }
$w.Flush()

[System.IO.File]::WriteAllBytes($target, $out.ToArray())
Write-Output "Icon written: $target"

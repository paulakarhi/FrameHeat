# Third-party notices

FrameHeat itself is licensed under the MIT License (see `LICENSE`). It is built on, and its installer
redistributes, the software below. Each component keeps its own license, and the full texts are in the
`licenses` folder or at the end of this file. The source code of every component is public at the address given.

## LibreHardwareMonitorLib

- Version 0.9.6, compiled into `FrameHeat.exe` unmodified.
- License: Mozilla Public License 2.0 (`licenses/MPL-2.0.txt`)
- Source: https://github.com/LibreHardwareMonitor/LibreHardwareMonitor

The library reads the hardware sensors. The MPL requires that recipients can obtain the source of
the covered files; the address above is where it is published.

It brings these components, also compiled in unmodified:

| Component | Version | License | Source |
| --- | --- | --- | --- |
| BlackSharp.Core | 1.0.7 | MPL-2.0 (`licenses/MPL-2.0.txt`) | https://github.com/Blacktempel/BlackSharp |
| DiskInfoToolkit | 1.1.2 | MPL-2.0 (`licenses/MPL-2.0.txt`) | https://github.com/Blacktempel/DiskInfoToolkit |
| RAMSPDToolkit-NDD | 1.4.2 | MPL-2.0 (`licenses/MPL-2.0.txt`) | https://github.com/Blacktempel/RAMSPDToolkit |
| HidSharp | 2.6.4 | Apache License 2.0 (`licenses/Apache-2.0.txt`) | https://www.nuget.org/packages/HidSharp |
| Mono.Posix.NETStandard | 1.0.0 | MIT (below) | https://github.com/mono/mono |

HidSharp is Copyright 2010-2025 James F. Bellinger. It ships no separate NOTICE file.

## TraceEvent

- Microsoft.Diagnostics.Tracing.TraceEvent 3.1.16, with Microsoft.Diagnostics.NETCore.Client and the
  Microsoft.Extensions libraries it needs, compiled in unmodified. It reads the frame rate through Windows event tracing.
- License: MIT (below)
- Source: https://github.com/microsoft/perfview

TraceEvent also ships two closed-source Microsoft DLLs for reading trace files and symbols. FrameHeat does not
need them and leaves them out.

## .NET

- The .NET runtime, its class libraries and Windows Presentation Foundation are included in the self-contained `FrameHeat.exe`.
- License: MIT (below)
- The runtime in turn contains components under other licenses. Their notices, as published with .NET 9.0.8, are in
  `licenses/dotnet-THIRD-PARTY-NOTICES.txt`.
- Source: https://github.com/dotnet/runtime and https://github.com/dotnet/wpf

## PawnIO modules

- Embedded in LibreHardwareMonitorLib as binary resources (`IntelMSR`, `LpcIO`, `RyzenSMU`, `Smbus*` and others).
- License: GNU Lesser General Public License 2.1 (`licenses/LGPL-2.1.txt`)
- Source: https://github.com/namazso/PawnIO.Modules

## PawnIO driver and setup program

- Version 2.2.0, `PawnIO_setup.exe`, bundled in the installer and run silently when the driver is not
  yet installed. It is not part of `FrameHeat.exe`.
- The driver is licensed under the GNU General Public License 2.0 (`licenses/GPL-2.0.txt`)
- Source: https://github.com/namazso/PawnIO
- Releases: https://github.com/namazso/PawnIO.Setup
- Website: https://pawnio.eu/

The driver stays installed when FrameHeat is uninstalled, because other programs can use it too.
It can be removed from Windows' list of installed apps.

## MIT License

Applies to TraceEvent, the .NET components and Mono.Posix.NETStandard.
Copyright (c) Microsoft Corporation, the .NET Foundation and contributors, and the Mono project.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the "Software"), to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of
the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS
OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

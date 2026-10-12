# Contributing

Bug reports, ideas and pull requests are welcome.

Everyone taking part is expected to follow the [code of conduct](CODE_OF_CONDUCT.md).

## Reporting a problem

Open an issue and tell which Windows version, graphics card and processor you have. The easiest way to give the
details is the diagnostics report: in the settings window, press **Save diagnostics** and paste the file into the
issue. It lists the Windows version and the hardware and sensors found, and nothing personal.

For a frame rate that is missing or wrong, also name the game and say whether it runs fullscreen, borderless or in
a window.

## Building

See [Building](README.md#building) in the README. The .NET 9 SDK is all that is needed.

## Pull requests

- Keep a change small and about one thing.
- Follow the style of the code around it: English names and comments, and short comments that say why, not what.
- A text the user sees has to be added to all ten language files in
  `src/FrameHeat.Core/Localization/Translations`, written as plain product text.
- Say how you tried it: with which Windows version, hardware and, for the frame rate, which game.

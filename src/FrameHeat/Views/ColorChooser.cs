using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using WpfColor = System.Windows.Media.Color;

namespace FrameHeat.Views;

/// <summary>Lets the user pick any color with the standard Windows color dialog.</summary>
internal static class ColorChooser
{
    /// <summary>The color as "#RRGGBB", or null when the dialog is cancelled.</summary>
    public static string? Choose(string currentHex, System.Windows.Window owner)
    {
        using var dialog = new ColorDialog { AnyColor = true, FullOpen = true, Color = ToDrawing(FromHex(currentHex)) };
        var handle = new WindowInteropHelper(owner).Handle;

        return dialog.ShowDialog(new OwnerWindow(handle)) == DialogResult.OK
            ? ToHex(dialog.Color.R, dialog.Color.G, dialog.Color.B)
            : null;
    }

    internal static string ToHex(byte red, byte green, byte blue) => $"#{red:X2}{green:X2}{blue:X2}";

    internal static WpfColor FromHex(string hex) => (WpfColor)ColorConverter.ConvertFromString(hex);

    private static System.Drawing.Color ToDrawing(WpfColor color) => System.Drawing.Color.FromArgb(color.R, color.G, color.B);

    // The dialog needs the window it belongs to as a plain handle.
    private sealed class OwnerWindow(IntPtr handle) : System.Windows.Forms.IWin32Window
    {
        public IntPtr Handle { get; } = handle;
    }
}

using System.Windows;

namespace FrameHeat.Views;

/// <summary>
/// A yes/no question or a one-button notice. It replaces the system message box, whose button
/// captions follow the Windows language instead of the program's and which wraps text unpredictably.
/// </summary>
internal sealed partial class MessageDialog : Window
{
    public MessageDialog(string message, bool isQuestion)
    {
        InitializeComponent();

        MessageText.Text = message;
        ConfirmButton.Content = Loc.T(isQuestion ? "button_yes" : "button_ok");
        DeclineButton.Content = Loc.T("button_no");

        // A notice has a single button, which also answers Escape.
        DeclineButton.Visibility = isQuestion ? Visibility.Visible : Visibility.Collapsed;
        ConfirmButton.IsCancel = !isQuestion;
    }

    /// <summary>True only when the confirm button was pressed; closing the window counts as no.</summary>
    public bool Accepted { get; private set; }

    public static bool Ask(string message)
    {
        var dialog = new MessageDialog(message, isQuestion: true);
        dialog.ShowDialog();
        return dialog.Accepted;
    }

    public static void Warn(string message) => new MessageDialog(message, isQuestion: false).ShowDialog();

    private void OnConfirm(object sender, RoutedEventArgs e)
    {
        Accepted = true;
        Close();
    }

    private void OnDecline(object sender, RoutedEventArgs e) => Close();
}

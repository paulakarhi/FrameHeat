using System.Windows.Input;

namespace FrameHeat.ViewModels;

/// <summary>A button command that simply runs an action.</summary>
internal sealed class RelayCommand(Action execute) : ICommand
{
    // The commands are always executable, so the event never fires.
    public event EventHandler? CanExecuteChanged
    {
        add { }
        remove { }
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => execute();
}

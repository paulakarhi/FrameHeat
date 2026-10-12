using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FrameHeat.Core;

/// <summary>Base for objects that tell the user interface when one of their properties changes.</summary>
public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool Set<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(name);
        return true;
    }

    /// <summary>
    /// Replaces a nested observable object and moves the change subscription from the old
    /// instance to the new one. A null value falls back to a fresh default instance.
    /// </summary>
    protected bool SetChild<T>(ref T field, T? value, PropertyChangedEventHandler onChanged, [CallerMemberName] string? name = null)
        where T : ObservableObject, new()
    {
        value ??= new T();
        if (ReferenceEquals(field, value))
            return false;

        field.PropertyChanged -= onChanged;
        field = value;
        field.PropertyChanged += onChanged;
        OnPropertyChanged(name);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

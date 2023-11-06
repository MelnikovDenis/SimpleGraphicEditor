using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels.EventControllers;

public class ClickController
{
    public bool CanClick { get; set; } = false;
    public event Action<object> OnClick;
    public ClickController(Action<object> onClick)
    {
        OnClick += onClick;
    }
    public void LeftMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanClick && sender is Shape clickable)
        {            
            OnClick?.Invoke(sender);
            eventArgs.Handled = true;
        }
    }
}
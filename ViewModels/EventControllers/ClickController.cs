using System;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels.EventControllers;

public class ClickController
{
    public bool CanClick { get; set; } = false;
    public event Action<object> OnLeftMouseClick;
    public event Action<object> OnRightMouseClick;
    public ClickController(Action<object> onLeftMouseClick, Action<object> onRightMouseClick)
    {
        OnLeftMouseClick += onLeftMouseClick;
        OnRightMouseClick += onRightMouseClick;
    }
    public void LeftMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanClick && sender is Shape clickable)
        {            
            OnLeftMouseClick?.Invoke(sender);
            eventArgs.Handled = true;
        }
    }
    public void RightMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanClick && sender is Shape clickable)
        {            
            OnRightMouseClick?.Invoke(sender);
            eventArgs.Handled = true;
        }
    }
}
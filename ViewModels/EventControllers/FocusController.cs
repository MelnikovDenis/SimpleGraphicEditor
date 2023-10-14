using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.EventControllers;
public class FocusController
{
    public bool CanFocus { get; set; } = false;
    public Brush? DefaultFillBrush { get; set; } = null;
    public Brush? DefaultStrokeBrush { get; set; } = null;
    public Brush FocusFillBrush { get; set; }
    public Brush FocusStrokeBrush { get; set; }
    public FocusController(
          Brush focusFillBrush,
          Brush focusStrokeBrush)
    {
        FocusFillBrush = focusFillBrush;
        FocusStrokeBrush = focusStrokeBrush;
    }
    public void OnMouseEnter(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus && sender is Shape focusable)
        {
            DefaultFillBrush = focusable.Fill;
            DefaultStrokeBrush = focusable.Stroke;
            focusable.Fill = FocusFillBrush;
            focusable.Stroke = FocusStrokeBrush;
            eventArgs.Handled = true;
        }
    }
    public void OnMouseLeave(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus && sender is Shape focusable)
        {
            focusable.Fill = DefaultFillBrush;
            focusable.Stroke = DefaultStrokeBrush;
            DefaultFillBrush = null;
            DefaultStrokeBrush = null;
            eventArgs.Handled = true;
        }
    }
}
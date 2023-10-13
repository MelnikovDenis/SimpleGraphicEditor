using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.EventControllers;
public class FocusController
{
    public bool CanFocus { get; set; } = false;
    public Brush DefaultFillBrush { get; set; }
    public Brush DefaultStrokeBrush { get; set; }
    public Brush FocusFillBrush { get; set; }
    public Brush FocusStrokeBrush { get; set; }
    public FocusController(Brush defaultFillBrush,
          Brush defaultStrokeBrush,
          Brush focusFillBrush,
          Brush focusStrokeBrush)
    {
        DefaultFillBrush = defaultFillBrush;
        DefaultStrokeBrush = defaultStrokeBrush;
        FocusFillBrush = focusFillBrush;
        FocusStrokeBrush = focusStrokeBrush;
    }
    public void OnMouseEnter(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus && sender is Shape focusable)
        {
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
            eventArgs.Handled = true;
        }
    }
}
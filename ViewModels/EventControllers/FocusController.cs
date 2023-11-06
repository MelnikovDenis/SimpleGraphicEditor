using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.EventControllers;
public class FocusController
{
    public bool CanFocus { get; set; } = false;
    public Brush? DefaultFillBrush { get; set; } = null;
    public Brush? DefaultStrokeBrush { get; set; } = null;
    public Brush? FocusFillBrush { get; set; } = null;
    public Brush? FocusStrokeBrush { get; set; } = null;
    public FocusController(Brush? focusFillBrush, Brush? focusStrokeBrush)
    {
        FocusFillBrush = focusFillBrush;
        FocusStrokeBrush = focusStrokeBrush;
    }
    public void MouseEnterHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus && sender is Shape focusable)
        {
            DefaultFillBrush = focusable.Fill;
            DefaultStrokeBrush = focusable.Stroke;
            if(FocusFillBrush != null)
                focusable.Fill = FocusFillBrush;
            if(FocusStrokeBrush != null)
                focusable.Stroke = FocusStrokeBrush;
            eventArgs.Handled = true;
        }
    }
    public void OnMouseLeaveHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus && sender is Shape focusable)
        {
            if (DefaultFillBrush != null)
                focusable.Fill = DefaultFillBrush;
            if (DefaultStrokeBrush != null)
                focusable.Stroke = DefaultStrokeBrush;
            DefaultFillBrush = null;
            DefaultStrokeBrush = null;
            eventArgs.Handled = true;
        }
    }
}
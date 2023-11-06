using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleGraphicEditor.ViewModels.EventControllers;
public class DragController
{
    private Canvas TargetCanvas { get; set; }
    private Point? LastTouch { get; set; } = null;
    public bool IsDragging { get; set; } = false;
    public bool CanDragging { get; set; } = false;
    public bool NeedCaptureMouse { get; set; }
    public event Action<object, Point> OnMove;
    public DragController(Canvas targetCanvas, Action<object, Point> onMove, bool needCaptureMouse = false)
    {
        NeedCaptureMouse = needCaptureMouse;
        TargetCanvas = targetCanvas;
        OnMove = onMove;
    }
    public void MouseLeftButtonDownHandler(object sender, MouseButtonEventArgs eventArgs)
    {
        if (sender is UIElement draggable && CanDragging)
        {
            if(NeedCaptureMouse)
                draggable.CaptureMouse();
            Debug.Write(LastTouch);
            LastTouch = eventArgs.GetPosition(TargetCanvas);
            Debug.WriteLine($"  =>  {LastTouch}");
            IsDragging = true;
            eventArgs.Handled = true;
        }
    }
    public void MouseMoveHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanDragging &&
            IsDragging &&
            LastTouch != null &&
            eventArgs.LeftButton == MouseButtonState.Pressed &&
            sender is UIElement draggable           
        )
        {
            var dropPosition = eventArgs.GetPosition(TargetCanvas);                
            OnMove?.Invoke(sender, new Point(dropPosition.X - LastTouch.Value.X, dropPosition.Y - LastTouch.Value.Y));
            LastTouch = dropPosition;
            eventArgs.Handled = true;
        }
    }
    public void MouseLeftButtonUpHandler(object sender, MouseButtonEventArgs eventArgs)
    {
        if (CanDragging && sender is UIElement draggable)
        {
            if (NeedCaptureMouse)
                draggable.ReleaseMouseCapture();
            LastTouch = null;
            IsDragging = false;
            eventArgs.Handled = true;
        }
    }   
}
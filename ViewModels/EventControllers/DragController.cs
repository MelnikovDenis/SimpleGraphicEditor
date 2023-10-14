using SimpleGraphicEditor.Models.Abstractions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels.EventControllers;
public class DragController
{
        private Canvas TargetCanvas { get; set; }
        private Point? LastTouch { get; set; } = null;
        private bool IsDragging { get; set; } = false;
        public bool CanDragging { get; set; } = false;
        public Action<object, Point> MoveToNewPosition { get; set; }
        public DragController(Canvas targetCanvas, Action<object, Point> moveToNewPosition)
        {
            TargetCanvas = targetCanvas;
            MoveToNewPosition = moveToNewPosition;
        }
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (sender is Shape draggable && CanDragging)
            {
                draggable.CaptureMouse();
                LastTouch = eventArgs.GetPosition(TargetCanvas);
                IsDragging = true;
                eventArgs.Handled = true;
            }
        }
        public void OnMouseMove(object sender, MouseEventArgs eventArgs)
        {
            if (CanDragging &&
                IsDragging &&
                LastTouch != null &&
                eventArgs.LeftButton == MouseButtonState.Pressed &&
                sender is Shape draggable                
            )
            {
                var dropPosition = eventArgs.GetPosition(TargetCanvas);                
                MoveToNewPosition(sender, new Point(dropPosition.X - LastTouch.Value.X, dropPosition.Y - LastTouch.Value.Y));
                LastTouch = dropPosition;
                eventArgs.Handled = true;
            }
        }
        public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs eventArgs)
        {
            if (CanDragging && sender is Shape draggable)
            {
                draggable.ReleaseMouseCapture();
                LastTouch = null;
                IsDragging = false;
                eventArgs.Handled = true;
            }
        }   
}
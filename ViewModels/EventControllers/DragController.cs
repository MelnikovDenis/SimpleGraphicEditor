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
        private bool IsDragging { get; set; } = false;
        public bool CanDragging { get; set; } = false;
        public Action<object, Point> SetNewPosition { get; set; }
        public DragController(Canvas targetCanvas, Action<object, Point> setNewPosition)
        {
            TargetCanvas = targetCanvas;
            SetNewPosition = setNewPosition;
        }
        public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (sender is Shape draggable && CanDragging)
            {
                draggable.CaptureMouse();
                IsDragging = true;
                eventArgs.Handled = true;
            }
        }
        public void OnMouseMove(object sender, MouseEventArgs eventArgs)
        {
            if (CanDragging && 
                eventArgs.LeftButton == MouseButtonState.Pressed && 
                sender is Shape draggable && 
                IsDragging)
            {
                var dropPosition = eventArgs.GetPosition(TargetCanvas);
                if (dropPosition.X < 0)
                    dropPosition.X = 0;
                if (dropPosition.Y < 0)
                    dropPosition.Y = 0;
                if (dropPosition.X > TargetCanvas.ActualWidth)
                    dropPosition.X = TargetCanvas.ActualWidth;
                if (dropPosition.Y > TargetCanvas.ActualHeight)
                    dropPosition.Y = TargetCanvas.ActualHeight;
                SetNewPosition(sender, dropPosition);            
                eventArgs.Handled = true;
            }
        }
        public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs eventArgs)
        {
            if (CanDragging && sender is Shape draggable)
            {
                draggable.ReleaseMouseCapture();
                IsDragging = false;
                eventArgs.Handled = true;
            }
        }   
}
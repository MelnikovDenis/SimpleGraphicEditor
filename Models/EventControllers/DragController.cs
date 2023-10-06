using System.Windows;
using SimpleGraphicEditor.Models.Static;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Diagnostics;
namespace SimpleGraphicEditor.Models.EventControllers;
public class DragController
{
      private Canvas TargetCanvas { get; set; }
      private bool IsDragging { get; set; } = false;
      public bool CanDragging { get; set; } = false;
      public DragController(Canvas targetCanvas)
      {
            TargetCanvas = targetCanvas;
      }
      public void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
      {            
            var draggable = sender as Shape;
            if(draggable != null && CanDragging)
            {
                  draggable.CaptureMouse(); 
                  IsDragging = true;                                                                                                                      
                  eventArgs.Handled = true;
            }        
      }
      public void OnMouseMove(object sender, MouseEventArgs eventArgs)
      {            
            var draggable = sender as Shape;
            if(CanDragging && eventArgs.LeftButton == MouseButtonState.Pressed && draggable != null && IsDragging)
            {                                    
                  var dropPosition = eventArgs.GetPosition(TargetCanvas);
                  if(dropPosition.X < 0)
                        dropPosition.X = 0;
                  if(dropPosition.Y < 0)
                        dropPosition.Y = 0;
                  if(dropPosition.X > TargetCanvas.ActualWidth)
                        dropPosition.X = TargetCanvas.ActualWidth;
                  if(dropPosition.Y > TargetCanvas.ActualHeight)
                        dropPosition.Y = TargetCanvas.ActualHeight;
                  draggable.SetCoordinates(dropPosition);              
                  eventArgs.Handled = true;                                
            } 
      }
      public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs eventArgs)
      {
            var draggable = sender as Shape;        
            if(CanDragging && draggable != null)
            {
                  draggable.ReleaseMouseCapture();
                  IsDragging = false;
                  eventArgs.Handled = true;
            }
      }
      
}
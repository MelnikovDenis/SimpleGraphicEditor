using System.Windows;
using SimpleGraphicEditor.Models.Static;
using System.Windows.Controls;
using System.Windows.Input;
namespace SimpleGraphicEditor.Models;
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
            var draggable = sender as FrameworkElement;
            if(draggable != null && CanDragging)
            {
                  draggable.CaptureMouse(); 
                  IsDragging = true;                                                                                                                      
                  eventArgs.Handled = true;
            }        
      }
      public void OnMouseMove(object sender, MouseEventArgs eventArgs)
      {            
            var draggable = sender as FrameworkElement;
            if(eventArgs.LeftButton == MouseButtonState.Pressed && draggable != null && IsDragging)
            {                                    
                  var dropPosition = eventArgs.GetPosition(TargetCanvas);         
                  draggable.SetCoordinates(dropPosition);   
                  eventArgs.Handled = true;
            } 
      }
      public void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs eventArgs)
      {
            var draggable = sender as FrameworkElement;        
            if(draggable != null)
            {
                  draggable.ReleaseMouseCapture();
                  IsDragging = false;
                  eventArgs.Handled = true;
            }
      }
      
}
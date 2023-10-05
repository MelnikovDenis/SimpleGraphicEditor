using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using SimpleGraphicEditor.Models.Static;
using SimpleGraphicEditor.Models.EventControllers;
using System.Windows.Data;
namespace SimpleGraphicEditor.Models;
public class PointFactory
{      
      private Canvas TargetCanvas { get; set; }
      private DragController DragController { get; set; }
      private FocusController FocusController { get; set; }
      public PointFactory(Canvas targetCanvas, 
            DragController dragController, 
            FocusController focusController)
      {
            TargetCanvas = targetCanvas;
            DragController = dragController;
            FocusController = focusController;            
      }
      public Ellipse CreateVisiblePoint(Point point)
      {
            var ellipse = new Ellipse()
            {
                  Fill = DefaultValues.DefaultPointBrush,
                  Width = DefaultValues.DefualutPointDiameter,
                  Height = DefaultValues.DefualutPointDiameter
            };
            Canvas.SetZIndex(ellipse, DefaultValues.DefaultPointZIndex);
            ellipse.SetCoordinates(point);
            TargetCanvas.Children.Add(ellipse);
            ellipse.MouseMove += DragController.OnMouseMove;
            ellipse.MouseLeftButtonDown += DragController.OnMouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += DragController.OnMouseLeftButtonUp;
            ellipse.MouseEnter += FocusController.OnMouseEnter;
            ellipse.MouseLeave += FocusController.OnMouseLeave;
            return ellipse;            
      }
      public void Remove(Ellipse ellipse)
      {
            ellipse.MouseMove -= DragController.OnMouseMove;
            ellipse.MouseLeftButtonDown -= DragController.OnMouseLeftButtonDown;
            ellipse.MouseLeftButtonUp -= DragController.OnMouseLeftButtonUp;
            ellipse.MouseEnter -= FocusController.OnMouseEnter;
            ellipse.MouseLeave -= FocusController.OnMouseLeave;
            TargetCanvas.Children.Remove(ellipse);
      }
}
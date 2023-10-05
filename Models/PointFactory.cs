using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using SimpleGraphicEditor.Models.Static;
namespace SimpleGraphicEditor.Models;
public class PointFactory
{
      public static Brush DefaultBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
      public static Brush FocusBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));
      public static int DefaultZIndex { get; } = 1;
      public static double Diameter { get; } = 10d;

      private Canvas TargetCanvas { get; set; }
      private DragController DragController { get; set; }
      public PointFactory(Canvas targetCanvas, DragController dragController)
      {
            TargetCanvas = targetCanvas;
            DragController = dragController;
      }
      public Ellipse CreateVisiblePoint(Point point)
      {
            var ellipse = new Ellipse()
            {
                  Fill = DefaultBrush,
                  Width = Diameter,
                  Height = Diameter
            };
            Canvas.SetZIndex(ellipse, DefaultZIndex);
            ellipse.SetCoordinates(point);
            TargetCanvas.Children.Add(ellipse);
            ellipse.AllowDrop = true;
            ellipse.MouseMove += DragController.OnMouseMove;
            ellipse.MouseLeftButtonDown += DragController.OnMouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += DragController.OnMouseLeftButtonUp;            
            return ellipse;            
      }      
}
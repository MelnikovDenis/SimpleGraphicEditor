using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models.Static;
public static class ShapeElementExtension
{
      public static Point GetCenter(this Shape shape) =>
            new Point(Canvas.GetLeft(shape) + shape.Width / 2d, 
            Canvas.GetTop(shape) + shape.Height / 2d);
      public static void SetCoordinates(this Shape shape, Point newPosition)
      {
            Canvas.SetLeft(shape, newPosition.X - shape.Width / 2d);
            Canvas.SetTop(shape, newPosition.Y - shape.Height / 2d);
      }
      public static void ScaleFromCenter(this Shape shape, double scaleСoefficient)
      {
            var position = shape.GetCenter();
            Debug.WriteLine($"old center: {position.X}, {position.Y}");
            shape.Height *= scaleСoefficient;
            shape.Width *= scaleСoefficient;
            shape.SetCoordinates(position);
            position = shape.GetCenter();
            Debug.WriteLine($"new center: {position.X}, {position.Y}");
      }
}
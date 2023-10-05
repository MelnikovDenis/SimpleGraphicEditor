using System.Windows;
using System.Windows.Controls;

namespace SimpleGraphicEditor.Models.Static;
public static class FrameworkElementExtension
{
      public static Point GetCenter(this FrameworkElement fElement) =>
            new Point(Canvas.GetLeft(fElement) - fElement.Width / 2d, 
            Canvas.GetTop(fElement) - fElement.Height / 2d);
      public static void SetCoordinates(this FrameworkElement fElement, Point newPosition)
      {
            fElement.SetValue(Canvas.LeftProperty, newPosition.X - fElement.Width / 2d);
            fElement.SetValue(Canvas.TopProperty, newPosition.Y - fElement.Height / 2d);
      }
}
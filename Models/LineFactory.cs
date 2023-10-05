using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using SimpleGraphicEditor.Models.Converters;
namespace SimpleGraphicEditor.Models;
public class LineFactory
{
      public static Brush DefaultBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 255));
      public static double DefaultThickness { get; } = 2d;
      private Canvas TargetCanvas { get; set; }
      public Ellipse? Buffer { get; set; } = null;
      public LineFactory(Canvas targetCanvas)
      {
            TargetCanvas = targetCanvas;
      }
      public Line CreateLineFromBuffer(Ellipse endPoint)
      {
            if(Buffer == null)
                  throw new NullReferenceException("Стартовая точка должна быть указана.");
            var line = new Line()
            {
                  Stroke = DefaultBrush,
                  StrokeThickness = DefaultThickness
            };
            line.SetBinding(Line.X1Property, new Binding()
            {
                  Source = Buffer,
                  Path = new PropertyPath(Canvas.LeftProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = endPoint.Width
            });
            line.SetBinding(Line.Y1Property, new Binding()
            {
                  Source = Buffer,
                  Path = new PropertyPath(Canvas.TopProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = endPoint.Height
            });
            line.SetBinding(Line.X2Property, new Binding()
            {
                  Source = endPoint,
                  Path = new PropertyPath(Canvas.LeftProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = endPoint.Width
            });
            line.SetBinding(Line.Y2Property, new Binding()
            {
                  Source = endPoint,
                  Path = new PropertyPath(Canvas.TopProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = endPoint.Height
            });
            TargetCanvas.Children.Add(line);
            return line;
      }
}
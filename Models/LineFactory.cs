using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using SimpleGraphicEditor.Models.Converters;
using SimpleGraphicEditor.Models.Static;
using SimpleGraphicEditor.Models.EventControllers;
namespace SimpleGraphicEditor.Models;
public class LineFactory
{     
      private Canvas TargetCanvas { get; set; }
      private FocusController FocusController { get; set; }
      //private DragController DragController { get; set; }
      public Ellipse? Buffer { get; set; } = null;
      public LineFactory(Canvas targetCanvas,
            //DragController dragController,
            FocusController focusController)
      {
            TargetCanvas = targetCanvas;
            //DragController = dragController;
            FocusController = focusController;        
      }
      public Line CreateLineFromBuffer(Ellipse endPoint)
      {
            if(Buffer == null)
                  throw new NullReferenceException("Стартовая точка должна быть указана.");
            var line = new Line()
            {
                  Stroke = DefaultValues.DefaultLineBrush,
                  StrokeThickness = DefaultValues.DefaultLineThickness
            };
            line.SetBinding(Line.X1Property, new Binding()
            {
                  Source = Buffer,
                  Path = new PropertyPath(Canvas.LeftProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = Buffer.Width
                  //, Mode = BindingMode.TwoWay
            });
            line.SetBinding(Line.Y1Property, new Binding()
            {
                  Source = Buffer,
                  Path = new PropertyPath(Canvas.TopProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = Buffer.Height
                  //, Mode = BindingMode.TwoWay
            });
            line.SetBinding(Line.X2Property, new Binding()
            {
                  Source = endPoint,
                  Path = new PropertyPath(Canvas.LeftProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = endPoint.Width
                  //, Mode = BindingMode.TwoWay
            });
            line.SetBinding(Line.Y2Property, new Binding()
            {
                  Source = endPoint,
                  Path = new PropertyPath(Canvas.TopProperty),
                  Converter = new CenterCoordinateConverter(),
                  ConverterParameter = endPoint.Height
                  //, Mode = BindingMode.TwoWay
            });

            //line.MouseMove += DragController.OnMouseMove;
            //line.MouseLeftButtonDown += DragController.OnMouseLeftButtonDown;
            //line.MouseLeftButtonUp += DragController.OnMouseLeftButtonUp;

            line.MouseEnter += FocusController.OnMouseEnter;
            line.MouseLeave += FocusController.OnMouseLeave;   

            TargetCanvas.Children.Add(line);
            return line;
      }
      public void Remove(Line line)
      {
            BindingOperations.ClearAllBindings(line);
            
            line.MouseEnter -= FocusController.OnMouseEnter;
            line.MouseLeave -= FocusController.OnMouseLeave; 

            TargetCanvas.Children.Remove(line);
      }
}
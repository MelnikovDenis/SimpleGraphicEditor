using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;
using SimpleGraphicEditor.Models;
using System.Collections.Generic;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;

namespace SimpleGraphicEditor.ViewModels;
public class LineViewModel
{          
        public FocusController FocusController { get; set; }
        public LineViewModel(FocusController focusController)
        {
            FocusController = focusController;
        }
        public (Line, SgeLine) CreateLine(SgePoint point1, SgePoint point2)
        {
            var sgeLine = new SgeLine(point1, point2);

            var line = CreateLine(sgeLine);

            return (line, sgeLine);
        }
        private Line CreateLine(SgeLine sgeLine) 
        {
            var line = new Line()
            {
                Stroke = DefaultValues.DefaultLineBrush,
                StrokeThickness = DefaultValues.DefaultLineThickness,
            };

            line.SetBinding(Line.X1Property, new Binding()
            {
                Source = sgeLine.Point1,
                Path = new PropertyPath(nameof(sgeLine.Point1.X)),
                Mode = BindingMode.OneWay
            });
            line.SetBinding(Line.Y1Property, new Binding()
            {
                Source = sgeLine.Point1,
                Path = new PropertyPath(nameof(sgeLine.Point1.Y)),
                Mode = BindingMode.OneWay
            });
            line.SetBinding(Line.X2Property, new Binding()
            {
                Source = sgeLine.Point2,
                Path = new PropertyPath(nameof(sgeLine.Point2.X)),
                Mode = BindingMode.OneWay
            });
            line.SetBinding(Line.Y2Property, new Binding()
            {
                Source = sgeLine.Point2,
                Path = new PropertyPath(nameof(sgeLine.Point2.Y)),
                Mode = BindingMode.OneWay
            });

            line.MouseEnter += FocusController.OnMouseEnter;
            line.MouseLeave += FocusController.OnMouseLeave;
            return line;
        }
}
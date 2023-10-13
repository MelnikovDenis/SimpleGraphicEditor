using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;
using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.Converters;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;

namespace SimpleGraphicEditor.ViewModels;
public class PointViewModel
{   
        public FocusController FocusController { get; set; }
        public DragController DragController { get; set; }
        public PointViewModel(FocusController focusController, DragController dragController)
        {
            FocusController = focusController;
            DragController = dragController;
        }
        public (Ellipse, SgePoint) CreatePoint(Point point)
        {
                var sgePoint = new SgePoint(point);

                var ellipse = CreateEllipse(sgePoint);

                return (ellipse, sgePoint);
        }
        private Ellipse CreateEllipse(SgePoint sgePoint) 
        {
            var ellipse = new Ellipse()
            {
                Fill = DefaultValues.DefaultPointBrush,
                Width = DefaultValues.DefualutPointDiameter,
                Height = DefaultValues.DefualutPointDiameter
            };

            Canvas.SetZIndex(ellipse, DefaultValues.DefaultPointZIndex);
            ellipse.SetBinding(Canvas.LeftProperty, new Binding()
            {
                Source = sgePoint,
                Path = new PropertyPath(nameof(sgePoint.X)),
                Converter = new CenterCoordinateConverter(),
                ConverterParameter = ellipse.Width,
                Mode = BindingMode.OneWay
            });
            ellipse.SetBinding(Canvas.TopProperty, new Binding()
            {
                Source = sgePoint,
                Path = new PropertyPath(nameof(sgePoint.Y)),
                Converter = new CenterCoordinateConverter(),
                ConverterParameter = ellipse.Height,
                Mode = BindingMode.OneWay
            });

            ellipse.MouseMove += DragController.OnMouseMove;
            ellipse.MouseLeftButtonDown += DragController.OnMouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += DragController.OnMouseLeftButtonUp;
            ellipse.MouseEnter += FocusController.OnMouseEnter;
            ellipse.MouseLeave += FocusController.OnMouseLeave;
            return ellipse;
        }
}
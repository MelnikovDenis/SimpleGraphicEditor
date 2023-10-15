using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Data;
using System.Collections.Generic;
using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.Converters;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows.Media;
using System.Windows.Ink;
using System.Windows.Media.Media3D;

namespace SimpleGraphicEditor.ViewModels;
public class PointFactory
{
        public Brush PointBrush { get; } 
        public Brush PointStrokeBrush { get; }
        public int PointZIndex { get; }
        public double PointStrokeThickness { get; }
        public double PointDiameter { get; }
        public FocusController FocusController { get; set; }
        public DragController DragController { get; set; }
        public PointFactory(FocusController focusController, DragController dragController)
        {
            FocusController = focusController;
            DragController = dragController;

            PointBrush = DefaultValues.DefaultPointBrush;
            PointZIndex = DefaultValues.DefaultPointZIndex;
            PointStrokeBrush = DefaultValues.DefaultPointBrush;
            PointStrokeThickness = DefaultValues.DefaultPointStrokeThickness;
            PointDiameter = DefaultValues.DefualtPointDiameter;
        }
        public PointFactory(FocusController focusController, DragController dragController, 
            Brush pointBrush,
            Brush pointStrokeBrush,
            int pointZIndex,
            double pointStrokeThickness, 
            double pointDiameter)
        {
            FocusController = focusController;
            DragController = dragController;

            PointBrush = pointBrush;
            PointStrokeBrush = pointStrokeBrush;
            PointZIndex = pointZIndex;
            PointStrokeThickness = pointStrokeThickness;
            PointDiameter = pointDiameter;
        }
        public (Ellipse, SgePoint) CreatePoint(Point point)
        {
                var sgePoint = new SgePoint(point);

                var ellipse = CreateEllipse(sgePoint);

                return (ellipse, sgePoint);
        }
        public Ellipse CreateEllipse(SgePoint sgePoint) 
        {
            var ellipse = new Ellipse()
            {
                Fill = PointBrush,
                Stroke = PointStrokeBrush,
                StrokeThickness = PointStrokeThickness,
                Width = PointDiameter,
                Height = PointDiameter
            };

            Canvas.SetZIndex(ellipse, PointZIndex);
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
using SimpleGraphicEditor.ViewModels.Converters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models.Abstractions;

public abstract class VisiblePoint3D : Point3D
{
    protected Canvas TargetCanvas { get; set; }
    public Ellipse VisibleEllipse { get; protected set; }
    protected VisiblePoint3D(double realX, double realY, double realZ, double minCoordinate, double maxCoordinate, Observer observer, Canvas targetCanvas) : base(realX, realY, realZ, observer, minCoordinate, maxCoordinate)
    {
        VisibleEllipse = CreateVisibleEllipse();
        TargetCanvas = targetCanvas;
        VisibleEllipse.SetBinding(Canvas.LeftProperty, new Binding()
        {
            Source = this,
            Path = new PropertyPath(nameof(VisibleX)),
            Mode = BindingMode.OneWay,
            Converter = new CenterCoordinateConverter(),
            ConverterParameter = VisibleEllipse.Height
        });
        VisibleEllipse.SetBinding(Canvas.TopProperty, new Binding()
        {
            Source = this,
            Path = new PropertyPath(nameof(VisibleY)),
            Mode = BindingMode.OneWay,
            Converter = new CenterCoordinateConverter(),
            ConverterParameter = VisibleEllipse.Width
        });
        TargetCanvas.Children.Add(VisibleEllipse);
    }
    protected abstract Ellipse CreateVisibleEllipse();
}

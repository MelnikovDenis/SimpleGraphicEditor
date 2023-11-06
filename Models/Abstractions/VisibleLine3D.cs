using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models.Abstractions;

public abstract class VisibleLine3D : Line3D
{
    protected Canvas TargetCanvas { get; set; }
    public Line VisibleLine { get; set; }
    protected VisibleLine3D(Canvas targetCanvas, Point3D firstPoint, Point3D secondPoint) : base(firstPoint, secondPoint)
    {
        TargetCanvas = targetCanvas;
        VisibleLine = CreateVisibleLine();
        VisibleLine.SetBinding(Line.X1Property, new Binding()
        {
            Source = FirstPoint,
            Path = new PropertyPath(nameof(FirstPoint.VisibleX)),
            Mode = BindingMode.OneWay
        });
        VisibleLine.SetBinding(Line.Y1Property, new Binding()
        {
            Source = FirstPoint,
            Path = new PropertyPath(nameof(FirstPoint.VisibleY)),
            Mode = BindingMode.OneWay
        });
        VisibleLine.SetBinding(Line.X2Property, new Binding()
        {
            Source = SecondPoint,
            Path = new PropertyPath(nameof(SecondPoint.VisibleX)),
            Mode = BindingMode.OneWay
        });
        VisibleLine.SetBinding(Line.Y2Property, new Binding()
        {
            Source = SecondPoint,
            Path = new PropertyPath(nameof(SecondPoint.VisibleY)),
            Mode = BindingMode.OneWay
        });
        TargetCanvas.Children.Add(VisibleLine);
    }
    protected abstract Line CreateVisibleLine();
}
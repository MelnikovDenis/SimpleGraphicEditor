using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models;

public class CoordinateAxis : VisibleLine3D
{
    public CoordinateAxis(Canvas targetCanvas, Brush axisBrush, Point3D firstPoint, Point3D secondPoint) : base(targetCanvas, firstPoint, secondPoint)
    {
        VisibleLine.Stroke = axisBrush;
        Canvas.SetZIndex(VisibleLine, DefaultValues.AxisLineZIndex);
    }
    protected override Line CreateVisibleLine()
    {
        var line = new Line()
        {
            StrokeThickness = DefaultValues.AxisLineThickness            
        };
        return line;
    }
}
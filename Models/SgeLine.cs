using SimpleGraphicEditor.Models.Abstractions;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models;

class SgeLine : INotifyPropertyChanged, IDrawable
{
    public static double DefaultThickness { get; } = 2;
    public static Brush DefaultBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 255));
    public Line Line { get; private set; }
    public SgePoint StartPoint { get; private set; }
    public SgePoint EndPoint { get; private set; } = null!;
    public Canvas TargetCanvas { get; private set; } = null!;
    public SgeLine(SgePoint startPoint, SgePoint endPoint, Canvas canvas)
    {
        TargetCanvas = canvas;
        StartPoint = startPoint;
        EndPoint = endPoint;
        Line = new Line()
        {
            Stroke = DefaultBrush,
            StrokeThickness = DefaultThickness
        };
        Line.X1 = StartPoint.X;
        Line.Y1 = StartPoint.Y;
        Line.X2 = EndPoint.X;
        Line.Y2 = EndPoint.Y;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void Draw()
    {
        TargetCanvas.Children.Add(Line);
    }
}

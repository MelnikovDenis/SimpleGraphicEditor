using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.Static;
public static class DefaultValues
{
    public static Brush FocusPointStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(221, 156, 87));
    public static Brush FocusLineStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(221, 156, 87));
    public static Brush SelectPointFillBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
    public static Brush SelectPointStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));

    public static Brush XAxisBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));
    public static Brush YAxisBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
    public static Brush ZAxisBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 255));
    public static int AxisLineZIndex { get; } = 2;  
    public static double AxisLineLength { get; } = 50d;
    public static double AxisLineThickness { get; } = 1d;

    public static Brush DefaultLineBrush { get; } = new SolidColorBrush(Color.FromRgb(47, 48, 52));
    public static int DefaultLineZIndex { get; } = 1;
    public static double DefaultLineThickness { get; } = 1.5d;

    public static Brush DefaultPointFillBrush { get; } = new SolidColorBrush(Color.FromRgb(139, 140, 142));
    public static Brush DefaultPointStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(139, 140, 142));
    public static Brush DefaultPointGroupFillBrush { get; } = new SolidColorBrush(Color.FromRgb(114, 88, 72));
    public static Brush DefaultPointGroupStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(114, 88, 72));
    public static int DefaultPointZIndex { get; } = 3;
    public static double DefaultPointStrokeThickness { get; } = 1d;
    public static double DefualtPointDiameter { get; } = 4d;

    public static Brush DefaultBindPointFillBrush { get; } = Brushes.DarkViolet;
    public static Brush DefaultBindPointStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
    public static int DefaultBindPointZIndex { get; } = 3;
    public static double DefaultBindPointStrokeThickness { get; } = 1d;
    public static double DefualtBindPointDiameter { get; } = 5d;


    public static double MinCoordinate { get; } = -500;
    public static double MaxCoordinate { get; } = 500;    
}
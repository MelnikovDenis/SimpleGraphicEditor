using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.Static;
public static class DefaultValues
{

    // ŒÕ—“¿Õ“€ ‘Œ ”—¿
    public static Brush FocusBrush { get; } = new SolidColorBrush(Color.FromRgb(221, 156, 87));
    public static Brush SelectPointBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
    // ŒÕ—“¿Õ“€ Œ—≈…  ŒŒ–ƒ»Õ¿“
    public static Brush XAxisBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));
    public static Brush YAxisBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
    public static Brush ZAxisBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 255));
    public static double AxisLineThickness { get; } = 1.5d;
    // ŒÕ—“¿Õ“€ À»Õ»…
    public static Brush DefaultLineBrush { get; } = new SolidColorBrush(Color.FromRgb(47, 48, 52));
    public static double DefaultLineThickness { get; } = 4d;
    // ŒÕ—“¿Õ“€ “Œ◊≈ 
    public static Brush DefaultPointBrush { get; } = new SolidColorBrush(Color.FromRgb(139, 140, 142));
    public static Brush DefaultPointGroupingBrush { get; } = new SolidColorBrush(Color.FromRgb(114, 88, 72));
    public static int DefaultPointZIndex { get; } = 1;
    public static double DefaultPointStrokeThickness { get; } = 2d;
    public static double DefualtPointDiameter { get; } = 13d;
    // ŒÕ—“¿Õ“€ √–”œœ
    public static Brush DefaultBindPointBrush { get; } = Brushes.Transparent;
    public static Brush DefaultBindPointStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
    public static int DefaultBindPointZIndex { get; } = 2;
    public static double DefaultBindPointStrokeThickness { get; } = 2d;
    public static double DefualtBindPointDiameter { get; } = 10d;
    // ŒÕ—“¿Õ“€  ŒŒ–ƒ»Õ¿“
    public static double MinCoordinate { get; } = -5000;
    public static double MaxCoordinate { get; } = 5000;
    
}
using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.Static;
public static class DefaultValues
{
    public static Brush FocusBrush { get; } = new SolidColorBrush(Color.FromRgb(221, 156, 87));
    
    public static Brush DefaultLineBrush { get; } = new SolidColorBrush(Color.FromRgb(47, 48, 52));
    public static double DefaultLineThickness { get; } = 4d;
    
    public static Brush DefaultPointBrush { get; } = new SolidColorBrush(Color.FromRgb(139, 140, 142));
    public static Brush DefaultPointGroupingBrush { get; } = new SolidColorBrush(Color.FromRgb(114, 88, 72));
    public static int DefaultPointZIndex { get; } = 1;
    public static double DefaultPointStrokeThickness { get; } = 2d;
    public static double DefualtPointDiameter { get; } = 13d;



    public static Brush DefaultBindPointBrush { get; } = Brushes.Transparent;
    public static Brush DefaultBindPointStrokeBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 0));
    public static int DefaultBindPointZIndex { get; } = 2;
    public static double DefaultBindPointStrokeThickness { get; } = 2d;
    public static double DefualtBindPointDiameter { get; } = 10d;
}
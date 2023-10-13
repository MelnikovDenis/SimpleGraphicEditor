using System.Windows.Media;

namespace SimpleGraphicEditor.ViewModels.Static;
public static class DefaultValues
{
      public static Brush FocusBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));

      public static Brush DefaultLineBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 0, 255));
      public static double DefaultLineThickness { get; } = 4d;

      public static Brush DefaultPointBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));      
      public static int DefaultPointZIndex { get; } = 1;
      public static double DefualutPointDiameter { get; } = 15d;
}
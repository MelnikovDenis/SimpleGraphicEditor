using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using SimpleGraphicEditor.Models.Static;
namespace SimpleGraphicEditor.Models.EventControllers;
public class FocusController
{
      public bool CanFocus { get; set; } = false;
      public Brush DefaultFillBrush { get; set; }
      public Brush DefaultStrokeBrush { get; set; }
      public Brush FocusFillBrush { get; set; }
      public Brush FocusStrokeBrush { get; set; }
      public FocusController(Brush defaultFillBrush,
            Brush defaultStrokeBrush,
            Brush focusFillBrush,
            Brush focusStrokeBrush)
      {
            DefaultFillBrush = defaultFillBrush;
            DefaultStrokeBrush = defaultStrokeBrush;
            FocusFillBrush = focusFillBrush;
            FocusStrokeBrush = focusStrokeBrush;
      }
      public void OnMouseEnter(object sender, MouseEventArgs eventArgs)
      {
            var focusable = sender as Shape;
            if(CanFocus && focusable != null)            
            {
                  focusable.Fill = FocusFillBrush;
                  focusable.Stroke = FocusStrokeBrush;
                  eventArgs.Handled = true;
            }            
      }
      public void OnMouseLeave(object sender, MouseEventArgs eventArgs)
      {
            var focusable = sender as Shape;            
            if(CanFocus && focusable != null)            
            {
                  focusable.Fill = DefaultFillBrush;
                  focusable.Stroke = DefaultStrokeBrush;
                  eventArgs.Handled = true;
            }
      }
}
using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public partial class SgeViewModel
{
    public PointFactory PointFactory { get; set; }
    private Dictionary<Ellipse, SgePoint> Points { get; } = new Dictionary<Ellipse, SgePoint>();    
    public void CreatePoint(Point position)
    {
        var point = PointFactory.CreatePoint(position);
        Points.Add(point.Item1, point.Item2);
        TargetCanvas.Children.Add(point.Item1);
    }
    public void RemovePoint(Ellipse ellipse)
    {
        ellipse.MouseMove -= PointFactory.DragController.OnMouseMove;
        ellipse.MouseLeftButtonDown -= PointFactory.DragController.OnMouseLeftButtonDown;
        ellipse.MouseLeftButtonUp -= PointFactory.DragController.OnMouseLeftButtonUp;
        ellipse.MouseEnter -= PointFactory.FocusController.OnMouseEnter;
        ellipse.MouseLeave -= PointFactory.FocusController.OnMouseLeave;

        var sgePoint = Points[ellipse];
        BindingOperations.ClearAllBindings(ellipse);
        Points.Remove(ellipse);
        TargetCanvas.Children.Remove(ellipse);
        var lines = from kvp in Lines where sgePoint.AttachedLines.Contains(kvp.Value) select kvp.Key;
        foreach (var line in lines)
            RemoveLine(line);
    }
    public void PointMove(object sender, Point delta)
    {
        var point = Points[(sender as Ellipse)!];
        LimitDelta(point, ref delta);
        point.Move(delta);
    }
    private void LimitDelta(SgePoint point, ref Point delta)
    {
        if (point.X + delta.X > TargetCanvas.ActualWidth || point.X + delta.X < 0d)
            delta.X = 0d;
        if (point.Y + delta.Y > TargetCanvas.ActualHeight || point.Y + delta.Y < 0d)
            delta.Y = 0d;
    }
}

using SimpleGraphicEditor.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public partial class SgeViewModel
{
    public LineFactory LineFactory { get; set; }
    private Dictionary<Line, SgeLine> Lines { get; } = new Dictionary<Line, SgeLine>();
    public Ellipse? EllipseBuffer { get; set; } = null;
    public void CreateLineFromBuffer(Ellipse endPoint)
    {
        if (EllipseBuffer == null)
            throw new NullReferenceException("Стартовая точка должна быть указана.");
        var point1 = Points[EllipseBuffer];
        var point2 = Points[endPoint];
        var line = LineFactory.CreateLine(point1, point2);
        Lines.Add(line.Item1, line.Item2);
        TargetCanvas.Children.Add(line.Item1);
        EllipseBuffer = null;
    }
    public void RemoveLine(Line line)
    {
        line.MouseEnter -= LineFactory.FocusController.OnMouseEnter;
        line.MouseLeave -= LineFactory.FocusController.OnMouseLeave;
        line.MouseMove -= PointFactory.DragController.OnMouseMove;
        line.MouseLeftButtonDown -= PointFactory.DragController.OnMouseLeftButtonDown;
        line.MouseLeftButtonUp -= PointFactory.DragController.OnMouseLeftButtonUp;

        var sgeLine = Lines[line];
        sgeLine.Point1.AttachedLines.Remove(sgeLine);
        sgeLine.Point2.AttachedLines.Remove(sgeLine);

        BindingOperations.ClearAllBindings(line);
        Lines.Remove(line);
        TargetCanvas.Children.Remove(line);
    }
    public void LineMove(object sender, Point delta)
    {
        var line = Lines[(sender as Line)!];
        LimitDelta(line.Point1, ref delta);
        LimitDelta(line.Point2, ref delta);
        line.Move(delta);
    }
}

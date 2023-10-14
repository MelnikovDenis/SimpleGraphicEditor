using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;

namespace SimpleGraphicEditor.ViewModels;

public class SgeViewModel
{
    private Dictionary<Line, SgeLine> Lines { get; } = new Dictionary<Line, SgeLine>();
    private Dictionary<Ellipse, SgePoint> Points { get; } = new Dictionary<Ellipse, SgePoint>();
    private Canvas TargetCanvas { get; set; }
    public PointViewModel PointViewModel { get; set; }
    public LineViewModel LineViewModel { get; set; }
    public SgeViewModel(Canvas targetCanvas)
    {
        TargetCanvas = targetCanvas;

        var pointDragController = new DragController(TargetCanvas, PointMove);
        var lineDragController = new DragController(TargetCanvas, LineMove); 

        var pointFocusController = new FocusController(DefaultValues.DefaultPointBrush,
                DefaultValues.DefaultPointBrush,
                DefaultValues.FocusBrush,
                DefaultValues.FocusBrush);
        var lineFocusController = new FocusController(DefaultValues.DefaultLineBrush,
                DefaultValues.DefaultLineBrush,
                DefaultValues.FocusBrush,
                DefaultValues.FocusBrush);

        PointViewModel = new PointViewModel(pointFocusController, pointDragController);
        LineViewModel = new LineViewModel(lineFocusController, lineDragController);
    }
    public Ellipse? EllipseBuffer { get; set; } = null;
    public void CreateLineFromBuffer(Ellipse endPoint) 
    {
        if (EllipseBuffer == null)
            throw new NullReferenceException("Стартовая точка должна быть указана.");
        var point1 = Points[EllipseBuffer];
        var point2 = Points[endPoint];
        var line = LineViewModel.CreateLine(point1, point2);
        Lines.Add(line.Item1, line.Item2);
        TargetCanvas.Children.Add(line.Item1);
        EllipseBuffer = null;
    }
    public void CreatePoint(Point position) 
    {
        var point = PointViewModel.CreatePoint(position);
        Points.Add(point.Item1, point.Item2);
        TargetCanvas.Children.Add(point.Item1);
    }
    public void RemovePoint(Ellipse ellipse) 
    {
        ellipse.MouseMove -= PointViewModel.DragController.OnMouseMove;
        ellipse.MouseLeftButtonDown -= PointViewModel.DragController.OnMouseLeftButtonDown;
        ellipse.MouseLeftButtonUp -= PointViewModel.DragController.OnMouseLeftButtonUp;
        ellipse.MouseEnter -= PointViewModel.FocusController.OnMouseEnter;
        ellipse.MouseLeave -= PointViewModel.FocusController.OnMouseLeave;
        var sgePoint = Points[ellipse];
        BindingOperations.ClearAllBindings(ellipse);
        Points.Remove(ellipse);
        TargetCanvas.Children.Remove(ellipse);

        var lines = from kvp in Lines where sgePoint.AttachedLines.Contains(kvp.Value) select kvp.Key;
        foreach (var line in lines) 
        {
            RemoveLine(line);
        }            
    }
    public void RemoveLine(Line line)
    {
        line.MouseEnter -= LineViewModel.FocusController.OnMouseEnter;
        line.MouseLeave -= LineViewModel.FocusController.OnMouseLeave;
        BindingOperations.ClearAllBindings(line);
        Lines.Remove(line);
        TargetCanvas.Children.Remove(line);
    }
    public void PointMove(object sender, Point delta) 
    {
        var point = Points[(sender as Ellipse)!];
        if (point.X + delta.X > TargetCanvas.ActualWidth || point.X + delta.X < 0d)
            delta.X = 0d;
        if (point.Y + delta.Y > TargetCanvas.ActualHeight || point.Y + delta.Y < 0d)
            delta.Y = 0d;
        point.Move(delta);
    }
    public void LineMove(object sender, Point delta) 
    {
        var line = Lines[(sender as Line)!];
        if (line.Point1.X + delta.X > TargetCanvas.ActualWidth || line.Point1.X + delta.X < 0d)
            delta.X = 0d;
        if (line.Point1.Y + delta.Y > TargetCanvas.ActualHeight || line.Point1.Y + delta.Y < 0d)
            delta.Y = 0d;
        if (line.Point2.X + delta.X > TargetCanvas.ActualWidth || line.Point2.X + delta.X < 0d)
            delta.X = 0d;
        if (line.Point2.Y + delta.Y > TargetCanvas.ActualHeight || line.Point2.Y + delta.Y < 0d)
            delta.Y = 0d;
        line.Move(delta);
    }
}
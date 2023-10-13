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

        var pointDragController = new DragController(TargetCanvas, (sender, newPosition) => 
        {
            var point = Points[(sender as Ellipse)!];
            point.Move(new Point(newPosition.X - point.X, newPosition.Y - point.Y));
        });
        var lineDragController = new DragController(TargetCanvas, (sender, newPosition) => 
        {
            var line = Lines[(sender as Line)!];
            line.Move(new Point(newPosition.X - (line.Point1.X + line.Point2.X) / 2, newPosition.Y - (line.Point1.Y + line.Point2.Y) / 2));
        });

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
}
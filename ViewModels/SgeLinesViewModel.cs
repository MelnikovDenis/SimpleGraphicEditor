using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public class SgeLinesViewModel
{
    private Canvas TargetCanvas { get; }
    private FocusController LineFocusController { get; } = new FocusController(null, DefaultValues.FocusBrush);
    public Dictionary<Line, SgeLine> Lines { get; } = new Dictionary<Line, SgeLine>();
    public Point3D? PointBuffer { get; private set; } = null;
    public bool CanFocus
    {
        get => LineFocusController.CanFocus;
        set => LineFocusController.CanFocus = value;
    }
    public SgeLinesViewModel(Canvas targetCanvas)
    {
        TargetCanvas = targetCanvas;
    }
    public bool SetPointBuffer(object sender, SgePointsViewModel pointsViewModel) 
    {
        if (sender is Ellipse ellipse && pointsViewModel.Points.ContainsKey(ellipse))
        {
            PointBuffer = pointsViewModel.Points[ellipse];
            return true;
        }
        return false;
    }
    public bool CreateLine(object sender, SgePointsViewModel pointsViewModel)
    {
        if (sender is Ellipse ellipse && 
            PointBuffer != null && 
            pointsViewModel.Points.ContainsKey(ellipse) && 
            PointBuffer != pointsViewModel.Points[ellipse])
        {            
            var myLine = new SgeLine(TargetCanvas, LineFocusController, PointBuffer, pointsViewModel.Points[ellipse]);
            var line = myLine.VisibleLine;
            Lines.Add(line, myLine);
            PointBuffer = null;
            return true;
        }
        return false;
    }
    public bool RemoveLine(object sender) 
    {
        if (sender is Line visibleLine && Lines.ContainsKey(visibleLine))
        {
            var line = Lines[visibleLine];
            line.Remove();
            Lines.Remove(visibleLine);
            return true;
        }
        return false;
    }
}

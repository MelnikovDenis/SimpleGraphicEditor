using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Linq;
using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.DTO;
using System.Diagnostics;

namespace SimpleGraphicEditor.ViewModels;

public class SgePointsViewModel
{    
    private Canvas TargetCanvas { get; }
    private Observer Observer { get; }
    private PositionDto PosDto { get;  }
    private FocusController PointFocusController { get; } = new FocusController(null, DefaultValues.FocusBrush);
    private SelectController PointSelectController { get; }
    public Point3D? PointBuffer { get; private set; } = null;
    public Dictionary<Ellipse, SgePoint> Points { get; } = new Dictionary<Ellipse, SgePoint>();
    public bool CanFocus
    {
        get => PointFocusController.CanFocus;
        set => PointFocusController.CanFocus = value;
    }
    public bool CanSelect
    {
        get => PointSelectController.CanSelect;
        set => PointSelectController.CanSelect = value;
    }
    public SgePointsViewModel(Canvas targetCanvas, Observer observer, PositionDto posDto)
    {
        TargetCanvas = targetCanvas;
        Observer = observer;
        PosDto = posDto;
        PointSelectController = new SelectController(DefaultValues.SelectPointFillBrush, DefaultValues.SelectPointFillBrush, SetPointBuffer, UnsetPointBuffer);      
    }
    public void CreatePoint(double x, double y, double z)
    {
        var point = new SgePoint(TargetCanvas, PointFocusController, PointSelectController, Observer, x, y, z, DefaultValues.MinCoordinate, DefaultValues.MaxCoordinate);
        var ellipse = point.VisibleEllipse;
        Points.Add(ellipse, point);
    }
    public void SetPointBuffer(object sender) 
    {
        if (sender is Ellipse ellipse && Points.ContainsKey(ellipse)) 
        {
            PointBuffer = Points[ellipse];
            PosDto.X = PointBuffer.RealX;
            PosDto.Y = PointBuffer.RealY;
            PosDto.Z = PointBuffer.RealZ;
            PosDto.IsValid = true;
        }
    }
    public void UnsetPointBuffer(object sender)
    {
        if (sender is Ellipse ellipse && Points.ContainsKey(ellipse))
        {
            if(PointBuffer == Points[ellipse])
                PointBuffer = null;
            PosDto.X = 0d;
            PosDto.Y = 0d;
            PosDto.Z = 0d;
            PosDto.IsValid = false;
        }
    }
    public void TransferPoint(double x, double y, double z) 
    {
        PointBuffer?.SetCoordinates(x, y, z);
    }
    public bool RemovePoint(object sender, SgeLinesViewModel linesViewModel) 
    {
        if(sender is Ellipse ellipse && Points.ContainsKey(ellipse))
        {
            var point = Points[ellipse];
            var lines = from kvp in linesViewModel.Lines where point.AttachedLines.Contains(kvp.Value) select kvp.Key;
            foreach(var line in lines)
                linesViewModel.RemoveLine(line);
            point.Remove();
            Points.Remove(ellipse);
            return true;
        }
        return false;
    }

}

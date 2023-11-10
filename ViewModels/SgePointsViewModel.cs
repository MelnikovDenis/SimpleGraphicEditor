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
    public SgePoint? PointBuffer { get; private set; } = null;
    public SgeGroupViewModel GroupViewModel { get; private set; }
    public Dictionary<Ellipse, SgePoint> Points { get; } = new Dictionary<Ellipse, SgePoint>();
    public bool CanFocus
    {
        get => SgePoint.CanFocus;
        set => SgePoint.CanFocus = value;
    }
    public bool CanSelect
    {
        get => SgePoint.CanSelect;
        set => SgePoint.CanSelect = value;
    }
    public bool CanTransfer { get; set; } = false;
    public SgePointsViewModel(Canvas targetCanvas, Observer observer, PositionDto posDto)
    {
        TargetCanvas = targetCanvas;
        Observer = observer;
        PosDto = posDto;
        GroupViewModel = new SgeGroupViewModel(TargetCanvas, Observer, posDto);
    }
    public SgePoint CreatePoint(double x, double y, double z)
    {
        var point = new SgePoint(TargetCanvas, Observer, SetPointBuffer, UnsetPointBuffer, x, y, z, DefaultValues.MinCoordinate, DefaultValues.MaxCoordinate);
        var ellipse = point.VisibleEllipse;
        Points.Add(ellipse, point);
        return point;
    }
    private void SetPointBuffer(SgePoint sender) 
    {
        if (CanTransfer && Points.ContainsKey(sender.VisibleEllipse)) 
        {
            if(PointBuffer != null)
                PointBuffer.Unselect();
            PointBuffer = sender;
            PosDto.X = PointBuffer.RealX;
            PosDto.Y = PointBuffer.RealY;
            PosDto.Z = PointBuffer.RealZ;
            PosDto.IsValid = true;
        }
        else if(GroupViewModel.CanGrouping)
        {
            GroupViewModel.AddToGroup(sender);
        }
    }
    private void UnsetPointBuffer(SgePoint sender)
    {
        if (Points.ContainsKey(sender.VisibleEllipse) && PointBuffer == sender)
        {
            PointBuffer = null;
            PosDto.IsValid = false;
        }
        else if(GroupViewModel.CanGrouping)
        {
            GroupViewModel.DeleteFromGroup(sender.VisibleEllipse);
        }
    }
    
    public void UnsetPointBuffer()
    {
        PointBuffer?.Unselect();
        PointBuffer = null;
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
            GroupViewModel.Group.Remove(ellipse);
            Points.Remove(ellipse);
            return true;
        }
        return false;
    }   
}

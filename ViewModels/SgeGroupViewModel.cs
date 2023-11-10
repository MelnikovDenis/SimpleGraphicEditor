using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.DTO;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public class SgeGroupViewModel
{    
    public bool CanGrouping { get; set; } = false;
    private Canvas TargetCanvas { get; set; }
    private Observer Observer { get; set; }
    public Dictionary<Ellipse, SgePoint> Group { get; } = new Dictionary<Ellipse, SgePoint>();
    public BindPoint BindPoint { get; }
    public SgeGroupViewModel(Canvas targetCanvas, Observer observer, PositionDto posDto)
    {
        TargetCanvas = targetCanvas;
        Observer = observer;
        BindPoint = new BindPoint(TargetCanvas, Observer, 0d, 0d, 0d, DefaultValues.MinCoordinate, DefaultValues.MaxCoordinate);
    }
    public void AddToGroup(SgePoint point)
    {
        if(!Group.ContainsKey(point.VisibleEllipse))
            Group.Add(point.VisibleEllipse, point);
    }
    public void MirrorXGroup()
    {
        foreach(var point in Group.Values)
        {
            point.Move(-BindPoint.RealX, -BindPoint.RealY, -BindPoint.RealZ);
            point.MirrorX();
            point.Move(BindPoint.RealX, BindPoint.RealY, BindPoint.RealZ);
        }            
    }
    public void MirrorYGroup()
    {
        foreach(var point in Group.Values)
        {
            point.Move(-BindPoint.RealX, -BindPoint.RealY, -BindPoint.RealZ);
            point.MirrorY();
            point.Move(BindPoint.RealX, BindPoint.RealY, BindPoint.RealZ);
        }           
    }
    public void MirrorZGroup()
    {
        foreach(var point in Group.Values)
        {
            point.Move(-BindPoint.RealX, -BindPoint.RealY, -BindPoint.RealZ);
            point.MirrorZ();
            point.Move(BindPoint.RealX, BindPoint.RealY, BindPoint.RealZ);
        }            
    }
    public void RotateGroup(double sinX, double cosX, double sinY, double cosY, double sinZ, double cosZ)
    {
        foreach(var point in Group.Values)
        {
            point.Move(-BindPoint.RealX, -BindPoint.RealY, -BindPoint.RealZ);
            point.Rotate(sinX, cosX, sinY, cosY, sinZ, cosZ);
            point.Move(BindPoint.RealX, BindPoint.RealY, BindPoint.RealZ);
        }
    }
    public void DeleteFromGroup(Ellipse visibleEllipse)
    {
        if(Group.ContainsKey(visibleEllipse))
            Group.Remove(visibleEllipse);
    }
    public void TransferGroup(double deltaX, double deltaY, double deltaZ)
    {
        foreach(var point in Group.Values)
        {
            point.Move(deltaX, deltaY, deltaZ);
        }
    }
    public void TransferBindPoint(double x, double y, double z)
    {
        BindPoint.SetCoordinates(x, y, z);
    }
    public void SetInvisible()
    {
        BindPoint.SetInvisible();
        foreach(var point in Group.Values)
        {
            point.Unselect();
        }
    }
    public void SetVisible()
    {
        BindPoint.SetVisible();
        foreach(var point in Group.Values)
        {
            point.Select();
        }
    }
}

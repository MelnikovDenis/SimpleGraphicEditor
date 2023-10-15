using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SimpleGraphicEditor.Models;

public class SgePointGroup : IMoveable
{   
    public SgePoint Max { get; } = new SgePoint(new Point(double.MinValue, double.MinValue));
    public SgePoint Min { get; } = new SgePoint(new Point(double.MaxValue, double.MaxValue));
    public SgePoint BindPoint { get; } = new SgePoint(new Point((double.MinValue - double.MaxValue) / 2d, (double.MinValue - double.MaxValue) / 2d));
    public List<SgePoint> SelectedGroup { get; } = new List<SgePoint>();
    public SgePointGroup() { }
    public bool Add(SgePoint newPoint) 
    {
        if (!SelectedGroup.Contains(newPoint)) 
        {
            SelectedGroup.Add(newPoint);
            SetControlPoints(newPoint);
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool Remove(SgePoint oldPoint) 
    {
        if (SelectedGroup.Contains(oldPoint))
        {
            SelectedGroup.Remove(oldPoint);
            ResetControlPoints(oldPoint);
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Move(Point delta)
    {
        foreach(var point in SelectedGroup) 
            point.Move(delta);
        BindPoint.Move(delta);
    }
    private void SetControlPoints(SgePoint pointToAdd)
    {
        if (pointToAdd.X > Max.X)
            Max!.X = pointToAdd.X;
        if (pointToAdd.Y > Max.Y)
            Max!.Y = pointToAdd.Y;
        if (pointToAdd.X < Min.X)
            Min!.X = pointToAdd.X;
        if (pointToAdd.Y < Min.Y)
            Min!.Y = pointToAdd.Y;
        SetBindPointToCenter();
    }
    private void ResetControlPoints(SgePoint pointToRemove)
    {
        if (pointToRemove.X == Max.X)
            Max!.X = SelectedGroup.Max(p => p.X);
        if (pointToRemove.Y == Max.Y)
            Max!.Y = SelectedGroup.Max(p => p.Y);
        if (pointToRemove.X == Min.X)
            Min!.X = SelectedGroup.Min(p => p.X);
        if (pointToRemove.Y == Min.Y)
            Min!.Y = SelectedGroup.Min(p => p.Y);
        SetBindPointToCenter();
    }
    private void ResetControlPoints()
    {
        Max!.X = SelectedGroup.Max(p => p.X);
        Max!.Y = SelectedGroup.Max(p => p.Y);
        Min!.X = SelectedGroup.Min(p => p.X);
        Min!.Y = SelectedGroup.Min(p => p.Y);
        SetBindPointToCenter();
    }
    private void SetBindPointToCenter()
    {
        BindPoint.X = Min.X + (Max.X - Min.X) / 2d;
        BindPoint.Y = Min.Y + (Max.Y - Min.Y) / 2d;
    }
}
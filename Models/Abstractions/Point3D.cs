using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleGraphicEditor.Models.Abstractions;

public class Point3D : INotifyPropertyChanged
{
    public LinkedList<Line3D> AttachedLines { get; protected set; } = new LinkedList<Line3D>();
    protected Observer Observer { get; set; }
    private double MinCoordinate { get; set; }
    private double MaxCoordinate { get; set; }
    protected double realX = 0d;
    protected double realY = 0d;
    protected double realZ = 0d;
    protected double visibleX = 0d;
    protected double visibleY = 0d;
    public double RealX
    {
        get => realX;
        protected set
        {
            realX = Limit(value);
            OnPropertyChanged();
        }
    }
    public double RealY
    {
        get => realY;
        protected set
        {
            realY = Limit(value);
            OnPropertyChanged();
        }
    }
    public double RealZ
    {
        get => realZ;
        protected set
        {
            realZ = Limit(value);
            OnPropertyChanged();
        }
    }
    public double VisibleX
    {
        get => visibleX;
        protected set
        {
            visibleX = value;
            OnPropertyChanged();
        }
    }
    public double VisibleY
    {
        get => visibleY;
        protected set
        {
            visibleY = value;
            OnPropertyChanged();
        }
    }
    public Point3D(double realX, double realY, double realZ, Observer observer, double minCoordinate = double.MinValue, double maxCoordinate = double.MaxValue)
    {
        MinCoordinate = minCoordinate;
        MaxCoordinate = maxCoordinate;
        RealX = realX;
        RealY = realY;
        RealZ = realZ;
        Observer = observer;
        Observer.OnMoveEvent += ObserverMoveHandler;
        Observer.OnRotateEvent += Project;
        Project();
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    public void Move(double deltaX, double deltaY, double deltaZ)
    {
        RealX += deltaX;
        RealY += deltaY;
        RealZ += deltaZ;
        Project();
    }
    public void SetCoordinates(double newX, double newY, double newZ)
    {
        RealX = newX;
        RealY = newY;
        RealZ = newZ;
        Project();
    }
    public void Rotate(double sinX, double cosX, double sinY, double cosY, double sinZ, double cosZ)
    {
        RealX = cosZ * (RealX * cosY + sinY * (RealY * sinX + RealZ * cosX)) - sinZ * (RealY * cosX - RealZ * sinX);
        RealY = sinZ * (RealX * cosY + sinY * (RealY * sinX + RealZ * cosX)) + cosZ * (RealY * cosX - RealZ * sinX);
        RealZ = cosY * (RealY * sinX + RealZ * cosX) - RealX * sinY; 
        Project();
    }
    public void MirrorX()
    {
        RealY = -RealY;
        RealZ = -RealZ;
        Project();
    }
    public void MirrorY()
    {
        RealX = -RealX;
        RealZ = -RealZ;
        Project();
    }
    public void MirrorZ()
    {
        RealX = -RealX;
        RealY = -RealY;
        Project();        
    }
    protected virtual void Project()
    {
        var homogeneousCoordinate = RealX * Observer.SinY * Observer.CosX / Observer.ViewPointZ
            - RealZ * Observer.CosY * Observer.CosX / Observer.ViewPointZ
            - RealY * Observer.SinX / Observer.ViewPointZ + 1d;
        VisibleX = (RealX * Observer.CosY + RealZ * Observer.SinY) / homogeneousCoordinate + Observer.X;
        VisibleY = (RealX * Observer.SinY * Observer.SinX - RealZ * Observer.SinX * Observer.CosY + RealY * Observer.CosX)
            / homogeneousCoordinate + Observer.Y;
    }
    protected void ObserverMoveHandler(double deltaX, double deltaY)
    {
        VisibleX += deltaX;
        VisibleY += deltaY;
    }
    protected double Limit(double value)
    {
        if (value > MaxCoordinate)
            return MaxCoordinate;
        else if (value < MinCoordinate)
            return MinCoordinate;
        else
            return value;
    }
}

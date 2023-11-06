using System;
using System.Windows.Controls;
using SimpleGraphicEditor.ViewModels.EventControllers;

namespace SimpleGraphicEditor.Models;
public class Observer
{    
    public event Action<double, double>? OnMoveEvent;
    public event Action? OnRotateEvent;
    private double x = 200;
    private double y = 200;

    public virtual double X { get => x; set => x = value; }

    public virtual double Y { get => y; set => y = value; }
    public double RotateSpeed { get; private set; } = 0.001d;
    private double angleX = 0d;
    private double angleY = 0d;   
    public double AngleX
    {
        get
        {
            return angleX;
        }
        private set
        {
            angleX = value % (2d * Math.PI);
            sinX = Math.Sin(angleX);
            cosX = Math.Cos(angleX);
        }
    }
    public double AngleY
    {
        get
        {
            return angleY;
        }
        private set
        {
            angleY = value % (2d * Math.PI);
            sinY = Math.Sin(angleY);
            cosY = Math.Cos(angleY);
        }
    }
    private double sinX;
    private double sinY;
    private double cosX;
    private double cosY;
    public double SinX { get => sinX; }
    public double SinY { get => sinY; }
    public double CosX { get => cosX; }
    public double CosY { get => cosY; }
    public double ViewPointZ { get; set; } = 100000d;
    public Observer(Canvas targetCanvas, DragController dragController)
    {
        AngleX = 0.3d;
        AngleY = 0.3d;
        targetCanvas.MouseMove += dragController.MouseMoveHandler;
        targetCanvas.MouseLeftButtonDown += dragController.MouseLeftButtonDownHandler;
        targetCanvas.MouseLeftButtonUp += dragController.MouseLeftButtonDownHandler;
    }
    public void Move(double deltaX, double deltaY)
    {
        double oldX = X;
        double oldY = Y;
        X += deltaX;
        Y += deltaY;
        OnMoveEvent?.Invoke(X - oldX, Y - oldY);
    }
    public void Rotate(double deltaAngleX, double deltaAngleY) 
    {
        AngleX += deltaAngleX;
        AngleY += deltaAngleY;
        OnRotateEvent?.Invoke();
    }
}
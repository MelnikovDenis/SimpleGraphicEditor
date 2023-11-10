using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Diagnostics;
using SimpleGraphicEditor.ViewModels.EventControllers;
using System.Windows.Media;

namespace SimpleGraphicEditor.Models;
public class Observer
{    
    private static double MinViewPointZ { get; set; } = 501d;
    private static double MaxViewPointZ { get; set; } = 2004d; 
    private static double MinScale { get; set; } = 1d;
    private static double MaxScale { get; set; } = 4d;
    private static double Ratio { get => MaxViewPointZ / MaxScale; }
    private Canvas TargetCanvas {get; set; }
    private ScaleTransform ScaleTransf {get; set; }
    private double ScaleCoef 
    {
        set 
        {
            ScaleTransf.ScaleX = Limit(value, MinScale, MaxScale);
            ScaleTransf.ScaleY = ScaleTransf.ScaleX;
        }
        get
        {
            return ScaleTransf.ScaleX;
        }
    } 
    public double viewPointZ;
    public double ViewPointZ 
    {
        get => viewPointZ; 
        set => viewPointZ = Limit(value, MinViewPointZ, MaxViewPointZ);
    }
    public event Action<double, double>? OnMoveEvent;
    public event Action? OnRotateEvent;
    private double x = 0;
    private double y = 0;

    public virtual double X { get => x; set => x = value; }

    public virtual double Y { get => y; set => y = value; }    
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
    public Observer(Canvas targetCanvas, DragController dragController)
    {
        AngleX = Math.PI;
        AngleY = 0d;
        TargetCanvas = targetCanvas;
        targetCanvas.MouseMove += dragController.MouseMoveHandler;
        targetCanvas.MouseLeftButtonDown += dragController.MouseLeftButtonDownHandler;
        targetCanvas.MouseLeftButtonUp += dragController.MouseLeftButtonDownHandler;
        targetCanvas.MouseWheel += OnMouseWheel;        
        targetCanvas.SizeChanged += OnSizeChanged;

        Move(TargetCanvas.ActualWidth / 2d, TargetCanvas.ActualHeight / 2d);
        ScaleTransf = new ScaleTransform(MaxScale / 2d, MaxScale / 2d, TargetCanvas.ActualWidth / 2d, TargetCanvas.ActualHeight / 2d);
        targetCanvas.RenderTransform = ScaleTransf;
        ViewPointZ = MaxViewPointZ / 2d;        
       
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
    private void OnMouseWheel(object sender, MouseWheelEventArgs eventArgs)
    {
        ScaleTransf.CenterX = TargetCanvas.ActualWidth / 2d;
        ScaleTransf.CenterY = TargetCanvas.ActualHeight / 2d;
        ScaleCoef += eventArgs.Delta / Ratio;
        ViewPointZ += eventArgs.Delta;
        OnRotateEvent?.Invoke();
    }
    private void OnSizeChanged(object sender, SizeChangedEventArgs eventArgs)
    {
        ScaleTransf.CenterX = TargetCanvas.ActualWidth / 2d;
        ScaleTransf.CenterY = TargetCanvas.ActualHeight / 2d;
        Move((eventArgs.NewSize.Width - eventArgs.PreviousSize.Width) / 2d, (eventArgs.NewSize.Height - eventArgs.PreviousSize.Height) / 2d);
    }
    private static double Limit(double value, double min, double max)
    {
        if (value > max)
            return max;
        else if (value < min)
            return min;
        else
            return value;
    }
}
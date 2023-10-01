using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Shapes;
using SimpleGraphicEditor.Models.Abstractions;
using System.Windows.Input;
using System.Diagnostics;
using System.Windows;

namespace SimpleGraphicEditor.Models;

public class SgePoint : INotifyPropertyChanged, IDrawable, IMoveable
{
    public static Brush DefaultBrush { get; } = new SolidColorBrush(Color.FromRgb(255, 0, 0));
    public static Brush FocusBrush { get; } = new SolidColorBrush(Color.FromRgb(0, 255, 0));
    public static double Diameter { get; } = 10d;
    public Ellipse VisiblePoint { get; private set; } = null!;
    public Canvas TargetCanvas { get; private set; } = null!;
    
    public Brush VisibleBrush 
    {
        get 
        {
            return VisiblePoint.Fill;
        } 
        set 
        {
            VisiblePoint.Fill = value;
            OnPropertyChanged(nameof(VisibleBrush));
        }        
    }
    private double x;
    public double X
    {
        get => x;
        set
        {
            x = value;
            VisiblePoint.SetValue(Canvas.LeftProperty, x - Diameter / 2d);
            OnPropertyChanged(nameof(X));
        }
    }
    private double y;
    public double Y 
    { 
        get => y;
        set 
        { 
            y = value;
            VisiblePoint.SetValue(Canvas.TopProperty, y - Diameter / 2d);
            OnPropertyChanged(nameof(Y));
        }
    }

    private bool IsDragging { get; set; } = false;

    public SgePoint(double x, double y, Canvas canvas)
    {
        VisiblePoint = new Ellipse()
        {
            Fill = DefaultBrush,
            Width = Diameter,
            Height = Diameter
        };
        X = x; 
        Y = y;
        TargetCanvas = canvas;
        //VisiblePoint.MouseLeftButtonDown += OnMouseLeftButtonDown;
        //VisiblePoint.MouseLeftButtonUp += OnMouseLeftButtonUp;
        //VisiblePoint.MouseMove += OnMouseMove;

    }

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    public void Draw()
    {
        TargetCanvas.Children.Add(VisiblePoint);
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs) 
    {
        var draggableControl = sender as Shape;
        IsDragging = true;
        draggableControl?.CaptureMouse();
        eventArgs.Handled = true;
    }
    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs eventArgs)
    {
        IsDragging = false;
        var draggable = sender as Shape;
        draggable?.ReleaseMouseCapture();
        eventArgs.Handled = true;
    }
    private void OnMouseMove(object sender, MouseEventArgs eventArgs)
    {
        var draggableControl = sender as Shape;
        if (IsDragging && draggableControl != null)
        {
            Point currentPosition = eventArgs.GetPosition(TargetCanvas);
            X = currentPosition.X;
            Y = currentPosition.Y;
            eventArgs.Handled = true;
        }
    }
}
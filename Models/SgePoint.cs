using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models;

public class SgePoint : VisiblePoint3D
{
    public static bool CanFocus { get; set; } = false;
    public static bool CanSelect { get; set; } = false;
    public bool IsSelected { get; private set; } = false;
    public event Action<SgePoint> OnSelect;
    public event Action<SgePoint> OnUnselect;
    public SgePoint(Canvas targetCanvas, Observer observer, Action<SgePoint> onSelect, Action<SgePoint> onUnselect, double realX, double realY, double realZ, double minCoordinate, double maxCoordinate) 
        : base(realX, realY, realZ, minCoordinate, maxCoordinate, observer, targetCanvas)
    {
        OnSelect = onSelect;
        OnUnselect = onUnselect;
        Canvas.SetZIndex(VisibleEllipse, DefaultValues.DefaultPointZIndex);
        VisibleEllipse.MouseEnter += MouseEnterHandler;
        VisibleEllipse.MouseLeave += OnMouseLeaveHandler;
        VisibleEllipse.MouseLeftButtonDown += LeftMouseClickHandler;
        VisibleEllipse.MouseRightButtonDown += RightMouseClickHandler;
    }
    protected override Ellipse CreateVisibleEllipse()
    {
        var ellipse = new Ellipse()
        {
                Fill = DefaultValues.DefaultPointFillBrush,
                Stroke = DefaultValues.DefaultPointStrokeBrush,
                StrokeThickness = DefaultValues.DefaultPointStrokeThickness,
                Width = DefaultValues.DefualtPointDiameter,
                Height = DefaultValues.DefualtPointDiameter 
        };                          
        return ellipse;
    }
    public void Remove()
    {             
        VisibleEllipse.MouseEnter -= MouseEnterHandler;
        VisibleEllipse.MouseLeave -= OnMouseLeaveHandler;
        VisibleEllipse.MouseLeftButtonDown -= LeftMouseClickHandler;
        VisibleEllipse.MouseRightButtonDown -= RightMouseClickHandler;
        VisibleEllipse.MouseLeftButtonUp -= MouseLeftButtonUpHandler; 
        BindingOperations.ClearAllBindings(VisibleEllipse);
        TargetCanvas.Children.Remove(VisibleEllipse);
        VisibleEllipse = null!;

        Observer.OnMoveEvent -= ObserverMoveHandler;
        Observer.OnRotateEvent -= Project;
        Observer = null!;
    
        AttachedLines = null!;        
    }
    private void MouseEnterHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus)
        {
            VisibleEllipse.Stroke = DefaultValues.FocusPointStrokeBrush;
            eventArgs.Handled = true;
        }
    }
    private void OnMouseLeaveHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanFocus)
        {
            if(IsSelected)
                VisibleEllipse.Stroke = DefaultValues.SelectPointStrokeBrush;
            else
                VisibleEllipse.Stroke = DefaultValues.DefaultPointStrokeBrush;
            eventArgs.Handled = true;
        }
    }    
    private void LeftMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if(CanSelect) 
        {
            IsSelected = true;
            VisibleEllipse.Fill = DefaultValues.SelectPointFillBrush;
            OnSelect(this);
            eventArgs.Handled = true;
        }
    }
    public void MouseLeftButtonUpHandler(object sender, MouseButtonEventArgs eventArgs)
    {
        if (CanSelect)
        {
            eventArgs.Handled = true;
        }
    } 
    private void RightMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanSelect)
        {
            IsSelected = false;
            VisibleEllipse.Fill = DefaultValues.DefaultPointFillBrush;
            OnUnselect(this);
            eventArgs.Handled = true;
        }
    }
    public void Select()
    {
        IsSelected = true;
        VisibleEllipse.Stroke = DefaultValues.SelectPointStrokeBrush;
        VisibleEllipse.Fill = DefaultValues.SelectPointFillBrush;
    }
    public void Unselect()
    {
        IsSelected = false;
        VisibleEllipse.Fill = DefaultValues.DefaultPointFillBrush;
        VisibleEllipse.Stroke = DefaultValues.DefaultPointStrokeBrush;
    }
}

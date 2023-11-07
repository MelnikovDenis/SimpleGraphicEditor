using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public class ObserverViewModel
{
    private Canvas TargetCanvas { get; }
    public DragController ObserverDragController { get; } = null!;
    public Observer Observer { get; } = null!;
    public bool CanDragging 
    {
        get => ObserverDragController.CanDragging;
        set => ObserverDragController.CanDragging = value;
    }
    public ObserverViewModel(Canvas targetCanvas)
    {
        TargetCanvas = targetCanvas;
        ObserverDragController = new DragController(TargetCanvas, ObserverRotate);
        Observer = new Observer(TargetCanvas, ObserverDragController);
    }
    private void ObserverMove(object sender, Point delta)
    {
        var canvas = sender as Canvas;
        if(ReferenceEquals(TargetCanvas, canvas))
        {                  
             Observer.Move(delta.X, delta.Y);
        }
    }
    private void ObserverRotate(object sender, Point delta)
    {
        var canvas = sender as Canvas;
        if (ReferenceEquals(TargetCanvas, canvas))
        {
            Observer.Rotate(delta.Y * Observer.RotateSpeed, delta.X * -Observer.RotateSpeed);
        }
    }
}

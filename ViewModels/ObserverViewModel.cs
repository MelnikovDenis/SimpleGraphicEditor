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
    private static double RotateSpeed { get; set; } = 0.001d;
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
        ObserverDragController = new DragController(TargetCanvas, OnDrag);
        Observer = new Observer(TargetCanvas, ObserverDragController);
    }
    private void OnDrag(object sender, Point delta)
    {
        Observer.Rotate(delta.Y * RotateSpeed, -delta.X * RotateSpeed);
    }
}

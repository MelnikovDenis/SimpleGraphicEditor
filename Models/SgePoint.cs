using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models;

public class SgePoint : VisiblePoint3D
{
    private FocusController FocusController { get; set; }
    private SelectController SelectController { get; set; }
    public SgePoint(Canvas targetCanvas, FocusController focusController, SelectController selectController, Observer observer, double realX, double realY, double realZ, double minCoordinate, double maxCoordinate) 
        : base(realX, realY, realZ, minCoordinate, maxCoordinate, observer, targetCanvas)
    {
        SelectController = selectController;
        FocusController = focusController;
        Canvas.SetZIndex(VisibleEllipse, DefaultValues.DefaultPointZIndex);
        VisibleEllipse.MouseEnter += FocusController.MouseEnterHandler;
        VisibleEllipse.MouseLeave += FocusController.OnMouseLeaveHandler;
        VisibleEllipse.MouseLeftButtonDown += SelectController.LeftMouseClickHandler;
        VisibleEllipse.MouseRightButtonDown += SelectController.RightMouseClickHandler;
    }
    protected override Ellipse CreateVisibleEllipse()
    {
        var ellipse = new Ellipse()
        {
                Fill = DefaultValues.DefaultPointBrush,
                Stroke = DefaultValues.DefaultPointBrush,
                StrokeThickness = DefaultValues.DefaultPointStrokeThickness,
                Width = DefaultValues.DefualtPointDiameter,
                Height = DefaultValues.DefualtPointDiameter 
        };                          
        return ellipse;
    }
    public void Remove()
    {             
        VisibleEllipse.MouseEnter -= FocusController.MouseEnterHandler;
        VisibleEllipse.MouseLeave -= FocusController.OnMouseLeaveHandler;
        VisibleEllipse.MouseLeftButtonDown -= SelectController.LeftMouseClickHandler;
        VisibleEllipse.MouseRightButtonDown -= SelectController.RightMouseClickHandler;
        Observer.OnMoveEvent -= ObserverMoveHandler;
        Observer.OnRotateEvent -= Project;
        TargetCanvas.Children.Remove(VisibleEllipse);
        BindingOperations.ClearAllBindings(VisibleEllipse);
        VisibleEllipse = null!;
        Observer = null!;
        AttachedLines = null!;
        FocusController = null!;
    }
}

using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows.Shapes;
using System.Windows.Controls;
using SimpleGraphicEditor.Models.Abstractions;
using System.Windows.Data;

namespace SimpleGraphicEditor.Models;

public class SgeLine : VisibleLine3D
{
    private FocusController FocusController { get; set; }
    public SgeLine(Canvas targetCanvas, FocusController focusController, Point3D firstPoint, Point3D secondPoint) : base(targetCanvas, firstPoint, secondPoint)
    {
        FocusController = focusController;
        VisibleLine.MouseEnter += FocusController.MouseEnterHandler;
        VisibleLine.MouseLeave += FocusController.OnMouseLeaveHandler;
    }
    protected override Line CreateVisibleLine()
    {
        var line = new Line()
        {
            Stroke = DefaultValues.DefaultLineBrush,
            StrokeThickness = DefaultValues.DefaultLineThickness,
        };      
        return line;
    }
    public void Remove()
    {
        FirstPoint.AttachedLines.Remove(this);
        SecondPoint.AttachedLines.Remove(this);
        BindingOperations.ClearAllBindings(VisibleLine);
        VisibleLine.MouseEnter -= FocusController.MouseEnterHandler;
        VisibleLine.MouseLeave -= FocusController.OnMouseLeaveHandler;
        TargetCanvas.Children.Remove(VisibleLine);
        FirstPoint = null!;
        SecondPoint = null!;
        VisibleLine = null!;
        FocusController = null!;
    }
}
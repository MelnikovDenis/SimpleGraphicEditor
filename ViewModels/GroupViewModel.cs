
using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public partial class SgeViewModel
{
    public PointFactory BindPointFactory { get; set; }
    private Ellipse? BindPointEllipse { get; set; } = null;
    private SgePointGroup SgePointGroup { get; } = new SgePointGroup();


    public void AddToGroup(Ellipse ellipse)
    {
        ellipse.Fill = DefaultValues.DefaultPointGroupingBrush;
        PointFactory.FocusController.DefaultFillBrush = DefaultValues.DefaultPointGroupingBrush;
        PointFactory.FocusController.DefaultStrokeBrush = DefaultValues.DefaultPointGroupingBrush;
        var sgePoint = Points[ellipse];
        SgePointGroup.Add(sgePoint);
        if(BindPointEllipse == null) 
            CreateBindPointEllipse(SgePointGroup.BindPoint);
    }
    public void DeleteFromGroup(Ellipse ellipse)
    {
        ellipse.Fill = DefaultValues.DefaultPointBrush;
        PointFactory.FocusController.DefaultFillBrush = DefaultValues.DefaultPointBrush;
        PointFactory.FocusController.DefaultStrokeBrush = DefaultValues.DefaultPointBrush;
        var sgePoint = Points[ellipse];
        SgePointGroup.Remove(sgePoint);
    }
    private void CreateBindPointEllipse(SgePoint bindPoint) 
    {
        BindPointEllipse = BindPointFactory.CreateEllipse(bindPoint);
        TargetCanvas.Children.Add(BindPointEllipse);
    }
    public void BindPointMove(object sender, Point delta)
    {
        var group = SgePointGroup;
        LimitDelta(SgePointGroup.BindPoint, ref delta);
        foreach(var point in SgePointGroup.SelectedGroup)
            LimitDelta(point, ref delta);
        group.Move(delta);
    }
    
}

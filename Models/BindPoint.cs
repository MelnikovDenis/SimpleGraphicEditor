using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.Models;

public class BindPoint : VisiblePoint3D
{
    public BindPoint(Canvas targetCanvas, Observer observer, double realX, double realY, double realZ, double minCoordinate, double maxCoordinate) 
        : base(realX, realY, realZ, minCoordinate, maxCoordinate, observer, targetCanvas)
    {
        Canvas.SetZIndex(VisibleEllipse, DefaultValues.DefaultBindPointZIndex);
        SetInvisible();
    }
    protected override Ellipse CreateVisibleEllipse()
    {
        var ellipse = new Ellipse()
        {
                Fill = DefaultValues.DefaultBindPointFillBrush,
                Stroke = DefaultValues.DefaultBindPointStrokeBrush,
                StrokeThickness = DefaultValues.DefaultBindPointStrokeThickness,
                Width = DefaultValues.DefualtBindPointDiameter,
                Height = DefaultValues.DefualtBindPointDiameter 
        };                          
        return ellipse;
    }
    public void SetInvisible() => VisibleEllipse.Visibility = Visibility.Collapsed;
    public void SetVisible() => VisibleEllipse.Visibility = Visibility.Visible;
}

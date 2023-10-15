using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Diagnostics;

namespace SimpleGraphicEditor.ViewModels;

public partial class SgeViewModel
{  
    private Canvas TargetCanvas { get; set; }
    public SgeViewModel(Canvas targetCanvas)
    {
        TargetCanvas = targetCanvas;
        var pointDragController = new DragController(TargetCanvas, PointMove);
        var bindPointDragController = new DragController(TargetCanvas, BindPointMove);
        var lineDragController = new DragController(TargetCanvas, LineMove); 

        var pointFocusController = new FocusController(null, DefaultValues.FocusBrush);
        var lineFocusController = new FocusController(null, DefaultValues.FocusBrush);

        PointFactory = new PointFactory(pointFocusController, pointDragController);
        BindPointFactory = new PointFactory(pointFocusController, bindPointDragController,
            DefaultValues.DefaultBindPointBrush,
            DefaultValues.DefaultBindPointStrokeBrush,
            DefaultValues.DefaultBindPointZIndex,
            DefaultValues.DefaultBindPointStrokeThickness,
            DefaultValues.DefualtBindPointDiameter);
        LineFactory = new LineFactory(lineFocusController, lineDragController);
    }        
}
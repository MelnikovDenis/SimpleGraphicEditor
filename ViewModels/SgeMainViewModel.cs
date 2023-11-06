using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels.DTO;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public class SgeMainViewModel
{
    private Canvas TargetCanvas { get; set; }
    private SgeStatus Status { get; set; }
    private PositionDto PosDto { get; set; }
    public SgePointsViewModel PointsViewModel { get; private set; }
    public SgeLinesViewModel LinesViewModel { get; private set; }
    public ObserverViewModel ObserverViewModel { get; private set; }
    public SgeMainViewModel(Canvas targetCanvas, SgeStatus status, PositionDto posDto)
    {
        Status = status;
        PosDto = posDto;
        TargetCanvas = targetCanvas;
        
        ObserverViewModel = new ObserverViewModel(TargetCanvas);
        PointsViewModel = new SgePointsViewModel(TargetCanvas, ObserverViewModel.Observer, PosDto);
        LinesViewModel = new SgeLinesViewModel(TargetCanvas);

        var negativeX = new Point3D(-10000d, 0d, 0d, ObserverViewModel.Observer);
        var positiveX = new Point3D(10000d, 0d, 0d, ObserverViewModel.Observer);
        var negativeY = new Point3D(0, -10000d, 0d, ObserverViewModel.Observer);
        var positiveY = new Point3D(0, 10000d, 0d, ObserverViewModel.Observer);
        var negativeZ = new Point3D(0, 0d, -10000d, ObserverViewModel.Observer);
        var positiveZ = new Point3D(0, 0d, 10000d, ObserverViewModel.Observer);
        var xAxis = new CoordinateAxis(TargetCanvas, DefaultValues.XAxisBrush, negativeX, positiveX);
        var yAxis = new CoordinateAxis(TargetCanvas, DefaultValues.YAxisBrush, negativeY, positiveY);
        var zAxis = new CoordinateAxis(TargetCanvas, DefaultValues.ZAxisBrush, negativeZ, positiveZ);


        ObserverViewModel.CanDragging = true;
        PointsViewModel.CanFocus = false;
        PointsViewModel.CanSelect = false;
        LinesViewModel.CanFocus = false;
    }
    
}
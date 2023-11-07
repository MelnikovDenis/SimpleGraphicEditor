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

        var zero = new Point3D(0, 0, 0, ObserverViewModel.Observer);
        var positiveX = new Point3D(100d, 0d, 0d, ObserverViewModel.Observer);
        var positiveY = new Point3D(0, 100d, 0d, ObserverViewModel.Observer);
        var positiveZ = new Point3D(0, 0d, 100d, ObserverViewModel.Observer);
        var xPositiveAxis = new CoordinateAxis(TargetCanvas, DefaultValues.XAxisBrush, zero, positiveX);
        var yPositiveAxis = new CoordinateAxis(TargetCanvas, DefaultValues.YAxisBrush, zero, positiveY);
        var zPositiveAxis = new CoordinateAxis(TargetCanvas, DefaultValues.ZAxisBrush, zero, positiveZ);


        ObserverViewModel.CanDragging = true;
        PointsViewModel.CanFocus = false;
        PointsViewModel.CanSelect = false;
        LinesViewModel.CanFocus = false;
    }    
}
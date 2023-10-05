using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.Models.Static;
using SimpleGraphicEditor.Models.EventControllers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleGraphicEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private PointFactory PointFactory { get; set; }
    private LineFactory LineFactory { get; set; }
    private DragController DragController { get; set; }
    private FocusController PointFocusController { get; set; }
    private FocusController LineFocusController { get; set; }
    public enum Action 
    {
        SetSignlePoint,
        ChooseLineStartPoint,
        ChooseLineEndPoint,
        Drag,
        Delete
    }
    public Action CurrentAction { get; private set; } = Action.SetSignlePoint;
    public MainWindow()
    {
        InitializeComponent();

        DragController = new DragController(SgeCanvas);
        PointFocusController = new FocusController(
            DefaultValues.DefaultPointBrush, 
            DefaultValues.DefaultPointBrush,
            DefaultValues.FocusBrush,
            DefaultValues.FocusBrush,
            DefaultValues.FocusScaleCoefficient);
        LineFocusController = new FocusController(
            DefaultValues.DefaultLineBrush, 
            DefaultValues.DefaultLineBrush,
            DefaultValues.FocusBrush,
            DefaultValues.FocusBrush,
            DefaultValues.FocusScaleCoefficient);
        PointFactory = new PointFactory(
            SgeCanvas, 
            DragController, 
            PointFocusController);
        LineFactory = new LineFactory(
            SgeCanvas, 
            //DragController,
            LineFocusController);

        var p1 = PointFactory.CreateVisiblePoint(new Point(1, 1));
        var p2 = PointFactory.CreateVisiblePoint(new Point(2, 2));
        LineFactory.Buffer = p1;
        var l1 = LineFactory.CreateLineFromBuffer(p2);
        p1.SetCoordinates(new Point(400, 400));
        p2.SetCoordinates(new Point(200, 200));
    }
    private void PointButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.SetSignlePoint;

        DragController.CanDragging = false;

        PointFocusController.CanFocus = false;
        LineFocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.ChooseLineStartPoint;

        DragController.CanDragging = false;

        PointFocusController.CanFocus = true;
        LineFocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.Delete;

        DragController.CanDragging = false;

        PointFocusController.CanFocus = false;
        LineFocusController.CanFocus = true;

        eventArgs.Handled = true;
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.Drag;

        DragController.CanDragging = true;

        PointFocusController.CanFocus = true;
        LineFocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void OnCanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs) 
    {        
        switch (CurrentAction)
        {
            case Action.Drag:
                return;
            case Action.SetSignlePoint:
                Point cursorPosition = eventArgs.GetPosition(SgeCanvas);
                PointFactory.CreateVisiblePoint(cursorPosition);
                break;
            case Action.ChooseLineStartPoint:
                var ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null)
                {
                    LineFactory.Buffer = ellipse;
                    CurrentAction = Action.ChooseLineEndPoint;
                }         
                break;
            case Action.ChooseLineEndPoint:                
                ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null && ellipse != LineFactory.Buffer)
                {                    
                    LineFactory.CreateLineFromBuffer(ellipse);
                    CurrentAction = Action.ChooseLineStartPoint;
                }               
                break;
            case Action.Delete:
                var line = eventArgs.OriginalSource as Line;
                if(line != null)
                    LineFactory.Remove(line);
                ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null)
                    PointFactory.Remove(ellipse);
                break;
        }
        eventArgs.Handled = true;
    }   
    
}

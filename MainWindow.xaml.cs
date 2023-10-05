using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.Models.Static;
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
    public enum Action 
    {
        SetSignlePoint,
        ChooseLineStartPoint,
        ChooseLineEndPoint,
        Drag
    }
    public Action CurrentAction { get; private set; } = Action.SetSignlePoint;
    public MainWindow()
    {
        InitializeComponent();
        DragController = new DragController(SgeCanvas);
        PointFactory = new PointFactory(SgeCanvas, DragController);
        LineFactory = new LineFactory(SgeCanvas);

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
        eventArgs.Handled = true;
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.ChooseLineStartPoint;
        DragController.CanDragging = false;
        eventArgs.Handled = true;
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.Drag;
        DragController.CanDragging = true;
        eventArgs.Handled = true;
    }
    private void SgeCanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs) 
    {        
        switch (CurrentAction)
        {
            case Action.SetSignlePoint:
                Point cursorPosition = eventArgs.GetPosition(SgeCanvas);
                PointFactory.CreateVisiblePoint(cursorPosition);
                eventArgs.Handled = true;
                break;
            case Action.ChooseLineStartPoint:
                var ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null)
                {
                    LineFactory.Buffer = ellipse;
                    CurrentAction = Action.ChooseLineEndPoint;
                }
                eventArgs.Handled = true;          
                break;
            case Action.ChooseLineEndPoint:                
                ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null && ellipse != LineFactory.Buffer)
                {
                    LineFactory.CreateLineFromBuffer(ellipse);
                    CurrentAction = Action.ChooseLineStartPoint;
                }
                eventArgs.Handled = true;
                break;
        }
        
    }   
    
}

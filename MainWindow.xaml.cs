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
    private SgeStatus Status { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        Status = (SgeStatus)this.Resources["Status"];
        PointFactory = new PointFactory(
            SgeCanvas,
            new DragController(SgeCanvas), 
            new FocusController(DefaultValues.DefaultPointBrush, 
                DefaultValues.DefaultPointBrush,
                DefaultValues.FocusBrush,
                DefaultValues.FocusBrush)
        );
        LineFactory = new LineFactory(
            SgeCanvas, 
            //DragController,
            new FocusController(DefaultValues.DefaultLineBrush, 
                DefaultValues.DefaultLineBrush,
                DefaultValues.FocusBrush,
                DefaultValues.FocusBrush)
            );
    }
    private void PointButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.SetSignlePoint;

        PointFactory.DragController.CanDragging = false;

        PointFactory.FocusController.CanFocus = false;
        LineFactory.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;

        PointFactory.DragController.CanDragging = false;

        PointFactory.FocusController.CanFocus = true;
        LineFactory.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.Delete;

        PointFactory.DragController.CanDragging = false;

        PointFactory.FocusController.CanFocus = true;
        LineFactory.FocusController.CanFocus = true;

        eventArgs.Handled = true;
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.Drag;
        
        PointFactory.DragController.CanDragging = true;

        PointFactory.FocusController.CanFocus = true;
        LineFactory.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void OnCanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs) 
    {        
        switch (Status.CurrentAction)
        {
            case SgeStatus.Action.Drag:
                return;
            case SgeStatus.Action.SetSignlePoint:
                Point cursorPosition = eventArgs.GetPosition(SgeCanvas);
                PointFactory.CreateVisiblePoint(cursorPosition);
                break;
            case SgeStatus.Action.ChooseLineStartPoint:
                var ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null)
                {
                    LineFactory.Buffer = ellipse;
                    Status.CurrentAction = SgeStatus.Action.ChooseLineEndPoint;
                }         
                break;
            case SgeStatus.Action.ChooseLineEndPoint:                
                ellipse = eventArgs.OriginalSource as Ellipse;
                if(ellipse != null && ellipse != LineFactory.Buffer)
                {                    
                    LineFactory.CreateLineFromBuffer(ellipse);
                    Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;
                }               
                break;
            case SgeStatus.Action.Delete:
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

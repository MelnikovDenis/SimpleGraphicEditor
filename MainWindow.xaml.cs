using SimpleGraphicEditor.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;

namespace SimpleGraphicEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private SgeStatus Status { get; set; }
    private SgeViewModel ViewModel { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        Status = (SgeStatus)this.Resources["Status"];
        ViewModel = new SgeViewModel(SgeCanvas);
    }
    private void PointButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.SetSignlePoint;

        ViewModel.PointFactory.DragController.CanDragging = false;
        ViewModel.BindPointFactory.DragController.CanDragging = false;
        ViewModel.LineFactory.DragController.CanDragging = false;
        ViewModel.PointFactory.FocusController.CanFocus = false;
        ViewModel.LineFactory.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;

        ViewModel.PointFactory.DragController.CanDragging = false;
        ViewModel.BindPointFactory.DragController.CanDragging = false;
        ViewModel.LineFactory.DragController.CanDragging = false;
        ViewModel.PointFactory.FocusController.CanFocus = true;
        ViewModel.LineFactory.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.Delete;

        ViewModel.PointFactory.DragController.CanDragging = false;
        ViewModel.BindPointFactory.DragController.CanDragging = false;
        ViewModel.LineFactory.DragController.CanDragging = false;
        ViewModel.PointFactory.FocusController.CanFocus = true;
        ViewModel.LineFactory.FocusController.CanFocus = true;

        eventArgs.Handled = true;
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.Drag;

        ViewModel.PointFactory.DragController.CanDragging = true;
        ViewModel.BindPointFactory.DragController.CanDragging = false;
        ViewModel.LineFactory.DragController.CanDragging = true;
        ViewModel.PointFactory.FocusController.CanFocus = true;
        ViewModel.LineFactory.FocusController.CanFocus = true;

        eventArgs.Handled = true;
    }
    private void GroupingButtonClick(object sender, RoutedEventArgs eventArgs) 
    {   
        Status.CurrentAction = SgeStatus.Action.Grouping;

        ViewModel.PointFactory.DragController.CanDragging = false;
        ViewModel.BindPointFactory.DragController.CanDragging = true;
        ViewModel.LineFactory.DragController.CanDragging = false;
        ViewModel.PointFactory.FocusController.CanFocus = true;
        ViewModel.LineFactory.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }    
    private void OnCanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs) 
    {
        var ellipse = eventArgs.OriginalSource as Ellipse;
        var line = eventArgs.OriginalSource as Line;
        switch (Status.CurrentAction)
        {
            case SgeStatus.Action.Drag:
                return;
            case SgeStatus.Action.SetSignlePoint:
                Point cursorPosition = eventArgs.GetPosition(SgeCanvas);
                ViewModel.CreatePoint(cursorPosition);
                break;
            case SgeStatus.Action.ChooseLineStartPoint:
                if (ellipse != null)
                {
                    ViewModel.EllipseBuffer = ellipse;
                    Status.CurrentAction = SgeStatus.Action.ChooseLineEndPoint;                    
                }
                break;
            case SgeStatus.Action.ChooseLineEndPoint:
                if (ellipse != null && ellipse != ViewModel.EllipseBuffer)
                {
                    ViewModel.CreateLineFromBuffer(ellipse);
                    Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;
                }
                break;
            case SgeStatus.Action.Delete:
                if (line != null)
                    ViewModel.RemoveLine((Line)eventArgs.OriginalSource);
                else if(ellipse != null)
                    ViewModel.RemovePoint((Ellipse)eventArgs.OriginalSource);
                break;
            case SgeStatus.Action.Grouping:
                if(ellipse != null) 
                {
                    ViewModel.AddToGroup(ellipse);
                }
                break;
        }
        eventArgs.Handled = true;
    }
    private void OnCanvasRightMouseDown(object sender, MouseButtonEventArgs eventArgs)
    {
        var ellipse = eventArgs.OriginalSource as Ellipse;
        switch (Status.CurrentAction) 
        {
            case SgeStatus.Action.Grouping:
                if (ellipse != null)
                {
                    ViewModel.DeleteFromGroup(ellipse);
                }
                break;
        }
        eventArgs.Handled = true;
    }

}

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

        ViewModel.PointViewModel.DragController.CanDragging = false;
        ViewModel.LineViewModel.DragController.CanDragging = false;
        ViewModel.PointViewModel.FocusController.CanFocus = false;
        ViewModel.LineViewModel.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;

        ViewModel.PointViewModel.DragController.CanDragging = false;
        ViewModel.LineViewModel.DragController.CanDragging = false;
        ViewModel.PointViewModel.FocusController.CanFocus = true;
        ViewModel.LineViewModel.FocusController.CanFocus = false;

        eventArgs.Handled = true;
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.Delete;

        ViewModel.PointViewModel.DragController.CanDragging = false;
        ViewModel.LineViewModel.DragController.CanDragging = false;
        ViewModel.PointViewModel.FocusController.CanFocus = true;
        ViewModel.LineViewModel.FocusController.CanFocus = true;

        eventArgs.Handled = true;
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        Status.CurrentAction = SgeStatus.Action.Drag;

        ViewModel.PointViewModel.DragController.CanDragging = true;
        ViewModel.LineViewModel.DragController.CanDragging = true;
        ViewModel.PointViewModel.FocusController.CanFocus = true;
        ViewModel.LineViewModel.FocusController.CanFocus = true;

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
                ViewModel.CreatePoint(cursorPosition);
                break;
            case SgeStatus.Action.ChooseLineStartPoint:
                var ellipse = eventArgs.OriginalSource as Ellipse;
                if (ellipse != null)
                {
                    ViewModel.EllipseBuffer = ellipse;
                    Status.CurrentAction = SgeStatus.Action.ChooseLineEndPoint;                    
                }
                break;
            case SgeStatus.Action.ChooseLineEndPoint:
                ellipse = eventArgs.OriginalSource as Ellipse;
                if (ellipse != null && ellipse != ViewModel.EllipseBuffer)
                {
                    ViewModel.CreateLineFromBuffer(ellipse);
                    Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;
                }
                break;
            case SgeStatus.Action.Delete:
                if (eventArgs.OriginalSource is Line)
                    ViewModel.RemoveLine((Line)eventArgs.OriginalSource);
                else if(eventArgs.OriginalSource is Ellipse)
                    ViewModel.RemovePoint((Ellipse)eventArgs.OriginalSource);
                break;
        }
        eventArgs.Handled = true;
    }   
    
}

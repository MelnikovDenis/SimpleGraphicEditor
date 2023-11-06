using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels;
using SimpleGraphicEditor.ViewModels.DTO;
using SimpleGraphicEditor.ViewModels.EventControllers;
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
    private PositionDto PosDto { get; set; }
    private SgeMainViewModel MainViewModel { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        Status = (SgeStatus)this.Resources["Status"];
        PosDto = (PositionDto)this.Resources["PosDto"];
        MainViewModel = new SgeMainViewModel(SgeCanvas, Status, PosDto);
        

    }
    private void PointButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        PosDto.IsValid = true;
        MainViewModel.PointsViewModel.CanSelect = false;
        MainViewModel.PointsViewModel.CanFocus = false;
        MainViewModel.LinesViewModel.CanFocus = false;
        MainViewModel.ObserverViewModel.CanDragging = true;
        Status.CurrentAction = SgeStatus.Action.SetSinglePoint; 
        eventArgs.Handled = true; 
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        MainViewModel.PointsViewModel.CanSelect = false;
        MainViewModel.PointsViewModel.CanFocus = true;
        MainViewModel.LinesViewModel.CanFocus = false;
        MainViewModel.ObserverViewModel.CanDragging = true;
        Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint; 
        eventArgs.Handled = true; 
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs eventArgs)
    { 
        Status.CurrentAction = SgeStatus.Action.Delete;
        MainViewModel.PointsViewModel.CanSelect = false;
        MainViewModel.PointsViewModel.CanFocus = true;
        MainViewModel.LinesViewModel.CanFocus = true;
        MainViewModel.ObserverViewModel.CanDragging = true;
        eventArgs.Handled = true; 
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {       
        PosDto.IsValid = false;
        MainViewModel.PointsViewModel.CanSelect = true;
        MainViewModel.PointsViewModel.CanFocus = false;
        MainViewModel.LinesViewModel.CanFocus = false;
        MainViewModel.ObserverViewModel.CanDragging = true;
        Status.CurrentAction = SgeStatus.Action.Transfer;
        eventArgs.Handled = true; 
    }
    private void GroupingButtonClick(object sender, RoutedEventArgs eventArgs) 
    { 
        Status.CurrentAction = SgeStatus.Action.Grouping; 
        eventArgs.Handled = true; 
    }
    private void CanvasRightMouseDown(object sender, MouseButtonEventArgs eventArgs) 
    { 
        MainViewModel.ObserverViewModel.Observer.Rotate(0.1d, 0.1d); 
    }
    private void CanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs)
    {
        switch (Status.CurrentAction)
        {
            case SgeStatus.Action.ChooseLineStartPoint:
                if (MainViewModel.LinesViewModel.SetPointBuffer(eventArgs.OriginalSource, MainViewModel.PointsViewModel)) 
                {
                    Status.CurrentAction = SgeStatus.Action.ChooseLineEndPoint;
                    eventArgs.Handled = true;
                }                
                break;
            case SgeStatus.Action.ChooseLineEndPoint:
                if (MainViewModel.LinesViewModel.CreateLine(eventArgs.OriginalSource, MainViewModel.PointsViewModel))
                {
                    Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;
                    eventArgs.Handled = true;
                }                   
                break;
            case SgeStatus.Action.Delete:
                if (MainViewModel.PointsViewModel.RemovePoint(eventArgs.OriginalSource, MainViewModel.LinesViewModel) || MainViewModel.LinesViewModel.RemoveLine(eventArgs.OriginalSource)) 
                {
                    eventArgs.Handled = true;
                }              
                break;
        }
    }
    private void PosButton1Click(object sender, RoutedEventArgs eventArgs)
    {
        switch (Status.CurrentAction)
        {
            case SgeStatus.Action.Transfer:
                if (PosDto.IsValid)
                    MainViewModel.PointsViewModel.TransferPoint(PosDto.X, PosDto.Y, PosDto.Z);
                break;
            case SgeStatus.Action.SetSinglePoint:
                if (PosDto.IsValid)
                    MainViewModel.PointsViewModel.CreatePoint(PosDto.X, PosDto.Y, PosDto.Z);
                break;
        }
        eventArgs.Handled = true;
    }

}

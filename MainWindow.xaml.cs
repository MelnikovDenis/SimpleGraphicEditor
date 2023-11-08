using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.Models.Abstractions;
using SimpleGraphicEditor.ViewModels;
using SimpleGraphicEditor.ViewModels.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SimpleGraphicEditor;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{    
    private SgeMainViewModel MainViewModel { get; set; } = null!;
    public MainWindow()
    {
        InitializeComponent();        
    }
    void OnLoad(object sender, RoutedEventArgs eventArgs)
    {
        MainViewModel = new SgeMainViewModel(SgeCanvas, 
            (SgeStatus)this.Resources["Status"], 
            (PositionDto)this.Resources["PosDto"]);        
    }
    private void PointButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        MainViewModel.PointsViewModel.UnsetPointBuffer();
        MainViewModel.PosDto.IsValid = true;
        MainViewModel.PointsViewModel.CanSelect = false;
        MainViewModel.PointsViewModel.CanFocus = false;
        MainViewModel.LinesViewModel.CanFocus = false;
        MainViewModel.ObserverViewModel.CanDragging = true;
        MainViewModel.Status.CurrentAction = SgeStatus.Action.SetSinglePoint; 
        eventArgs.Handled = true; 
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        MainViewModel.PointsViewModel.UnsetPointBuffer();
        MainViewModel.PointsViewModel.CanSelect = false;
        MainViewModel.PointsViewModel.CanFocus = true;
        MainViewModel.LinesViewModel.CanFocus = false;
        MainViewModel.ObserverViewModel.CanDragging = true;
        MainViewModel.Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint; 
        eventArgs.Handled = true; 
    }
    private void DeleteButtonClick(object sender, RoutedEventArgs eventArgs)
    { 
        MainViewModel.PointsViewModel.UnsetPointBuffer();
        MainViewModel.Status.CurrentAction = SgeStatus.Action.Delete;
        MainViewModel.PointsViewModel.CanSelect = false;
        MainViewModel.PointsViewModel.CanFocus = true;
        MainViewModel.LinesViewModel.CanFocus = true;
        MainViewModel.ObserverViewModel.CanDragging = true;
        eventArgs.Handled = true; 
    }
    private void DragButtonClick(object sender, RoutedEventArgs eventArgs)
    {        
        MainViewModel.PosDto.IsValid = false;
        MainViewModel.PointsViewModel.CanSelect = true;
        MainViewModel.PointsViewModel.CanFocus = true;
        MainViewModel.LinesViewModel.CanFocus = false;
        MainViewModel.ObserverViewModel.CanDragging = true;
        MainViewModel.Status.CurrentAction = SgeStatus.Action.Transfer;
        eventArgs.Handled = true; 
    }
    private void GroupingButtonClick(object sender, RoutedEventArgs eventArgs) 
    { 
        MainViewModel.PointsViewModel.UnsetPointBuffer();
        MainViewModel.Status.CurrentAction = SgeStatus.Action.Grouping; 
        eventArgs.Handled = true; 
    }
    private void CanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs)
    {
        switch (MainViewModel.Status.CurrentAction)
        {
            case SgeStatus.Action.ChooseLineStartPoint:
                if (MainViewModel.LinesViewModel.SetPointBuffer(eventArgs.OriginalSource, MainViewModel.PointsViewModel)) 
                {
                    MainViewModel.Status.CurrentAction = SgeStatus.Action.ChooseLineEndPoint;
                    eventArgs.Handled = true;
                }                
                break;
            case SgeStatus.Action.ChooseLineEndPoint:
                if (MainViewModel.LinesViewModel.CreateLine(eventArgs.OriginalSource, MainViewModel.PointsViewModel))
                {
                    MainViewModel.Status.CurrentAction = SgeStatus.Action.ChooseLineStartPoint;
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
        switch ( MainViewModel.Status.CurrentAction)
        {
            case SgeStatus.Action.Transfer:
                if (MainViewModel.PosDto.IsValid)
                    MainViewModel.PointsViewModel.TransferPoint(MainViewModel.PosDto.X, MainViewModel.PosDto.Y, MainViewModel.PosDto.Z);
                break;
            case SgeStatus.Action.SetSinglePoint:
                if (MainViewModel.PosDto.IsValid)
                    MainViewModel.PointsViewModel.CreatePoint(MainViewModel.PosDto.X, MainViewModel.PosDto.Y, MainViewModel.PosDto.Z);
                break;
        }
        eventArgs.Handled = true;
    }
    private void MenuOpenClick(object sender, RoutedEventArgs eventArgs)
    {
        MainViewModel.Reset();
        var dialog = new Microsoft.Win32.OpenFileDialog();
        dialog.FileName = "Model";
        dialog.DefaultExt = ".txt";
        dialog.Filter = "Text documents (.txt)|*.txt";
        bool? result = dialog.ShowDialog();
        if (result == true)
        {
            string filename = dialog.FileName;
            try
            {
                using (StreamReader reader = new StreamReader(filename, System.Text.Encoding.UTF8))
                {
                    var points = new Dictionary<int, Point3D>(100);
                    string? line;
                    int counter = 0;
                    while ((line = reader.ReadLine()) != null)
                    {                   
                        var values = line.Split(' ');
                        if(values[0] == "v")
                        {
                            points.Add(counter++, 
                            MainViewModel.PointsViewModel.CreatePoint(
                                double.Parse(values[1]), 
                                double.Parse(values[2]), 
                                double.Parse(values[3]))
                            ); 
                        }
                        if(values[0] == "ed")
                        {
                            MainViewModel.LinesViewModel.CreateLine(points[int.Parse(values[1])], points[int.Parse(values[2])]);
                        }
                    }
                }
            }
            catch
            {
                MainViewModel.Reset();
                MessageBox.Show("Ooops, something went wrong!", "Open error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        eventArgs.Handled = true;
        
    }
    private void MenuSaveClick(object sender, RoutedEventArgs eventArgs)
    {
        var dialog = new Microsoft.Win32.SaveFileDialog();
        dialog.FileName = "Model";
        dialog.DefaultExt = ".txt";
        dialog.Filter = "Text documents (.txt)|*.txt";
        bool? result = dialog.ShowDialog();
        if (result == true)
        {
            string filename = dialog.FileName;
            using (StreamWriter writer = new StreamWriter(filename, false, System.Text.Encoding.UTF8))
            {
                int counter = 0;
                var points = new Dictionary<Point3D, int>(MainViewModel.PointsViewModel.Points.Count);
                foreach(var value in MainViewModel.PointsViewModel.Points.Values)
                {
                    writer.WriteLine($"v {value.RealX} {value.RealY} {value.RealZ}");
                    points.Add((Point3D)value, counter++);
                }
                counter = 0;
                foreach(var value in MainViewModel.LinesViewModel.Lines.Values)
                {
                    writer.WriteLine($"ed {points[value.FirstPoint]} {points[value.SecondPoint]}");
                }
            }
        }
        eventArgs.Handled = true;
    }
}

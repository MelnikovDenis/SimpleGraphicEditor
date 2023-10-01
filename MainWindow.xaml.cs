using SimpleGraphicEditor.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
    private IEnumerable<SgePoint> Points { get; set; } = new List<SgePoint>();
    private SgePoint? Buffer { get; set; } = null;
    private bool IsDragging { get; set; } = false;
    public enum Action 
    {
        SetSignlePoint,
        SetLineStartPoint,
        SetLineEndPoint
    }
    public Action CurrentAction { get; private set; } = Action.SetSignlePoint;
    public MainWindow()
    {
        InitializeComponent();
    }
    private void PointButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.SetSignlePoint;
        eventArgs.Handled = true;
    }
    private void LineButtonClick(object sender, RoutedEventArgs eventArgs)
    {
        CurrentAction = Action.SetLineStartPoint;
        eventArgs.Handled = true;
    }
    private void SgeCanvasLeftMouseDown(object sender, MouseButtonEventArgs eventArgs) 
    {
        
        switch (CurrentAction)
        {
            case Action.SetSignlePoint:
                var draggableControl = FromEllipse(eventArgs.OriginalSource);
                if (draggableControl != null)
                {
                    IsDragging = true;
                    draggableControl.VisiblePoint.CaptureMouse();
                    eventArgs.Handled = true;
                    return;
                }
                Debug.WriteLine($"CurrentAction: {CurrentAction.ToString()}");
                Point cursorPosition = eventArgs.GetPosition(SgeCanvas);
                var point = new SgePoint(cursorPosition.X, cursorPosition.Y, SgeCanvas);
                Points = Points.Append(point);
                point.Draw();
                break;
            case Action.SetLineStartPoint:
                Debug.WriteLine($"CurrentAction: {CurrentAction.ToString()}");
                Buffer = FromEllipse(eventArgs.OriginalSource);
                Debug.WriteLine($"Buffer: {Buffer?.ToString()}");
                if (Buffer != null)
                    CurrentAction = Action.SetLineEndPoint;
                break;
            case Action.SetLineEndPoint:
                Debug.WriteLine($"CurrentAction: {CurrentAction.ToString()}");
                var endPoint = FromEllipse(eventArgs.OriginalSource);
                if (Buffer != null && endPoint != null)
                {
                    var line = new SgeLine(Buffer, endPoint, SgeCanvas);
                    Buffer = null;
                    line.Draw();
                }
                CurrentAction = Action.SetLineStartPoint;
                break;

        }
        eventArgs.Handled = true;
    }
    private void SgeCanvasMouseMove(object sender, MouseEventArgs eventArgs) 
    {
        var draggableControl = FromEllipse(eventArgs.OriginalSource);

        if (IsDragging && draggableControl != null)
        {
            Point currentPosition = eventArgs.GetPosition(sender as Shape);
            draggableControl.X = currentPosition.X;
            draggableControl.Y = currentPosition.Y;
            eventArgs.Handled = true;
        }
    }
    private void SgeCanvasMouseUp(object sender, MouseButtonEventArgs eventArgs)
    {
        IsDragging = false;
        var draggable = FromEllipse(eventArgs.OriginalSource);
        draggable?.VisiblePoint.ReleaseMouseCapture();
        eventArgs.Handled = true;
    }

    private SgePoint? FromEllipse(object source)
    {
        var ellipse = source as Ellipse;
        return Points.FirstOrDefault(sgep => sgep.VisiblePoint.Equals(ellipse));
    }
}

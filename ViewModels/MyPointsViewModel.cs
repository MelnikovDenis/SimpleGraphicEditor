using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public class MyPointsViewModel
{    
      private Canvas TargetCanvas { get; }
      private Observer Observer { get; }
      private DragController PointDragController { get; } = null!;
      private FocusController PointFocusController { get; } = null!;
      private Dictionary<Ellipse, MyPoint> Points { get; } = new Dictionary<Ellipse, MyPoint>();
      public bool CanDragging 
      {
            get => PointDragController.CanDragging;
            set => PointDragController.CanDragging = value;
      }
      public bool CanFocus
      {
            get => PointFocusController.CanFocus;
            set => PointFocusController.CanFocus = value;
      }
      public MyPointsViewModel(Canvas targetCanvas, Observer observer)
      {
            TargetCanvas = targetCanvas;
            Observer = observer;
            PointDragController = new DragController(TargetCanvas, PointMove);
            PointFocusController = new FocusController(null, DefaultValues.FocusBrush);
      }
      public void CreatePoint(double x, double y, double z)
      {
            var point = new MyPoint(TargetCanvas, Observer, PointFocusController, PointDragController, x + Observer.RealX, y + Observer.RealY, z);
            var ellipse = point.VisiblePoint;
            Points.Add(ellipse, point);
      }
      //ДОПИСАТЬ ДЛЯ 3Д
      private void PointMove(object sender, Point delta)
      {
            var ellipse = sender as Ellipse;
            if(ellipse != null && Points.ContainsKey(ellipse))
            {
                  var point = Points[ellipse];
                  point.Move(delta.X, delta.Y, 0d);
            }
      }
}

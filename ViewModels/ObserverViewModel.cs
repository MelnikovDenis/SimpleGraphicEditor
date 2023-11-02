using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.EventControllers;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels;

public class ObserverViewModel
{
      private Canvas TargetCanvas { get; }
      private DragController ObserverDragController { get; } = null!;
      public Observer Observer { get; } = null!;
      public bool CanDragging 
      {
            get => ObserverDragController.CanDragging;
            set => ObserverDragController.CanDragging = value;
      }
      public ObserverViewModel(Canvas targetCanvas)
      {
            TargetCanvas = targetCanvas;
            ObserverDragController = new DragController(TargetCanvas, ObserverMove);            
            Observer = new Observer(TargetCanvas, ObserverDragController, 0, 0);
      }
      //ДОПИСАТЬ ДЛЯ 3Д
      private void ObserverMove(object sender, Point delta)
      {
            Debug.WriteLine($"ViewModel.ObserverMove: {delta.X}, {delta.Y}");
            var canvas = sender as Canvas;
            if(ReferenceEquals(TargetCanvas, canvas))
            {                  
                  Observer.Move(delta.X, delta.Y);
            }
      }
}

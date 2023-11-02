using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using SimpleGraphicEditor.ViewModels.EventControllers;

namespace SimpleGraphicEditor.Models;
public class Observer : PositionObject2D
{
      public event Action<double, double>? MoveEventHandler;
      public Observer(Canvas targetCanvas, DragController dragController, double realX, double realY) : base(realX, realY)
      { 
            targetCanvas.MouseMove += dragController.OnMouseMove;
            targetCanvas.MouseLeftButtonDown += dragController.OnMouseLeftButtonDown;
            targetCanvas.MouseLeftButtonUp += dragController.OnMouseLeftButtonDown;
      }
      public void Move(double deltaX, double deltaY)
      {
            Console.WriteLine($"ObserverMove: {deltaX}, {deltaY}");
            double oldX = RealX;
            double oldY = RealY;
            RealX += deltaX;
            RealY += deltaY;
            MoveEventHandler?.Invoke(RealX - oldX, RealY - oldY);
      }
      public void Move(Point delta)
      {
            Console.WriteLine($"ObserverMove: {delta.X}, {delta.Y}");
            double oldX = RealX;
            double oldY = RealY;
            RealX += delta.X;
            RealY += delta.Y;
            MoveEventHandler?.Invoke(RealX - oldX, RealY - oldY);
      }
}
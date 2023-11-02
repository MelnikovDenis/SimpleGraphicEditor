using System.Windows.Controls;
using SimpleGraphicEditor.ViewModels.Static;
using System.Windows.Shapes;
using SimpleGraphicEditor.ViewModels.EventControllers;
using System.Diagnostics;
using System;

namespace SimpleGraphicEditor.Models;
public class MyPoint : PositionObject3D
{      
      public Ellipse VisiblePoint { get; private set; }
      public double VisibleX 
      {
            get
            {
                  return Canvas.GetLeft(VisiblePoint) + VisiblePoint.Width / 2d;
            }
            private set
            {
                  Canvas.SetLeft(VisiblePoint, value - VisiblePoint.Width / 2d);
            }
      }
      public double VisibleY
      {
            get
            {
                  return Canvas.GetTop(VisiblePoint) + VisiblePoint.Height / 2d;
            }
            private set
            {
                  Canvas.SetTop(VisiblePoint, value - VisiblePoint.Height / 2d);
            }
      }
      public Observer Observer { get; set; }
      public MyPoint(
            Canvas targetCanvas,
            Observer observer, 
            FocusController focusController, 
            DragController dragController, 
            double realX, 
            double realY, 
            double realZ) 
            : base(realX, realY, realZ)
      {
            Observer = observer;
            observer.MoveEventHandler += OnObserverMove;
            VisiblePoint = CreateEllipse(targetCanvas, focusController, dragController);
            Project();
      }
      //ДОПИСАТЬ ДЛЯ ПОВОРОТОВ 3D
      private void Project()
      {
            VisibleX = RealX - Observer.RealX;
            VisibleY = RealY - Observer.RealY;
      }
      public void OnObserverMove(double deltaX, double deltaY)
      {
            Console.WriteLine($"OnObserverMove: {deltaX}, {deltaY}");
            VisibleX += deltaX;
            VisibleY += deltaY;           
      }
      public void Move(double deltaX, double deltaY, double deltaZ)
      {
            RealX += deltaX;
            RealY += deltaY;
            RealZ += deltaZ;
            Project();
      }
      private static Ellipse CreateEllipse(Canvas targetCanvas, FocusController focusController, DragController dragController) 
      {
            var ellipse = new Ellipse()
            {
                  Fill = DefaultValues.DefaultPointBrush,
                  Stroke = DefaultValues.DefaultPointBrush,
                  StrokeThickness = DefaultValues.DefaultPointStrokeThickness,
                  Width = DefaultValues.DefualtPointDiameter,
                  Height = DefaultValues.DefualtPointDiameter
            };
            Canvas.SetZIndex(ellipse, DefaultValues.DefaultPointZIndex);
            ellipse.MouseMove += dragController.OnMouseMove;
            ellipse.MouseLeftButtonDown += dragController.OnMouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += dragController.OnMouseLeftButtonUp;
            ellipse.MouseEnter += focusController.OnMouseEnter;
            ellipse.MouseLeave += focusController.OnMouseLeave;
            targetCanvas.Children.Add(ellipse);            
            return ellipse;
      }
}
namespace SimpleGraphicEditor.Models;
public abstract class PositionObject2D
{
      public static double MinX { get; } = -5000d;
      public static double MaxX { get; } = 5000d;
      public static double MinY { get; } = -5000d;
      public static double MaxY { get; } = 5000d;
      private double realX = 0d;
      private double realY = 0d;
      
      public virtual double RealX
      { 
            get 
            {
                  return realX;
            } 
            protected set 
            {
                  realX = Limit(MinX, MaxX, value);
            } 
      }
      public virtual double RealY
      { 
            get 
            {
                  return realY;
            } 
            protected set 
            {
                  realY = Limit(MinY, MaxY, value);
            } 
      }
      protected PositionObject2D() { }
      protected PositionObject2D(double realX, double realY)
      {
            RealX = realX;
            RealY = realY;
      }
      protected static double Limit(double min, double max, double value)
      {
            if(value > max)
                  return max;
            else if(value < min)
                  return min;
            else
                  return value;
      }
}
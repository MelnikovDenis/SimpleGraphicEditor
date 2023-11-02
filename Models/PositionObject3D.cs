namespace SimpleGraphicEditor.Models;
public abstract class PositionObject3D : PositionObject2D 
{
      public static double MinZ { get; } = -5000d;
      public static double MaxZ { get; } = 5000d;
      private double realZ = 0d;
      public virtual double RealZ
      { 
            get 
            {
                  return realZ;
            } 
            protected set 
            {
                  realZ = Limit(MinZ, MaxZ, value);
            }
      }
      protected PositionObject3D() : base() { }
      protected PositionObject3D(double realX, double realY, double realZ) : base(realX, realY)
      {
            RealZ = realZ;
      }
}
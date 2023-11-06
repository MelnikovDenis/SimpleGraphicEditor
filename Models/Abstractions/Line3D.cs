namespace SimpleGraphicEditor.Models.Abstractions;

public class Line3D
{
    public Point3D FirstPoint { get; set; }
    public Point3D SecondPoint { get; set; }
    public Line3D(Point3D firstPoint, Point3D secondPoint)
    {
        FirstPoint = firstPoint;
        SecondPoint = secondPoint;
        FirstPoint.AttachedLines.AddLast(this);
        SecondPoint.AttachedLines.AddLast(this);
    }
}

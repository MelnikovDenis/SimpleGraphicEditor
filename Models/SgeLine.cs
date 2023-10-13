using SimpleGraphicEditor.Models.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimpleGraphicEditor.Models;

public class SgeLine : IMoveable
{
    public SgePoint Point1 { get; }
    public SgePoint Point2 { get; }
    public SgeLine(SgePoint point1, SgePoint point2)
    {
        Point1 = point1;      
        Point2 = point2;
        point1.AttachedLines.Add(this);
        point2.AttachedLines.Add(this);
    }
    public override bool Equals(object? obj) =>
        Equals(obj as SgeLine);
    public bool Equals(SgeLine? other) =>
        (other != null) &&
        ((other.Point1.Equals(Point1) && other.Point2.Equals(Point2)) || 
        (other.Point2.Equals(Point1) && other.Point2.Equals(Point1)));

    public override int GetHashCode()
        => (Point1.GetHashCode() ^ Point2.GetHashCode());

    public void Move(Point delta)
    {       
        Point1.Move(delta);
        Point2.Move(delta);
    }
}

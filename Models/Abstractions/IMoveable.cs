using System.Windows;

namespace SimpleGraphicEditor.Models.Abstractions;

public interface IMoveable
{
    public void Move(Point delta);
}

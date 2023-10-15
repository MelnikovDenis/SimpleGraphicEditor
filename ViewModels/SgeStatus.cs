using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimpleGraphicEditor.ViewModels;
public class SgeStatus : INotifyPropertyChanged
{
    private static string[] Messages { get; } = new string[]
    {
            "Точка",
            "Первая точка линии",
            "Вторая точка линии",
            "Перемещение",
            "Удаление",
            "Группировка точек"
    };
    private static Action DefaultAction { get; } = Action.SetSignlePoint;
    public enum Action
    {
        SetSignlePoint,
        ChooseLineStartPoint,
        ChooseLineEndPoint,
        Drag,
        Delete,
        Grouping
    }
    private Action currentAction = DefaultAction;
    public Action CurrentAction
    {
        get { return currentAction; }
        set
        {
            currentAction = value;
            Message = Messages[(int)value];
        }
    }
    private string message = Messages[(int)DefaultAction];
    public string Message
    {
        get { return message; }
        private set
        {
            message = value;
            OnPropertyChanged(nameof(message));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public SgeStatus()
    {

    }
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
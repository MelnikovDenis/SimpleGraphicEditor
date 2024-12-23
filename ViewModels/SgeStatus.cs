using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimpleGraphicEditor.ViewModels;
public class SgeStatus : INotifyPropertyChanged
{
    private static string[] PosButton1Texts { get; } = new string[]
    {        
        "Поставить точку",
        "Переместить точку",
        "Переместить точку привязки",
        "Переместить группу",
        "Повернуть группу",
        "Увеличить группу"
    };
    private static string[] ActionMessages { get; } = new string[]
    {
        "Поставить точку на координаты",
        "Указать первую точку линии",
        "Указать вторую точку линии",
        "Переместить точку на координаты",
        "Удалить выбранную точку или линию",
        "Установить точку привязки на координаты",
        "Переместить группу на координаты",
        "Повернуть группу на угол в градусах",
        "Зеркалировать группу",
        "Масштабировать группу"
    };
    private string actionMessage = ActionMessages[(int)DefaultAction];
    public string ActionMessage
    {
        get { return actionMessage; }
        private set
        {
            actionMessage = value;
            OnPropertyChanged(nameof(ActionMessage));
        }
    }
    private string posButton1Text = PosButton1Texts[0];
    public string PosButton1Text
    {
        get { return posButton1Text; }
        private set
        {
            posButton1Text = value;
            OnPropertyChanged(nameof(PosButton1Text));
            OnPropertyChanged(nameof(PosBlock1Enabled));
        }
    }
    public Visibility PosBlock1Enabled 
    {
        get 
        {
            if (posButton1Text != string.Empty)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }  
    }
    private Visibility mirrorBlockEnabled = Visibility.Collapsed;
    public Visibility MirrorBlockEnabled 
    {
        get 
        {
            return mirrorBlockEnabled;

        }
        set
        {
            mirrorBlockEnabled = value;
            OnPropertyChanged(nameof(MirrorBlockEnabled));
        }
    }
    private static Action DefaultAction { get; } = Action.SetSinglePoint;
    public enum Action
    {
        SetSinglePoint,
        ChooseLineStartPoint,
        ChooseLineEndPoint,
        Transfer,
        Delete,
        Grouping,
        GroupTransfer,
        GroupRotate,
        GroupMirror,
        GroupScale
    }
    private Action currentAction = DefaultAction;
    public Action CurrentAction
    {
        get { return currentAction; }
        set
        {
            currentAction = value;
            ActionMessage = ActionMessages[(int)value];
            if(value == Action.GroupMirror)
                MirrorBlockEnabled = Visibility.Visible;
            else
                MirrorBlockEnabled = Visibility.Collapsed;
            if (value == Action.SetSinglePoint)
                PosButton1Text = PosButton1Texts[0];
            else if(value == Action.Transfer)
                PosButton1Text = PosButton1Texts[1];
            else if (value == Action.Grouping)
                PosButton1Text = PosButton1Texts[2];
            else if (value == Action.GroupTransfer)
                PosButton1Text = PosButton1Texts[3];
            else if (value == Action.GroupRotate)
                PosButton1Text = PosButton1Texts[4];
            else if (value == Action.GroupScale)
                PosButton1Text = PosButton1Texts[5];
            else
                PosButton1Text = string.Empty;
        }
    }   
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
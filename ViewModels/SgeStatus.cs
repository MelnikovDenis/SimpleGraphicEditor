using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SimpleGraphicEditor.ViewModels;
public class SgeStatus : INotifyPropertyChanged
{
    private static string[] ActionMessages { get; } = new string[]
    {
            "Поставить точку",
            "Указать первую точку линии",
            "Указать вторую точку линии",
            "Переместить точку",
            "Удалить точку",
            "Группировать точки"
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
    private string posButton1Text = ActionMessages[(int)DefaultAction];
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
    private static Action DefaultAction { get; } = Action.SetSinglePoint;
    public enum Action
    {
        SetSinglePoint,
        ChooseLineStartPoint,
        ChooseLineEndPoint,
        Transfer,
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
            ActionMessage = ActionMessages[(int)value];
            if (value == Action.SetSinglePoint || value == Action.Transfer)
                PosButton1Text = ActionMessages[(int)value];
            else
                PosButton1Text = string.Empty;
        }
    }   
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
using SimpleGraphicEditor.Models;
using SimpleGraphicEditor.ViewModels.Static;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SimpleGraphicEditor.ViewModels.DTO;

public class PositionDto : IDataErrorInfo, INotifyPropertyChanged
{
    private Dictionary<string, string> Errors { get; } = new Dictionary<string, string>();
    public string this[string propertyName] => Errors.ContainsKey(propertyName) ? Errors[propertyName] : string.Empty;
    private bool isValid = true;
    public bool IsValid 
    { 
        get => !Errors.Values.Any(x => x != string.Empty) && isValid; 
        set 
        { 
            isValid = value; 
            OnPropertyChanged(nameof(IsValid)); 
        } 
    }
    public string Error
    {
        get { return string.Empty; }
    }
    private double x = 0d;
    private double y = 0d;
    private double z = 0d;
    public double X 
    {
        get => x;
        set 
        {
            x = value;
            OnPropertyChanged(nameof(X));
            if (value < DefaultValues.MinCoordinate || value > DefaultValues.MaxCoordinate) 
                Errors[nameof(X)] = $"{nameof(X)} must be in range [{DefaultValues.MinCoordinate}, {DefaultValues.MaxCoordinate}].";                
            else 
                Errors[nameof(X)] = string.Empty;
            OnPropertyChanged(nameof(IsValid));
        }
    }
    public double Y
    {
        get => y;
        set
        {
            y = value;
            OnPropertyChanged(nameof(Y));
            if (value < DefaultValues.MinCoordinate || value > DefaultValues.MaxCoordinate)             
                Errors[nameof(Y)] = $"{nameof(Y)} must be in range [{DefaultValues.MinCoordinate}, {DefaultValues.MaxCoordinate}].";
            else              
                Errors[nameof(Y)] = string.Empty;
            OnPropertyChanged(nameof(IsValid));
        }
    }
    public double Z
    {
        get => z;
        set
        {
            z = value;
            OnPropertyChanged(nameof(Z));
            if (value < DefaultValues.MinCoordinate || value > DefaultValues.MaxCoordinate)
                Errors[nameof(Z)] = $"{nameof(Z)} must be in range [{DefaultValues.MinCoordinate}, {DefaultValues.MaxCoordinate}].";
            else                
                Errors[nameof(Z)] = string.Empty;
            OnPropertyChanged(nameof(IsValid));
        }
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}

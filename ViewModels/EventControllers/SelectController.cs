using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SimpleGraphicEditor.ViewModels.EventControllers;

public class SelectController
{
    private Shape? Selected { get; set; } = null;
    public bool CanSelect { get; set; }
    public bool AllowMultipleSelect { get; set; }  
    public Brush? DefaultFillBrush { get; set; } = null;
    public Brush? DefaultStrokeBrush { get; set; } = null;
    public Brush? SelectFillBrush { get; set; } = null;
    public Brush? SelectStrokeBrush { get; set; } = null;
    public event Action<object> OnSelect;
    public event Action<object> OnUnselect;
    public SelectController(Brush? selectFillBrush, 
        Brush? selectStrokeBrush, 
        Action<object> onLeftMouseClick, 
        Action<object> onRightMouseClick, 
        bool allowMultipleSelect = false)
    {
        AllowMultipleSelect = allowMultipleSelect;
        SelectFillBrush = selectFillBrush;
        SelectStrokeBrush = selectStrokeBrush;
        OnSelect += onLeftMouseClick;
        OnUnselect += onRightMouseClick;       
    }
    public void LeftMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if(CanSelect && sender is Shape selectable) 
        {
            if (!AllowMultipleSelect) 
            {
                if(Selected != null) 
                {
                    if (DefaultFillBrush != null)
                        Selected.Fill = DefaultFillBrush;
                    if (DefaultStrokeBrush != null)
                        Selected.Stroke = DefaultStrokeBrush;
                    DefaultFillBrush = null;
                    DefaultStrokeBrush = null;
                    OnUnselect?.Invoke(Selected);
                }                
                Selected = selectable;
            }
            DefaultFillBrush = selectable.Fill;
            DefaultStrokeBrush = selectable.Stroke;
            if (SelectFillBrush != null)
                selectable.Fill = SelectFillBrush;
            if (SelectStrokeBrush != null)
                selectable.Stroke = SelectStrokeBrush;            
            OnSelect?.Invoke(sender);
            eventArgs.Handled = true;
        }
    }
    public void RightMouseClickHandler(object sender, MouseEventArgs eventArgs)
    {
        if (CanSelect && sender is Shape selectable && selectable == Selected)
        {
            if (DefaultFillBrush != null)
                selectable.Fill = DefaultFillBrush;
            if (DefaultStrokeBrush != null)
                selectable.Stroke = DefaultStrokeBrush;
            DefaultFillBrush = null;
            DefaultStrokeBrush = null;
            OnUnselect?.Invoke(sender);
            eventArgs.Handled = true;
        }
    }
}
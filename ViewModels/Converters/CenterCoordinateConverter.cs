using System;
using System.Globalization;
using System.Windows.Data;
namespace SimpleGraphicEditor.ViewModels.Converters;
public class CenterCoordinateConverter: IValueConverter
{
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
            if(value is not double || parameter is not double)
                  throw new InvalidCastException("Конвертируемое значение и параметр не являются типом double.");
            return (double)value - (double)parameter / 2d; 
      }
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
            if(value is not double || parameter is not double)
                  throw new InvalidCastException("Конвертируемое значение и параметр не являются типом double.");
            return (double)value + (double)parameter / 2d; 
      }
}
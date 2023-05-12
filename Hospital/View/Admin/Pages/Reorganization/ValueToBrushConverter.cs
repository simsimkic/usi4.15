using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Hospital.View.Admin.Pages.Reorganization
{
    public class ValueToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int input = (int)value;
            switch (input)
            {

                case 0:
                    return Brushes.Tomato;
                case < 5:
                    return Brushes.Khaki;
                default:
                    return Brushes.PaleGreen;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

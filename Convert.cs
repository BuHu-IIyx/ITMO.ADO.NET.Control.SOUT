using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ITMO.ADO.NET.Control.SOUT
{
    public sealed class CategoryValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Departments)
            {
                Departments x = (Departments)value;
                return (x).Departments1.Where(n => n.ParrentDepartmentID == x.ParrentDepartmentID);
            }
            else
            {
                return null;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

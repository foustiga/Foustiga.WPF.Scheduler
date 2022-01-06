using System;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Foustiga.WPF.Scheduler.Foundation.Enums
{
    public class EnumToDisplayAttribConverter : IValueConverter
    {
        #region IValueConverter Members

        //source from http://formatexception.com/2014/07/bind-a-combobox-to-an-enum-while-using-display-attribute/
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (!value.GetType().IsEnum)
                {
                    return null;
                    //throw new ArgumentException("Value must be an Enumeration type");
                }
                var fieldInfo = value.GetType().GetField(value.ToString());
                var array = fieldInfo.GetCustomAttributes(false);
                foreach (var attrib in array)
                {
                    if (attrib is EnumInfoAttribute)
                    {
                        EnumInfoAttribute enumInfoAttrib = attrib as EnumInfoAttribute;
                        return enumInfoAttrib.Display;
                    }
                }

                //if we get here than there was no attrib, just pretty up the output by spacing on case
                // per http://stackoverflow.com/questions/155303/net-how-can-you-split-a-caps-delimited-string-into-an-array
                string name = Enum.GetName(value.GetType(), value);
                return Regex.Replace(name, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class EnumToTooltipAttribConverter : IValueConverter
    {
        #region IValueConverter Members
        //source from http://formatexception.com/2014/07/bind-a-combobox-to-an-enum-while-using-display-attribute/
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (!value.GetType().IsEnum)
                {
                    return null;
                    //throw new ArgumentException("Value must be an Enumeration type");
                }
                var fieldInfo = value.GetType().GetField(value.ToString());
                if (fieldInfo == null) return null;
                var array = fieldInfo.GetCustomAttributes(false);
                foreach (var attrib in array)
                {
                    if (attrib is EnumInfoAttribute)
                    {
                        EnumInfoAttribute enumInfoAttrib = attrib as EnumInfoAttribute;
                        return enumInfoAttrib.Tooltip;
                    }
                }

                //if we get here than there was no attrib, just pretty up the output by spacing on case
                // per http://stackoverflow.com/questions/155303/net-how-can-you-split-a-caps-delimited-string-into-an-array
                string name = Enum.GetName(value.GetType(), value);
                return Regex.Replace(name, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            }
            else
            { return null; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}

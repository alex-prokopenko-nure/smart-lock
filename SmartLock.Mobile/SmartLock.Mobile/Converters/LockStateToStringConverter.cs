using SmartLock.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SmartLock.Mobile.Converters
{
    class LockStateToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((LockState)value) {
                case LockState.Opened:
                    return "Opened";
                case LockState.Closed:
                    return "Closed";
                case LockState.Failed:
                    return "Failed";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Locked" ? true : false;
        }
    }
}

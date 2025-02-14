﻿using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Playnite.Converters
{
    public class ListSizeToBoolConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is IList<dynamic> list)
            {
                return list.Count > 0;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class ListSizeToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is System.Collections.IList list)
            {
                return list.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class NiceListToStringConverter : MarkupExtension, IValueConverter
    {        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return string.Join(", ", (IEnumerable<object>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var stringVal = (string)value;

            if (string.IsNullOrEmpty(stringVal))
            {
                return null;
            }
            else
            {
                var converted = stringVal.Split(new char[] { ',' }).Select(a => a.Trim());
                if (targetType == typeof(ComparableList<string>))
                {
                    return new ComparableList<string>(converted);
                }
                else
                {
                    return converted.ToList();
                }
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class ListToStringConverter : MarkupExtension, IValueConverter
    {
        private const string defaultSeperator = ",";

        public static string MakeString(IEnumerable<string> source)
        {
            return string.Join(",", source);
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var sep = defaultSeperator;
            if (parameter is string customSep)
            {
                sep = customSep;
            }

            return string.Join(sep, (IEnumerable<object>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var stringVal = (string)value;
            if (string.IsNullOrEmpty(stringVal))
            {
                return null;
            }
            else
            {
                var sep = defaultSeperator;
                if (parameter is string customSep)
                {
                    sep = customSep;
                }

                var converted = stringVal.Split(new [] { sep }, StringSplitOptions.None);
                if (targetType == typeof(ComparableList<string>))
                {
                    return new ComparableList<string>(converted);
                }
                if (targetType == typeof(ObservableCollection<string>))
                {
                    return new ObservableCollection<string>(converted);
                }
                else
                {
                    return converted.ToList();
                }
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

    public class ListToMultilineStringConverter : MarkupExtension, IValueConverter
    {
        private readonly string[] splitter = new string[] { "\n" };

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return string.Join("\n", (IEnumerable<object>)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var stringVal = (string)value;
            if (string.IsNullOrEmpty(stringVal))
            {
                return null;
            }
            else
            {
                var converted = stringVal.Split(splitter, StringSplitOptions.None).Select(a => a.Trim('\r')).ToArray();
                if (targetType == typeof(ComparableList<string>))
                {
                    return new ComparableList<string>(converted);
                }
                if (targetType == typeof(ObservableCollection<string>))
                {
                    return new ObservableCollection<string>(converted);
                }
                else
                {
                    return converted.ToList();
                }
            }
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

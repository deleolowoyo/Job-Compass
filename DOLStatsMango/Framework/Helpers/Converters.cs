using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DOLStatsMango.Models.Zillow;
using DOLStatsMango.Resources.en;

namespace DOLStatsMango.Framework.Helpers
{
    public class NegativeBoolConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? x = (bool?)value;

            return (x.HasValue ? !x : false);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class NegativeBoolVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? x = (bool?)value;

            if (x.HasValue)
            {
                if (x.Value)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class BoolToYesNoConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value ? "Yes" : "No");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StringtoNAConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (value == null ? string.Empty : (string)value);

            return (String.IsNullOrWhiteSpace(val) ? "N/A" : val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ListingEmptyStringConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (value == null ? string.Empty : (string)value);

            return (String.IsNullOrWhiteSpace(val) ? "--" : val);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class JobFieldToHeadingConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string defaultHeading = "(for related job field(s))";
            string mainHeading = "(for {0} field(s))";

            if (value == null)
            {
                return defaultHeading;
            }

            string jobField = (string)value;
            return (String.IsNullOrWhiteSpace(jobField) ? defaultHeading : String.Format(mainHeading, jobField));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class StringToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// This function will return Visibile or Collapsed based on string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string val = (value == null ? string.Empty : (string)value);

            return (String.IsNullOrWhiteSpace(val) ? Visibility.Collapsed : Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class ListingCategoryConverter : IValueConverter
    {
        /// <summary>
        /// This function handles converting listing category enum to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ListingCategory cat = (ListingCategory) value;
            string retVal = string.Empty;

            switch (cat)
            {
                case ListingCategory.MakeMeMove:
                    retVal = AppResources.Zillow_MakeMeMove;
                    break;
                case ListingCategory.ForSaleByOwner:
                    retVal = "For Sale By Owner";
                    break;
                case ListingCategory.ForSaleByAgent:
                    retVal = "For Sale By Agent";
                    break;
                case ListingCategory.ReportForSale:
                    retVal = "Report For Sale";
                    break;
                case ListingCategory.ForRent:
                    retVal = "For Rent";
                    break;
                default:
                    break;
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public class FormatStringConverter : IValueConverter
    {
        public string Format
        {
            get;
            set;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Format(this.Format, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class DemographicValueToImageConverter : IValueConverter
    {
        /// <summary>
        /// This function will return the opposite bool value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = string.Empty;
            double val = (value == null ? 0 : double.Parse((string)value));

            if (val < 0)
            {
                imagePath = @"/Resources/Images/RedDownArrow.png";
            }

            return imagePath;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

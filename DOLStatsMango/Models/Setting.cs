using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DOLStatsMango.Models
{
    public class Setting : ModelBase
    {
        #region Properties

        private bool _isLocationAware;
        public bool IsLocationAware
        {
            get
            {
                return _isLocationAware;
            }
            set
            {
                if (value != _isLocationAware)
                {
                    _isLocationAware = value;
                    OnPropertyChanged("IsLocationAware");
                }
            }
        }

        #endregion
    }
}

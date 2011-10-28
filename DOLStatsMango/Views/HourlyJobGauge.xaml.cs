using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DOLStatsMango.Views
{
    public partial class HourlyJobGauge : UserControl
    {
        private double value = 0;
        private int percentile = 10;

        public HourlyJobGauge()
        {
            InitializeComponent();
            this.mainIndicator.Value = this.value;
            this.jGauge.Text = this.percentile + "th";
        }

        public double Value
        {
            get
            {
                return this.value;
            }

            set
            {
                this.value = value;
                if (this.mainIndicator != null)
                {
                    this.mainIndicator.Value = value;
                }
            }
        }

        public int Percentile
        {
            get
            {
                return this.percentile;
            }

            set
            {
                this.percentile = value;
                if (this.jGauge != null)
                {
                    this.jGauge.Text = value + "th";
                }
            }
        }
    }
}

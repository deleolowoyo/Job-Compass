﻿#pragma checksum "C:\Users\e42cgki\Desktop\WorkDocs\WindowsPhone7\DOLStatsMango\DOLStatsMango\Views\ProfitGauge.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "477C2349FDBEC74E66F366212A9D0DB2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;
using Telerik.Windows.Controls;


namespace Telerik.Examples.WP.Chart.Dashboards {
    
    
    public partial class ProfitGauge : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Telerik.Windows.Controls.RadialBarGaugeIndicator mainIndicator;
        
        internal System.Windows.Controls.TextBlock qGauge;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/DOLStatsMango;component/Views/ProfitGauge.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.mainIndicator = ((Telerik.Windows.Controls.RadialBarGaugeIndicator)(this.FindName("mainIndicator")));
            this.qGauge = ((System.Windows.Controls.TextBlock)(this.FindName("qGauge")));
        }
    }
}

﻿#pragma checksum "C:\Users\e42cgki\Desktop\WorkDocs\WindowsPhone7\DOLStatsMango\DOLStatsMango\Views\CompanySearchResults.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5183FF0D95FA2FA5EF67AE68AC4B12B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
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


namespace DOLStatsMango.Views {
    
    
    public partial class CompanySearchResults : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Telerik.Windows.Controls.RadDataBoundListBox lbJobSearchResults;
        
        internal Microsoft.Phone.Controls.Maps.Map mapJobs;
        
        internal Telerik.Windows.Controls.RadWindow radWindowSortBy;
        
        internal System.Windows.Controls.RadioButton rbPostedDate;
        
        internal System.Windows.Controls.RadioButton rbPayHigh;
        
        internal System.Windows.Controls.RadioButton rbPayLow;
        
        internal System.Windows.Controls.RadioButton rbJobTitle;
        
        internal System.Windows.Controls.RadioButton rbCompanyName;
        
        internal System.Windows.Controls.RadioButton rbDistance;
        
        internal System.Windows.Controls.RadioButton rbRelevance;
        
        internal System.Windows.Controls.Button btnOk;
        
        internal System.Windows.Controls.Button btnCancel;
        
        internal Telerik.Windows.Controls.RadWindow radMapPinInfo;
        
        internal System.Windows.Controls.TextBlock txtJobRank;
        
        internal System.Windows.Controls.TextBlock txtJobTitle;
        
        internal System.Windows.Controls.TextBlock txtLocation;
        
        internal System.Windows.Controls.Button btnMapJobDetails;
        
        internal Telerik.Windows.Controls.RadBusyIndicator busyIndicator;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DOLStatsMango;component/Views/CompanySearchResults.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.lbJobSearchResults = ((Telerik.Windows.Controls.RadDataBoundListBox)(this.FindName("lbJobSearchResults")));
            this.mapJobs = ((Microsoft.Phone.Controls.Maps.Map)(this.FindName("mapJobs")));
            this.radWindowSortBy = ((Telerik.Windows.Controls.RadWindow)(this.FindName("radWindowSortBy")));
            this.rbPostedDate = ((System.Windows.Controls.RadioButton)(this.FindName("rbPostedDate")));
            this.rbPayHigh = ((System.Windows.Controls.RadioButton)(this.FindName("rbPayHigh")));
            this.rbPayLow = ((System.Windows.Controls.RadioButton)(this.FindName("rbPayLow")));
            this.rbJobTitle = ((System.Windows.Controls.RadioButton)(this.FindName("rbJobTitle")));
            this.rbCompanyName = ((System.Windows.Controls.RadioButton)(this.FindName("rbCompanyName")));
            this.rbDistance = ((System.Windows.Controls.RadioButton)(this.FindName("rbDistance")));
            this.rbRelevance = ((System.Windows.Controls.RadioButton)(this.FindName("rbRelevance")));
            this.btnOk = ((System.Windows.Controls.Button)(this.FindName("btnOk")));
            this.btnCancel = ((System.Windows.Controls.Button)(this.FindName("btnCancel")));
            this.radMapPinInfo = ((Telerik.Windows.Controls.RadWindow)(this.FindName("radMapPinInfo")));
            this.txtJobRank = ((System.Windows.Controls.TextBlock)(this.FindName("txtJobRank")));
            this.txtJobTitle = ((System.Windows.Controls.TextBlock)(this.FindName("txtJobTitle")));
            this.txtLocation = ((System.Windows.Controls.TextBlock)(this.FindName("txtLocation")));
            this.btnMapJobDetails = ((System.Windows.Controls.Button)(this.FindName("btnMapJobDetails")));
            this.busyIndicator = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("busyIndicator")));
        }
    }
}


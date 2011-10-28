﻿#pragma checksum "C:\Users\e42cgki\Desktop\WorkDocs\WindowsPhone7\DOLStatsMango\DOLStatsMango\Views\SearchAdvancedView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "ABDADDA116DE7F4DC0B1D31537C30043"
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
    
    
    public partial class SearchAdvancedView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtSearchKeyword;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtCompanyName;
        
        internal System.Windows.Controls.Grid grdLocation;
        
        internal System.Windows.Controls.Button btnFindLocation;
        
        internal Microsoft.Phone.Controls.PhoneTextBox txtLocation;
        
        internal Telerik.Windows.Controls.RadListPicker lpLocatedWithin;
        
        internal System.Windows.Controls.Grid grdPosted;
        
        internal Telerik.Windows.Controls.RadListPicker lpPostedWithin;
        
        internal System.Windows.Controls.Grid grdCategory;
        
        internal Telerik.Windows.Controls.RadListPicker lpCategory;
        
        internal System.Windows.Controls.Grid grdDegree;
        
        internal Telerik.Windows.Controls.RadListPicker lpDegree;
        
        internal System.Windows.Controls.Grid grdEmployment;
        
        internal Telerik.Windows.Controls.RadListPicker lpEmploymentType;
        
        internal System.Windows.Controls.Grid grdSalaryRange;
        
        internal Telerik.Windows.Controls.RadListPicker lpPayLow;
        
        internal Telerik.Windows.Controls.RadListPicker lpPayHigh;
        
        internal System.Windows.Controls.Grid grdSearchButtons;
        
        internal System.Windows.Controls.Button btnPreviewSearch;
        
        internal System.Windows.Controls.TextBlock txtPreviewSearch;
        
        internal System.Windows.Controls.Button btnAdvancedSearch;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DOLStatsMango;component/Views/SearchAdvancedView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.txtSearchKeyword = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtSearchKeyword")));
            this.txtCompanyName = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtCompanyName")));
            this.grdLocation = ((System.Windows.Controls.Grid)(this.FindName("grdLocation")));
            this.btnFindLocation = ((System.Windows.Controls.Button)(this.FindName("btnFindLocation")));
            this.txtLocation = ((Microsoft.Phone.Controls.PhoneTextBox)(this.FindName("txtLocation")));
            this.lpLocatedWithin = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpLocatedWithin")));
            this.grdPosted = ((System.Windows.Controls.Grid)(this.FindName("grdPosted")));
            this.lpPostedWithin = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpPostedWithin")));
            this.grdCategory = ((System.Windows.Controls.Grid)(this.FindName("grdCategory")));
            this.lpCategory = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpCategory")));
            this.grdDegree = ((System.Windows.Controls.Grid)(this.FindName("grdDegree")));
            this.lpDegree = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpDegree")));
            this.grdEmployment = ((System.Windows.Controls.Grid)(this.FindName("grdEmployment")));
            this.lpEmploymentType = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpEmploymentType")));
            this.grdSalaryRange = ((System.Windows.Controls.Grid)(this.FindName("grdSalaryRange")));
            this.lpPayLow = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpPayLow")));
            this.lpPayHigh = ((Telerik.Windows.Controls.RadListPicker)(this.FindName("lpPayHigh")));
            this.grdSearchButtons = ((System.Windows.Controls.Grid)(this.FindName("grdSearchButtons")));
            this.btnPreviewSearch = ((System.Windows.Controls.Button)(this.FindName("btnPreviewSearch")));
            this.txtPreviewSearch = ((System.Windows.Controls.TextBlock)(this.FindName("txtPreviewSearch")));
            this.btnAdvancedSearch = ((System.Windows.Controls.Button)(this.FindName("btnAdvancedSearch")));
            this.busyIndicator = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("busyIndicator")));
        }
    }
}


﻿#pragma checksum "C:\Users\e42cgki\Desktop\WorkDocs\WindowsPhone7\DOLStatsMango\DOLStatsMango\Views\JobDetailsView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "098D2EE47F6C191E6961A711C0218847"
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
    
    
    public partial class JobDetailsView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid grdOverview;
        
        internal System.Windows.Controls.TextBlock txtJobTitle;
        
        internal System.Windows.Controls.Grid grdCompany;
        
        internal System.Windows.Controls.Button btnMoreCompanyJobs;
        
        internal System.Windows.Controls.Grid grdLocation;
        
        internal System.Windows.Controls.Button btnViewLargeMap;
        
        internal Microsoft.Phone.Controls.Maps.Map mapJobLocation;
        
        internal System.Windows.Controls.Button btnNeighborhood;
        
        internal System.Windows.Controls.Grid grdPayInfo;
        
        internal System.Windows.Controls.Grid grdEducation;
        
        internal System.Windows.Controls.Grid grdJobId;
        
        internal System.Windows.Controls.Button btnIndustryStats;
        
        internal System.Windows.Controls.Grid grdDetail;
        
        internal System.Windows.Controls.Grid grdJobDescription;
        
        internal System.Windows.Controls.Grid grdJobRequirements;
        
        internal System.Windows.Controls.Grid grdContact;
        
        internal System.Windows.Controls.Grid grdContactInfo;
        
        internal System.Windows.Controls.Button btnContact;
        
        internal System.Windows.Controls.Button btnPhone;
        
        internal System.Windows.Controls.Button btnSendEmail;
        
        internal System.Windows.Controls.Button btnApply;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DOLStatsMango;component/Views/JobDetailsView.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.grdOverview = ((System.Windows.Controls.Grid)(this.FindName("grdOverview")));
            this.txtJobTitle = ((System.Windows.Controls.TextBlock)(this.FindName("txtJobTitle")));
            this.grdCompany = ((System.Windows.Controls.Grid)(this.FindName("grdCompany")));
            this.btnMoreCompanyJobs = ((System.Windows.Controls.Button)(this.FindName("btnMoreCompanyJobs")));
            this.grdLocation = ((System.Windows.Controls.Grid)(this.FindName("grdLocation")));
            this.btnViewLargeMap = ((System.Windows.Controls.Button)(this.FindName("btnViewLargeMap")));
            this.mapJobLocation = ((Microsoft.Phone.Controls.Maps.Map)(this.FindName("mapJobLocation")));
            this.btnNeighborhood = ((System.Windows.Controls.Button)(this.FindName("btnNeighborhood")));
            this.grdPayInfo = ((System.Windows.Controls.Grid)(this.FindName("grdPayInfo")));
            this.grdEducation = ((System.Windows.Controls.Grid)(this.FindName("grdEducation")));
            this.grdJobId = ((System.Windows.Controls.Grid)(this.FindName("grdJobId")));
            this.btnIndustryStats = ((System.Windows.Controls.Button)(this.FindName("btnIndustryStats")));
            this.grdDetail = ((System.Windows.Controls.Grid)(this.FindName("grdDetail")));
            this.grdJobDescription = ((System.Windows.Controls.Grid)(this.FindName("grdJobDescription")));
            this.grdJobRequirements = ((System.Windows.Controls.Grid)(this.FindName("grdJobRequirements")));
            this.grdContact = ((System.Windows.Controls.Grid)(this.FindName("grdContact")));
            this.grdContactInfo = ((System.Windows.Controls.Grid)(this.FindName("grdContactInfo")));
            this.btnContact = ((System.Windows.Controls.Button)(this.FindName("btnContact")));
            this.btnPhone = ((System.Windows.Controls.Button)(this.FindName("btnPhone")));
            this.btnSendEmail = ((System.Windows.Controls.Button)(this.FindName("btnSendEmail")));
            this.btnApply = ((System.Windows.Controls.Button)(this.FindName("btnApply")));
            this.busyIndicator = ((Telerik.Windows.Controls.RadBusyIndicator)(this.FindName("busyIndicator")));
        }
    }
}

﻿#pragma checksum "C:\Users\e42cgki\Desktop\WorkDocs\WindowsPhone7\DOLStatsMango\DOLStatsMango\Views\SettingsView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CF6FA82C89B6D10A91739B3685E1660B"
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
using Microsoft.Phone.Shell;
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


namespace DOLStatsMango.Views {
    
    
    public partial class SettingsView : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appBarIcon_Save;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appBarIcon_Cancel;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.ToggleSwitch tsUseLocation;
        
        internal System.Windows.Controls.TextBlock txtLocationMessage;
        
        internal System.Windows.Controls.TextBlock txtGpsEnabled;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/DOLStatsMango;component/Views/SettingsView.xaml", System.UriKind.Relative));
            this.appBarIcon_Save = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appBarIcon_Save")));
            this.appBarIcon_Cancel = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appBarIcon_Cancel")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tsUseLocation = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("tsUseLocation")));
            this.txtLocationMessage = ((System.Windows.Controls.TextBlock)(this.FindName("txtLocationMessage")));
            this.txtGpsEnabled = ((System.Windows.Controls.TextBlock)(this.FindName("txtGpsEnabled")));
        }
    }
}


﻿#pragma checksum "..\..\ConfigCarrera.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D4C84058242DB24303BB733FD16DF50D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Cronomur_WRI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Cronomur_WRI {
    
    
    /// <summary>
    /// ConfigCarrera
    /// </summary>
    public partial class ConfigCarrera : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 28 "..\..\ConfigCarrera.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox rs_ip;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ConfigCarrera.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox rs_port;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ConfigCarrera.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox rs_event_name;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\ConfigCarrera.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox rs_conn_timeout;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Cronomur_WRI;component/configcarrera.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ConfigCarrera.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.rs_ip = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\ConfigCarrera.xaml"
            this.rs_ip.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.rs_ip_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.rs_port = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\ConfigCarrera.xaml"
            this.rs_port.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.rs_port_TextChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.rs_event_name = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\ConfigCarrera.xaml"
            this.rs_event_name.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.rs_event_name_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.rs_conn_timeout = ((System.Windows.Controls.TextBox)(target));
            
            #line 39 "..\..\ConfigCarrera.xaml"
            this.rs_conn_timeout.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.rs_conn_timeout_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


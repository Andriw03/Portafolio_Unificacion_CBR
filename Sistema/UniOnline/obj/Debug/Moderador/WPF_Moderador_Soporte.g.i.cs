﻿#pragma checksum "..\..\..\Moderador\WPF_Moderador_Soporte.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "48730D52C32DF3DA29176E25A4B29EC803665DE631407FA24CDC3B2D05237A3A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

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
using UniOnline.Moderador;


namespace UniOnline.Moderador {
    
    
    /// <summary>
    /// WPF_Moderador_Soporte
    /// </summary>
    public partial class WPF_Moderador_Soporte : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInicio;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPerfil;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSoporte;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnVerTicket;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChat;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/moderador/wpf_moderador_soporte.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
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
            this.btnInicio = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
            this.btnInicio.Click += new System.Windows.RoutedEventHandler(this.btnInicioMod);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnPerfil = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
            this.btnPerfil.Click += new System.Windows.RoutedEventHandler(this.btnPerfilMod);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnSoporte = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
            this.btnSoporte.Click += new System.Windows.RoutedEventHandler(this.btnSoporteMod);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnVerTicket = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
            this.btnVerTicket.Click += new System.Windows.RoutedEventHandler(this.btnVerTicket_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnChat = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\Moderador\WPF_Moderador_Soporte.xaml"
            this.btnChat.Click += new System.Windows.RoutedEventHandler(this.btnVerTicket_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


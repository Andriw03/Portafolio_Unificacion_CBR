﻿#pragma checksum "..\..\..\Director\WPF_MainDirector.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "524826A6D21A1B06094E611FB2B2E87878B24469D551A1579615085687DAF027"
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
using UniOnline.Trabajador;


namespace UniOnline.Director {
    
    
    /// <summary>
    /// WPF_MainDirector
    /// </summary>
    public partial class WPF_MainDirector : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInicio;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIngresarUsuario;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGestionUsuario;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGestionInforme;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrarSesion;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MyTime;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock datelbl;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblSaludo;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Director\WPF_MainDirector.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LblMotivacion;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/director/wpf_maindirector.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Director\WPF_MainDirector.xaml"
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
            
            #line 8 "..\..\..\Director\WPF_MainDirector.xaml"
            ((UniOnline.Director.WPF_MainDirector)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnInicio = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Director\WPF_MainDirector.xaml"
            this.btnInicio.Click += new System.Windows.RoutedEventHandler(this.btnInicio_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnIngresarUsuario = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Director\WPF_MainDirector.xaml"
            this.btnIngresarUsuario.Click += new System.Windows.RoutedEventHandler(this.btnIngresarUsuario_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnGestionUsuario = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Director\WPF_MainDirector.xaml"
            this.btnGestionUsuario.Click += new System.Windows.RoutedEventHandler(this.btnGestionUsuario_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnGestionInforme = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Director\WPF_MainDirector.xaml"
            this.btnGestionInforme.Click += new System.Windows.RoutedEventHandler(this.btnGestionInforme_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnCerrarSesion = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Director\WPF_MainDirector.xaml"
            this.btnCerrarSesion.Click += new System.Windows.RoutedEventHandler(this.btnCerrarSesion_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.MyTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.datelbl = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.LblSaludo = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.LblMotivacion = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C7BA0B162F4E8E45295A53D1D417D2170013131C3AFB39BD3731C7CD7E0B71A7"
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
using UniOnline.Recepcion;


namespace UniOnline.Director {
    
    
    /// <summary>
    /// WPF_SolicitudesTrabajadores
    /// </summary>
    public partial class WPF_SolicitudesTrabajadores : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 56 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_consultar;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_listarFormulario;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInicio;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrarSesion;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblBienvenidoRec;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblB;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPerfil;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/director/wpf_solicitudestrabajadores.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
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
            this.btn_consultar = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            this.btn_consultar.Click += new System.Windows.RoutedEventHandler(this.btn_consultar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dg_listarFormulario = ((System.Windows.Controls.DataGrid)(target));
            
            #line 60 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            this.dg_listarFormulario.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dg_listartramite_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnInicio = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            this.btnInicio.Click += new System.Windows.RoutedEventHandler(this.Button_Inicio);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnCerrarSesion = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            this.btnCerrarSesion.Click += new System.Windows.RoutedEventHandler(this.btnCerrarSesion_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblBienvenidoRec = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblB = ((System.Windows.Controls.Label)(target));
            
            #line 88 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            this.lblB.ContextMenuClosing += new System.Windows.Controls.ContextMenuEventHandler(this.lblB_ContextMenuClosing);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnPerfil = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            this.btnPerfil.Click += new System.Windows.RoutedEventHandler(this.btnPerfil_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 3:
            
            #line 70 "..\..\..\..\Director\WPF_SolicitudesTrabajadores.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnDetalles_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}


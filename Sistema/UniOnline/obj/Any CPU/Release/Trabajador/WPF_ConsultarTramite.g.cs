﻿#pragma checksum "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "093EFB6CEE3EB202673FAB8F8EA35588A0C1968EA380016785952C4051063872"
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


namespace UniOnline.Trabajador {
    
    
    /// <summary>
    /// WPF_ConsultarTramite
    /// </summary>
    public partial class WPF_ConsultarTramite : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 45 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtRut;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_consultar;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_listartramite;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNumeroSeguimiento;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_Volver;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/trabajador/wpf_consultartramite.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
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
            this.txtRut = ((System.Windows.Controls.TextBox)(target));
            
            #line 45 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
            this.txtRut.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtRut_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_consultar = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
            this.btn_consultar.Click += new System.Windows.RoutedEventHandler(this.btn_consultar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dg_listartramite = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.txtNumeroSeguimiento = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btn_Volver = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
            this.btn_Volver.Click += new System.Windows.RoutedEventHandler(this.btn_Volver_Click);
            
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
            case 4:
            
            #line 63 "..\..\..\..\Trabajador\WPF_ConsultarTramite.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnEditar_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}


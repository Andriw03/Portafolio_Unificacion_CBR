﻿#pragma checksum "..\..\..\Recepcion\WPF_Recepcion.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A95743E10AF79348D823666FA765A8F5EE5B9DB0C4BFC0829A5E9B52E2046D1C"
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


namespace UniOnline.Recepcion {
    
    
    /// <summary>
    /// WPF_Recepcion
    /// </summary>
    public partial class WPF_Recepcion : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Recepcion\WPF_Recepcion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_buscar;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Recepcion\WPF_Recepcion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_consultar;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Recepcion\WPF_Recepcion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_listartramite;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Recepcion\WPF_Recepcion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInicio;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Recepcion\WPF_Recepcion.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInicio_Copy;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/recepcion/wpf_recepcion.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Recepcion\WPF_Recepcion.xaml"
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
            this.txt_buscar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.btn_consultar = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Recepcion\WPF_Recepcion.xaml"
            this.btn_consultar.Click += new System.Windows.RoutedEventHandler(this.btn_consultar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.dg_listartramite = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.btnInicio = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Recepcion\WPF_Recepcion.xaml"
            this.btnInicio.Click += new System.Windows.RoutedEventHandler(this.Button_Inicio);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnInicio_Copy = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Recepcion\WPF_Recepcion.xaml"
            this.btnInicio_Copy.Click += new System.Windows.RoutedEventHandler(this.Button_Inicio);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


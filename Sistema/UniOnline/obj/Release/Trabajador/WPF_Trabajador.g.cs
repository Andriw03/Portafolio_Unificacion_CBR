﻿#pragma checksum "..\..\..\Trabajador\WPF_Trabajador.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A57A9375349BD0414896E19770054C78326BEC957DAFFB2EC727D656897A7D42"
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
    /// WPF_Trabajador
    /// </summary>
    public partial class WPF_Trabajador : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Trabajador\WPF_Trabajador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInicio;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Trabajador\WPF_Trabajador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGestionProp;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Trabajador\WPF_Trabajador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGestionTra;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Trabajador\WPF_Trabajador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConsultaTra;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Trabajador\WPF_Trabajador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MyTime;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Trabajador\WPF_Trabajador.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock datelbl;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/trabajador/wpf_trabajador.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Trabajador\WPF_Trabajador.xaml"
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
            
            #line 8 "..\..\..\Trabajador\WPF_Trabajador.xaml"
            ((UniOnline.Trabajador.WPF_Trabajador)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnInicio = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Trabajador\WPF_Trabajador.xaml"
            this.btnInicio.Click += new System.Windows.RoutedEventHandler(this.Button_Inicio);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnGestionProp = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Trabajador\WPF_Trabajador.xaml"
            this.btnGestionProp.Click += new System.Windows.RoutedEventHandler(this.Button_Gestion_Prop);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnGestionTra = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\Trabajador\WPF_Trabajador.xaml"
            this.btnGestionTra.Click += new System.Windows.RoutedEventHandler(this.Button_Gestion_Tra);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnConsultaTra = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Trabajador\WPF_Trabajador.xaml"
            this.btnConsultaTra.Click += new System.Windows.RoutedEventHandler(this.Button_Consulta_Tra);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MyTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.datelbl = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            
            #line 20 "..\..\..\Trabajador\WPF_Trabajador.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Conect);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


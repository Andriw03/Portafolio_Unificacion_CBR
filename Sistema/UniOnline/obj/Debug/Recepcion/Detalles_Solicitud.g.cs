﻿#pragma checksum "..\..\..\Recepcion\Detalles_Solicitud.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "55D0101377481C8F511AD87664337B2E073A30F227B0EA62A9CFB4969FD37541"
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
    /// Detalles_Solicitud
    /// </summary>
    public partial class Detalles_Solicitud : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl1;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCerrar;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNombreCompleto;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFechaSoli;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblFechaCierre;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblEstado;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblIdSoli;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNseg;
        
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
            System.Uri resourceLocater = new System.Uri("/UniOnline;component/recepcion/detalles_solicitud.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
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
            this.lbl1 = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.btnCerrar = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Recepcion\Detalles_Solicitud.xaml"
            this.btnCerrar.Click += new System.Windows.RoutedEventHandler(this.btnCerrar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lblNombreCompleto = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblFechaSoli = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lblFechaCierre = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblEstado = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.lblIdSoli = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.lblNseg = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


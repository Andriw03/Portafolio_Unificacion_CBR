﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UniOnline
{
    /// <summary>
    /// Lógica de interacción para WPF_Trabajador.xaml
    /// </summary>
    public partial class WPF_Trabajador : Window
    {
        public WPF_Trabajador()
        {
            InitializeComponent();
            startclock();
        }

        private void startclock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickevent;
            timer.Start();
        }

        private void tickevent(object sender, EventArgs e)
        {
            MyTime.Text = DateTime.Now.ToString("T");
            datelbl.Text = DateTime.Now.ToString("d");
        }


        private void Button_Inicio(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Gestion_Prop(object sender, RoutedEventArgs e)
        {
            
            WPF_Trabajador_Prop tprop = new WPF_Trabajador_Prop();
            tprop.ShowDialog();
        }

        private void Button_Gestion_Tra(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Consulta_Tra(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }

}

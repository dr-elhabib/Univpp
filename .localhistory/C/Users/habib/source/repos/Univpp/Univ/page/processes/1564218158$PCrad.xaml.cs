﻿using Univ.modelview;
using System;
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

namespace Univ.page
{
    /// <summary>
    /// Interaction logic for PCrad.xaml
    /// </summary>
    public partial class PCrad : Page
    {
        public PCrad()
        {
            InitializeComponent();
            this.DataContext = new processesViewModel();
        }

        private void Ser_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
    
}
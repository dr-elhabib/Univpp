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
using Univ.modelview;

namespace Univ.page.lib
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class Messagebox : UserControl
    {
        public Messagebox(string s, Action yes,Action no)
        {
            InitializeComponent();
            this.DataContext = new MessageboxModelView(s,yes,no);
           
        }
    }
}

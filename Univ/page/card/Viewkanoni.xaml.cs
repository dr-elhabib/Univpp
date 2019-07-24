﻿using Univ.modeldb;
using Univ.modelview;
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
    /// Interaction logic for View7isabi.xaml
    /// </summary>
    public partial class Viewkanoni : Page
    {
        public Viewkanoni(card_kanoni card )
        {
            InitializeComponent();
            this.DataContext = new ViewkanoniViewModel(card);
        }
    }
}
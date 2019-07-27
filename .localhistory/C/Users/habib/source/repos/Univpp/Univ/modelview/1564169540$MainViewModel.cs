using Univ.lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.page;

namespace Univ
{
    class MainViewModel:BaseViewModel {
        public static Page page { get; set; } = new PCrad();

        public MainViewModel() {

        }
    }
}


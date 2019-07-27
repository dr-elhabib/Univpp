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
using Univ.lib;

namespace Univ.page
{
    /// <summary>
    /// Interaction logic for PCrad.xaml
    /// </summary>
    public partial class PCrad : Page,MPage
    {
        private processesViewModel MD  {get;set;}

        public PCrad()
        {
           MD = new processesViewModel();
            InitializeComponent();
            this.DataContext = MD;
        }
        public Action Reload { get => MD.inTilData  ; set => Reload = value; }

    }

}
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
using Univ.modelview;

namespace Univ.page.lib
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class Messagebox : UserControl
    {
        public Messagebox(List<string> s, Action OK)
        {
            InitializeComponent();
            this.DataContext = new MessageboxModelView(s,OK);
           
        }
    }
}

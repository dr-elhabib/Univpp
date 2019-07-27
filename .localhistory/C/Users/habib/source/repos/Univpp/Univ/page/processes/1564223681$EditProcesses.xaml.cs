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
using Univ.modeldb;

namespace Univ.page
{
    /// <summary>
    /// Interaction logic for NewProcesses.xaml
    /// </summary>
    public partial class EditProcesses : Page
    {
        public EditProcesses(process process)
        {
            InitializeComponent();
            this.DataContext = new EditprocessesViewModel(process);
        }

        private void ComboBox_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}

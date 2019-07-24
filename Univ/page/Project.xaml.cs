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
using Univ.Model;

namespace Univ
{
    /// <summary>
    /// Interaction logic for Project.xaml
    /// </summary>
    public partial class Project : Page
    {
        public Project()
        {
            InitializeComponent();
            this.DataContext = new processesViewModel();
            var db = new db_univ();
            var items = db.processes.ToList();
           // table.ItemsSource = items;
        }
    }
}

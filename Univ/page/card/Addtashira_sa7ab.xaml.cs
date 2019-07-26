using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview;
using Univ.modelview.lib;
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
    /// Interaction logic for AddPartCard.xaml
    /// </summary>
    public partial class Addtashira_sa7ab : UserControl
    {


        public Addtashira_sa7ab(card_sa7ab card_sa7ab, Action accept, Action Cancel)
        {
            InitializeComponent();
            this.DataContext = new Addtashira_sa7ab_VewModel(card_sa7ab) {
                acc=accept,
                con=Cancel
            };
        }

        
    }
}

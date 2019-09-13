using Univ.lib;
using Univ.modeldb;
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

namespace Univ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //       univdb db = new univdb();
            //  MessageBox.Show((db.processes.ToList())[0].date + "");
            ContentApp contentApp = new ContentApp(this);
            if (Ico.getValue<ContentApp>() ==null)
            {
                Ico.setValue<ContentApp>(contentApp);


            }
            else {

                Ico.ResetValue<ContentApp>(contentApp);
            }
            this.DataContext = Ico.getValue<ContentApp>();

        }

        private void FullScreenButton(object sender, RoutedEventArgs e)
        {
            this.WindowState ^= WindowState.Maximized;
        }
        private void Power(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void ResizeButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

       
    }
}

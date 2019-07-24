using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Univ
{
    public class WindowModelView: INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public ICommand command { get; set; }
        public double Width { get; set; } = 900;
        public double Heigth { get; set; } = 600;
        public double ButtonHeigth { get; set; } = 33;
        public double Menu { get; set; } = 200;

        public Page CurrentPage { get; set; } = new Project();
        public  WindowModelView(Window window) {
            //window.WindowState = WindowState.Maximized;
            window.SizeChanged += SizeChanged;

            command = new Command(()=> {
                CurrentPage = new addproject();
                MessageBox.Show("donne");
            });

        }

        private void SizeChanged(object sender, SizeChangedEventArgs e)
        {

            Menu =  e.NewSize.Width * 0.18;
            ButtonHeigth =  e.NewSize.Height * 0.055;
        }
           
    }
}

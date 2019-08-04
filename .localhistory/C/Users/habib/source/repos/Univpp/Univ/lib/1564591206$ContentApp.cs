using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.page.lib;

namespace Univ.lib
{
    class ContentApp : BaseViewModel<Page>
    {
        private Page _page { get; set; } = new ViewClient();

        public Page page
        {
            set
            {
                var p = new Centent(prev, _page);
                prev = p;
                _page = value;
            }
            get
            {
                return _page;
            }

        }
        public Centent prev { get; set; } = null;
        public double menuWidth { get; set; } = 250;
        public double contentHeigth { get; set; } 
        public double contentWidth { get; set; } 
        public Window window { get; set; }

        public Command btnclose { get; set; } 
        public Command btnmenu { get; set; } 

        public Visibility vismenu { get; set; } 
        public Visibility visbtn { get; set; } 
        public Visibility visbtnclose { get; set; } 

        public ContentApp(Window window)
        {
            this.btnmenu = new Command(() => {
                this.vismenu = Visibility.Visible;
                contentWidth = window.Width - 250;
                this.visbtn = Visibility.Hidden;
                MessageBox.Show("" + menuWidth);
            });
            this.btnclose = new Command(() => {
                this.vismenu = Visibility.Hidden;
                contentWidth = window.Width ;
                this.visbtn = Visibility.Visible;
                MessageBox.Show("" + menuWidth);
            });
            this.window = window;
            contentHeigth = window.Height;
            contentWidth = window.Width-250;
            window.SizeChanged += SizeChanged;
            this.vismenu = Visibility.Collapsed;
            this.visbtn = Visibility.Visible;

        }
        public void SetPage(Page p) {
            this._page = p;
        }
       private void SizeChanged(object sender, SizeChangedEventArgs e) {
            contentWidth = window.Width;
            contentHeigth = window.Height;
            this.visbtn = Visibility.Visible;
            this.vismenu = Visibility.Hidden;
            this.visbtnclose = Visibility.Visible;

            if (window.Width > 1190)
            {
                contentWidth = window.Width-250;
                this.visbtn = Visibility.Hidden;
                this.vismenu = Visibility.Visible;
                this.visbtnclose = Visibility.Collapsed;
            }
        }
        public void back()
        {

            if (prev != null)
            {
                _page = prev.curent;

                if (_page is MPage) {
                    (_page as MPage).Reload();
                }

                prev = prev.prev;

            }

        }
        public void clear()
        {
            prev = null;
        }

        
        public class Centent
        {
            public Centent prev { get; set; }
            public Page curent { get; set; }

            public Centent(Centent prev, Page Curent)
            {
                this.prev = prev;
                this.curent = Curent;
            }

        }

        public bool IsSample4DialogOpen
        {
            get { return _isSample4DialogOpen; }
            set
            {
                if (_isSample4DialogOpen == value) return;
                _isSample4DialogOpen = value;
            }
        }

        public UserControl Sample4Content
        {
            get { return _sample4Content; }
            set
            {
                if (_sample4Content == value) return;
                _sample4Content = value;
            }
        }
        private bool _isSample4DialogOpen;
        private UserControl _sample4Content;


        public void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        public void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
        }

        public void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }


    }
}

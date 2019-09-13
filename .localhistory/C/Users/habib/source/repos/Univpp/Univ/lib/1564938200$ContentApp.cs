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
        private Page _page { get; set; } = new welcomme();

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


        public Command HOME { get; set; } 
        public Command clients { get; set; } 
        public Command processes { get; set; } 
        public Command account { get; set; } 
        public Command chrts { get; set; } 
        public Command settings { get; set; } 
        public Command logout { get; set; } 

        public Visibility vismenu { get; set; } 
        public Visibility visbtn { get; set; } 
        public Visibility visbtnclose { get; set; } 

        public ContentApp(Window window)

        {
            HOME = new Command(() => {
                page = new welcomme();
                prev = null;
            });
            account = new Command(() => {
                page = new ViewAcount();
                prev = null;
            });
            logout = new Command(() => {
                page = new ViewAcount();
                prev = null;
            });
            clients = new Command(() => {
                page = new ViewClient();
                prev = null;
            });
            processes = new Command(() => {
                page = new PCrad();
                prev = null;

            });
            this.btnmenu = new Command(() => {
                this.vismenu = Visibility.Visible;
                contentWidth = window.Width - 250;
                this.visbtn = Visibility.Hidden;
            });
            this.btnclose = new Command(() => {
                this.vismenu = Visibility.Hidden;
                contentWidth = window.Width ;
                this.visbtn = Visibility.Visible;
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

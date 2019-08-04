using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.lib
{
    class ContentApp : BaseViewModel<Page>
    {
        private Page _page { get; set; } = new PCrad();

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
             //   MessageBox.Show(_page.ToString());
                return _page;
            }

        }
        public Centent prev { get; set; } = null;
        public double menuWidth { get; set; } = 250;
        public double contentHeigth { get; set; } 
        public double contentWidth { get; set; } 
        public Window window { get; set; } 

        public Visibility vismenu { get; set; } 

        public ContentApp(Window window)
        {
            this.window = window;
            contentHeigth = window.Height;
            contentWidth = window.Width-250;
            window.SizeChanged += SizeChanged;
            this.vismenu = Visibility.Collapsed;

        }
        public void SetPage(Page p) {
            this._page = p;
        }
       private void SizeChanged(object sender, SizeChangedEventArgs e) {
            contentWidth = window.Width-250;
            contentHeigth = window.Height;
        }
        public void back()
        {

            if (prev != null)
            {

                page = prev.curent;
                if (page is MPage) {
                    (page as MPage).Reload();
                }
                prev = prev.prev;

            }

        }
        public void clear()
        {
            prev = null;
        }

        /*
        public void ReloadANDback()
        {

            if (prev != null)
            {

                page =  prev.curent;
                prev = prev.prev;

            }
        }*/
        // TODO::THIS FUNCTION DONT FORGET  IT

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
        

    }
}

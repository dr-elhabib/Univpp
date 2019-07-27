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

        public ContentApp()
        {


        }
        public void SetPage(Page p) {
            this._page = p;
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

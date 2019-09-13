using Univ.lib;
using Univ.modeldb;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Univ.modelview
{
    public class MessageboxModelView : BaseViewModel<string>
    {
        public List<Message> Mes { set; get; }
        public Command ok { get; set; }
        public string imoji { get; set; } = null;
        public SolidColorBrush brush { get; set; } 
        public MessageboxModelView(List<string> s,Action okaction,bool happy=false)
        {
            var color = System.Drawing.Color.Orange;
            brush = new SolidColorBrush(Color.FromArgb(color.A,color.R,color.G,color.B));
            this.Mes = s.Select(sr => new Message() { message= "- "+sr}).ToList() ;
            if (happy)
            {

                 color = System.Drawing.Color.Green;
                imoji = "SmileyHappy";
                brush = new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));


            }
            this.ok = new Command(()=> {
                okaction();
            });

        }

     public class Message
        {

          public  string message { get; set; }

        } 
    }
    

}

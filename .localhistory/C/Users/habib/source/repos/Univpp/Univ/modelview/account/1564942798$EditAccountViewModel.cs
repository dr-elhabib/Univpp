using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.lib;
using Univ.modeldb;
using Univ.page.lib;

namespace Univ.modelview
{
   public class EditAccountViewModel : BaseViewModel<user>
    {
        public string name { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Repassword { get; set; }
        public Command save { get; set; }
        public EditAccountViewModel(user user) {

            name = user.name;
            username = user.username;
            save = new Command(() => {
            if (Repassword.Equals(password))
            {
                Ico.getValue<ContentApp>().OpenSample4Dialog();
                Ico.getValue<ContentApp>().AcceptSample4Dialog();

                Ico.getValue<db>().GetUnivdb().users.ToList().Where(c => c.id == Ico.getValue<user>().id).ToList().FirstOrDefault().name = name;
                Ico.getValue<db>().GetUnivdb().users.ToList().Where(c => c.id == Ico.getValue<user>().id).ToList().FirstOrDefault().username = username;
                Ico.getValue<db>().GetUnivdb().users.ToList().Where(c => c.id == Ico.getValue<user>().id).ToList().FirstOrDefault().passoword = password;
                Ico.getValue<db>().GetUnivdb().users.ToList().Where(c => c.id == Ico.getValue<user>().id).ToList().FirstOrDefault().date_edit = DateTime.Now;
                Ico.getValue<db>().savedb();
                Ico.getValue<ContentApp>().CancelSample4Dialog();

            }
            else
            {

                Ico.getValue<ContentApp>().OpenSample4Dialog();
                Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "" },
                Ico.getValue<ContentApp>().OpenSample4Dialog);}
            });

        }
      
    }
}

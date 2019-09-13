using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.lib;
using Univ.modeldb;

namespace Univ.modelview
{
   public class EditAccountViewModel : BaseViewModel<user>
    {
        public string name { get; set; }
        public string username { get; set; }
        public string date_gust { get; set; }
        public string date_edit { get; set; }
        public EditAccountViewModel(user user) {

            this.actionUP = () => {
                val = Ico.getValue<db>().GetUnivdb().users.ToList().Where(c => c.id == Ico.getValue<user>().id).ToList().FirstOrDefault();
            };
            inTilData();

            name = val.name;
            username = val.username;
            date_gust = val.date_gust.ToString();
            date_edit = val.date_edit.ToString();
        }
       public void inTilData() {
            actionUP();

        }
    }
}

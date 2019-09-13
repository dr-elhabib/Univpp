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

            name = user.name;
            username = user.username;

        }
      
    }
}

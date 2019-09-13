using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.lib;
using Univ.modeldb;

namespace Univ.modelview
{
    public class loginModelView:BaseViewModel<user>
    {
        public CommandPar login { get; set; }
    public  loginModelView() {
            login = new CommandPar((p)=> {

                var passwordBox = p as PasswordBox ;
                var password = passwordBox.Password;
                MessageBox.Show(password);
            });
        }
    }
}

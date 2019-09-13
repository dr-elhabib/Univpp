using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.lib;
using Univ.modeldb;
using Univ.page.lib;

namespace Univ.modelview
{
    public class loginModelView:BaseViewModel<user>
    {
        public string username { get; set; }
        public Visibility erour { get; set; } = Visibility.Collapsed;
        public CommandPar login { get; set; }
    public  loginModelView() {
            login = new CommandPar((p)=> {

                var passwordBox = p as PasswordBox ;
                var password = passwordBox.Password;
                var user = Ico.getValue<db>().GetUnivdb().users.ToList().Where(u => u.username.Equals(username) && u.passoword.Equals(password)).ToList().FirstOrDefault();
                MessageBox.Show(user+"");
                if (user == null)
                {
                    erour = Visibility.Visible;

                }
                else
                {
                    erour = Visibility.Collapsed;

                    Ico.setValue<user>(user);
                    App.Current.MainWindow = new MainWindow();

                    App.Current.MainWindow.Show();
                    Ico.getValue<ContentApp>().OpenSample4Dialog();
                    Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "أهلا وسهلا بك  يا " + user.name }, Ico.getValue<ContentApp>().CancelSample4Dialog);
                }
            });
        }
    }
}

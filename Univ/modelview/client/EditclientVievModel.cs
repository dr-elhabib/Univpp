using Univ.lib;
using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.page.lib;

namespace Univ.modelview
{
    class EditclientVievModel : BaseViewModel<client>
    {
        public string Name { get; set; } = "";
        public string numaccount { get; set; } = "";
        public string agence { get; set; } = "";
        public string bank { get; set; } = "";
        public string address { get; set; } = "";
        List<string> erour = new List<string>();

        public Action action { get; set; }
        public Command save { get; set; }
        public Command back { get; set; }
        public EditclientVievModel(client client) {
            Name = client.Name;
            agence = client.gence;
            numaccount = client.num_account;
            bank = client.bank;
            address = client.address;
               save = new Command(async() => {

               erour = new List<string>();

               if (Name.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة إسم العميل");

               }




               if (numaccount.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة رقم الحساب   ");

               }


               if (agence.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة الوكالة    ");

               }

               if (bank.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة البنك    ");

               }
               if (address.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة العنوان    ");

               }
                   Ico.getValue<ContentApp>().OpenSample4Dialog();

                   if (erour.Count == 0)
                   {
                       Ico.getValue<ContentApp>().AcceptSample4Dialog();

                       await Task.Run(() =>
                       {

                           var c = Ico.getValue<db>().GetUnivdb().clients.ToList().Where(cl => cl.Id == client.Id).FirstOrDefault();


                           c.Name = Name;
                           c.address = address;
                           c.bank = bank;
                           c.gence = agence;
                           c.num_account = numaccount;
                           Ico.getValue<db>().savedb();
                           Ico.getValue<ContentApp>().CancelSample4Dialog();
                           action();
                           Ico.getValue<ContentApp>().back();
                       });
                   }
                   else {

                       Ico.getValue<ContentApp>().Sample4Content = new Messagebox(erour, () => {
                           Ico.getValue<ContentApp>().CancelSample4Dialog();

                       });

                   }
               });
            back = new Command(() => {
                Ico.getValue<ContentApp>().back();

            });
        }

        

    }
}

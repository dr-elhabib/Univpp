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
               OpenSample4Dialog();

                   if (erour.Count == 0)
                   {
                       AcceptSample4Dialog();

                       await Task.Run(() =>
                       {

                           var c = Ico.getValue<db>().GetUnivdb().clients.ToList().Where(cl => cl.Id == client.Id).FirstOrDefault();


                           c.Name = Name;
                           c.address = address;
                           c.bank = bank;
                           c.gence = agence;
                           c.num_account = numaccount;
                           
                           Ico.getValue<db>().savedb();
                           CancelSample4Dialog();

                           Ico.getValue<ContentApp>().back();
                       });
                   }
                   else {

                       Sample4Content = new Messagebox(erour, () => {
                           CancelSample4Dialog();

                       });

                   }
               });
            back = new Command(() => {
                Ico.getValue<ContentApp>().back();

            });
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


        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }


    }
}

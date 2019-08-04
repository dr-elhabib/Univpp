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
    class AddclientVievModel : BaseViewModel<client>
    {
        public string Name { get; set; }
        public string numaccount { get; set; }
        public string agence { get; set; }
        public string bank { get; set; }
        public string address { get; set; }
        List<string> erour = new List<string>();

        public Command save { get; set; }
        public AddclientVievModel() {

               save = new Command(() => {

               erour = new List<string>();

               if (name.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة إسم الحصة ");

               }
               string pattern = "[1-9]+";
               Regex rgx = new Regex(pattern);
               if (!rgx.IsMatch(cost.ToString()))
               {
                   erour.Add("الرجاء كتابة المبلغ ");

               }



               if (alhcost.ToString().Length == 0)
               {
                   erour.Add("الرجاء كتابة  المبلغ حرفيا   ");

               }
               if (numtype == null)
               {
                   erour.Add("الرجاء  إختيار نوع الحصة    ");

               }
               OpenSample4Dialog();

               if (erour.Count == 0)
               {
                   AcceptSample4Dialog();

                   await Task.Run(() =>
                   {


                       var client = new client()
                    {
                        Name = Name,
                        address = address,
                        bank = bank,
                        gence = agence,
                        num_account = numaccount
                    };
                   Ico.getValue<db>().GetUnivdb().clients.Add(client);
                   Ico.getValue<db>().savedb();
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

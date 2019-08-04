using Univ.lib;
using Univ.lib.Enum;
using Univ.modeldb.model;
using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Univ.page;
using Univ.page.lib;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Univ.modelview
{
    class Editdafa3VewModel : BaseViewModel<card_dafa3>
    {

        public part part { get; set; }

        public double Costd { get; set; }
        public string AlCost { get; set; }
        public string tswiya { get; set; }

        public string namepro { get; set; }
        public string namepart { get; set; }
        public double cost { get; set; }
        public string nameclient { get; set; }
        public string bankclient { get; set; }
        public string codebankclient { get; set; }
        public UserControl THIS { get; set; }

        public Command Cancelcommand { get; set; }

        public Command savecommand { get; set; }
        public Command back{get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        public List<string> erour  { set; get; }

        public Editdafa3VewModel(card_dafa3 card_dafa3)
        {
            part = card_dafa3.part;
            this.namepro = card_dafa3.part.process.Name;
            this.cost = card_dafa3.part.Cost;
            this.namepart = part.Name;
            var client = card_dafa3.part.card_kanoni.ToList().FirstOrDefault().client;
            this.nameclient = client.Name;
            this.codebankclient = client.num_account;
            this.bankclient = client.bank;
            this.Costd = card_dafa3.Cost;
            this.AlCost = card_dafa3.alcost;
            this.tswiya = card_dafa3.tswiya;
            savecommand = new Command( () =>
            {
                if (Costd != card_dafa3.Cost|| AlCost!=card_dafa3.alcost|| tswiya != card_dafa3.tswiya) { 
            erour = new List<string>();


            string pattern = "[0-9]+";
            Regex rgx = new Regex(pattern);
            if (Costd == 0 || !rgx.IsMatch(cost.ToString()))
            {
                erour.Add("الرجاء كتابة المبلغ ");

            }
            else
            {
                if (!((part.mcost - part.nowcost) >= Costd))
                {
                    erour.Add("المبلغ أكبر من الرصيد المتاح");
                }

            }



            if (AlCost.ToString().Length == 0)
            {
                erour.Add("الرجاء كتابة  المبلغ حرفيا   ");

            }
            if (tswiya.ToString().Length == 0)
            {
                erour.Add("الرجاء كتابة   تسوية الفاتوؤة   ");

            }
            Ico.getValue<ContentApp>().OpenSample4Dialog();
                    if (erour.Count != 0)
                    {
                        
                        Ico.getValue<ContentApp>().Sample4Content = new Messagebox(erour, () =>
                        {

                            Ico.getValue<ContentApp>().Sample4Content = THIS;
                        });

                    }
                    else
                    {

                        var d = 0d;
                        foreach (var c in part.card_mo7sabi.ToList())
                        {
                            d += c.cost;
                        }
                        var d2 = 0d;
                        foreach (var c in part.card_dafa3.ToList().Where(c => c.num < card_dafa3.num))
                        {
                            d2 += c.Cost;
                        }
                        Ico.getValue<ContentApp>().OpenSample4Dialog();

                        if ((d - d2) > Costd)
                        {
                            var t = (card_dafa3.Cost - Costd);
                            Ico.getValue<ContentApp>().AcceptSample4Dialog();
                            Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == ct.id_part).ToList().SingleOrDefault().nowcost -= t;
                            Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == ct.id_part).ToList().SingleOrDefault().mcost += t;
                            MessageBox.Show(t + "");

                            Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).FirstOrDefault().Cost = Costd;
                            Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).FirstOrDefault().tswiya = tswiya;
                            Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == card_dafa3.Id).FirstOrDefault().alcost = AlCost;
                            Ico.getValue<db>().savedb();
                            acc();
                            Ico.getValue<ContentApp>().CancelSample4Dialog();
                        }
                        else
                        {
                            Ico.getValue<ContentApp>().Sample4Content = new Messagebox(
                                new List<string> { " المبلغ أكبر من الرصيد المتاح " }, Ico.getValue<ContentApp>().CancelSample4Dialog);

                        }
                    }
                }
            });
            back = new Command(()=> {
                Ico.getValue<ContentApp>().back();
            });

            Cancelcommand = new Command(() => {
                Ico.getValue<ContentApp>().CancelSample4Dialog();

            });
        }
    }

}
    


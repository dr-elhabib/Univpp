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
using System.Windows.Controls;
using System.Text.RegularExpressions;
using Univ.page.lib;

namespace Univ.modelview
{
    class Addmo7asabiVewModel : BaseViewModel<card_kanoni>
    {

        public part part { get; set; }

        public double Cost { get; set; }
        private List<string> erour = new List<string>();

        public string namepro { get; set; }
        public string namepart { get; set; }
        public string client { get; set; }
        public string subject { get; set; }
        public double cost { get; set; }
        public ViewMo7sabiViewModel Sample4Contentviw { get; set; }
        public UserControl THIS;
        public Command savecommand { get; set; }
        public Command Cancelcommand { get; set; }
        public client ClientSelected { get; set; }
        public Action acc { set; get; }
        public Action con { set; get; }
        int numm;
        public Addmo7asabiVewModel(card_kanoni card_kanoni)
        {
            part = card_kanoni.part;
            this.namepro = card_kanoni.part.process.Name;
            this.cost = card_kanoni.cost;
            this.namepart = part.Name;
            this.client = card_kanoni.client.Name;
            //  var carda = Ico.getValue<db>().GetUnivdb().years.Where(y => y.year1.Year == DateTime.Now.Year).ToList().FirstOrDefault().cards.ToList().Where(c => c.id_prosess == card_kanoni.part.Id_Pro)
            //    .ToList().FirstOrDefault();

            var carda = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => (c.card.year == Ico.getValue<Date>().GetNowDate().Id) && c.id_part == card_kanoni.id_part)
                .ToList().OrderByDescending(c => c.num).ToList().FirstOrDefault();
            //.card_mo7sabi.Where(c=>c.id_part== card_kanoni.id_part).OrderByDescending(c=>c.num).LastOrDefault();

             numm = 1;
            if (carda != null)
            {
                numm = carda.num + 1;
            }
            var nums = (numm.ToString().Length == 1) ? "0" + numm.ToString() : numm.ToString();
            this.subject = "الإلتزام المحاسبي رقم " + nums + " للعقد المتعلق بالعملية " + part.process.Name;

            savecommand = new Command(() =>
            {



                erour = new List<string>();


                string pattern = "[0-9]+";
                Regex rgx = new Regex(pattern);
                if (Cost == 0 || !rgx.IsMatch(cost.ToString()))
                {
                    erour.Add("الرجاء كتابة المبلغ ");

                }
                else
                {
                    if (!((part.Cost - part.mcost) >= Cost))
                    {
                        erour.Add("المبلغ أكبر من الرصيد المتاح");
                    }

                }



                if (subject.ToString().Length == 0)
                {
                    erour.Add("الرجاء كتابة  موضوع البطاقة    ");

                }

                Ico.getValue<ContentApp>().OpenSample4Dialog();
                if (erour.Count != 0)
                {


                    Ico.getValue<ContentApp>().Sample4Content=new Messagebox(erour, () => {

                        Ico.getValue<ContentApp>().Sample4Content = THIS;
                    });

                }
                else
                {
                    Ico.getValue<ContentApp>().AcceptSample4Dialog();
                    Creat_card(card_kanoni);
                    
                }

                
            });
            Cancelcommand = new Command(() =>
            {
                Ico.getValue<ContentApp>().CancelSample4Dialog();

            });
        }
        public async Task Creat_card(card_kanoni card_kanoni)
        {
                await Task.Run(() =>
                {

                    var card = Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c => c.id_prosess == card_kanoni.part.Id_Pro && c.year == Ico.getValue<Date>().GetNowDate().Id).OrderByDescending(c => c.num).ToList().FirstOrDefault();
                    var num = 1;
                    if (card != null)
                    {
                        num = card.num + 1;
                    }

                    var d = DateTime.Now;
                    var name = "بطاقة إلتزام محاسبي رقم " + num + " سنة " + d.Year;
                    var loca = Ico.getValue<IO>().CREATE_F_mo7asabi(part.process.location) + "\\" + name;
                    var car = new card()
                    {
                        date = d,
                        id_prosess = card_kanoni.part.Id_Pro,
                        num = Ico.getValue<db>().GetUnivdb().cards.ToList().Where(c => c.id_prosess == card_kanoni.part.Id_Pro).LastOrDefault().num + 1,
                        year = Ico.getValue<Date>().GetNowDate().Id
                     ,
                        location = loca
                    };
                    var card_mo7sabi = new card_mo7sabi()
                    {
                        id_client = card_kanoni.id_client,
                        id_part = card_kanoni.id_part,
                        cost = Cost,
                        oldCost = card_kanoni.part.process.NewCost,
                        card = car,
                        num = numm,
                        visa = null,
                        subject = subject
                    };

                    Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_kanoni.part.Id_Pro).ToList().First().NewCost -= Cost;
                    Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == card_kanoni.id_part).ToList().First().mcost += Cost;
                    Ico.getValue<db>().GetUnivdb().cards.Add(car);
                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.Add(card_mo7sabi);
                    Ico.getValue<db>().savedb();
                    var cardm = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.card.location == loca).FirstOrDefault();
                    Card_mo7asabiExecl c7 = new Card_mo7asabiExecl(cardm);
                    c7.CreateCard();
                    acc();
                    Ico.getValue<ContentApp>().CancelSample4Dialog();


                });
            }

        
    }

}



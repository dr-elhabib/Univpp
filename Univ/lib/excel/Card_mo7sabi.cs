using Univ.modeldb;
using Univ.modeldb.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.lib
{
    class Card_mo7sabi
    {
        public ExcelHlper excel { get; set; }
        public string Location { set; get; }
        public Card_mo7sabi( )
        {
         //   this.bills = bills;
        }

     /*   public void CreateCard() {
            CreateP1();
            CreateP2();
            MessageBox.Show("1");
            excel.SaveAs(Location);

            MessageBox.Show("2");
            excel.Close();

            MessageBox.Show("3");
        }
        public void CreateP1()
        {
            Dictionary<string, string> Data = new Dictionary<string, string>();

            var part = bills.part;
            var taype = bills.taype;
            var p = bills.processes;

            Data["L4"] = p.code_.n + "";
            Data["M4"] = p.code_.k + "";
            Data["Z4"] = p.code_.b + "";
            Data["Y4"] = p.code_.a + "";
            Data["Q4"] = p.code_.e + "";
            Data["P4"] = p.code_.d + "";
            Data["O4"] = p.code_.c + "";
            Data["R4"] = p.code_.j + "";
            Data["N4"] = p.code_.f + "";


            inserttype(taype.numtype, bills.cost,Data);
            Data["E9"] = p.Name + "";
            Data["E15"] = "مؤسسة : "+bills.client.Name;
            Data["E11"] = "إلتزام  محاسبي رقم للمقرر رقم../" + part.process.num + " المؤرخ بتاريخ   " + part.process.date.ToShortDateString();
            Data["E13"] = "الحصة رقم" + part.num + " : " + part.Name;
            int i = Ico.getValue<univdb>().years.ToList().Where(y => y.year1.Year == DateTime.Now.Year).ToList().FirstOrDefault().Id;
            int num = Ico.getValue<univdb>().cards.Where(c => c.year == i && c.id_prosess == p.process.Id).ToList().LastOrDefault() == null ? 10 : Ico.getValue<univdb>().cards.Where(c => c.year == i && c.id_prosess == p.process.Id).ToList().LastOrDefault().num + 1;
            string year = DateTime.Now.Year + "";
            Data["G4"] = year.ElementAt(2) + "";
            Data["F4"] = year.ElementAt(3) + "";
            string a = num + "";
            switch (a.Length)
            {
                case 1:
                    Data["J4"] = "0";
                    Data["I4"] = "0";
                    Data["H4"] = a.ElementAt(0) + "";
                    break;
                case 2:
                    Data["J4"] = "0";
                    Data["I4"] = a.ElementAt(0) + "";
                    Data["H4"] = a.ElementAt(1) + "";
                    break;
                case 3:
                    Data["J4"] = a.ElementAt(0) + "";
                    Data["I4"] = a.ElementAt(1) + "";
                    Data["H4"] = a.ElementAt(2) + "";
                    break;
            }

            Data["B36"] = "" + p.process.NewCost;
            double oldcost = p.process.NewCost;
            p.process.NewCost = p.process.NewCost - bills.cost;
            Data["L36"] = "" + p.process.NewCost;
            Data["E36"] = "" + bills.cost ;
            Data["G32"] = "" + bills.cost;

            var card = new card() { id_prosess = p.process.Id, num = num, year = i, date = DateTime.Now };
          var  _card = new card_mo7sabi() { cost = bills.cost, id_part = part.Id, id_type = taype.Id, oldCost = oldcost,id_client=bills.client.Id };
        card.location = "card_mo7asabi_" + p.Code + "num" + num + ".xlsx";
            Location = card.location;
        card.card_mo7sabi.Add(_card);
            Ico.getValue<univdb>().cards.Add(card);
            Ico.getValue<univdb>().SaveChanges();
            excel = new ExcelHlper("T1",new string[] { "P1" });
            excel.EditMenyCell("P1",Data);


            MessageBox.Show("P1");

        }
        public void CreateP2() {
            Dictionary<string, string> Data = new Dictionary<string, string>();
            Data["A20"] = bills.client.Name;
            Data["A22"] = bills.client.address;
            Data["E20"] = bills.client.num_account;
            Data["E21"] = bills.client.bank;
            Data["E22"] = bills.client.gence;
            Data["F20"] = bills.cost+"";
            Data["G31"] = bills.AlCost;
            Data["L20"] = bills.processes.code_.j + "";
            Data["R21"] = bills.processes.Name;
            Data["R21"] = bills.processes.Name;
            Data["R23"] = bills.part.Name;
            Data["L9"] = bills.processes.code_.e + bills.processes.code_.d + bills.processes.code_.c + "";
            Data["K9"] = DateTime.Now.Year+" ";
            Data["O20"] = bills.processes.code_.a+ bills.processes.code_.b+"";
            Data["M20"] = bills.processes.process.date.Year+"";
            Data["M20"] = bills.processes.process.date.Year+"";
            excel.EditMenyCell("P2", Data);
            MessageBox.Show("P2");

        }
        public void inserttype(int numtype, double cost, Dictionary<string, string> Data) {
            switch (numtype)
            {
                case 1:
                    Data["G20"] = cost+""; break;
                case 2:
                    Data["G21"] = cost + ""; break;
                case 3:
                    Data["G22"] = cost + ""; break;
                case 4:
                    Data["G23"] = cost + ""; break;
                case 5:
                    Data["G24"] =cost + ""; break;
                case 6:
                    Data["G25"] = cost + ""; break;
                case 7:
                    Data["G26"] = cost + "";

                    Data["G27"] = cost + ""; break;
                case 8:
                    Data["G26"] = cost + "";

                    Data["G28"] = cost + ""; break;
                case 9:
                    Data["G26"] = cost + "";

                    Data["G29"] = cost + ""; break;
                case 10:
                    Data["G26"] = cost + ""; 
                    Data["G30"] = cost + ""; break;

            }
    }*/
    }
}

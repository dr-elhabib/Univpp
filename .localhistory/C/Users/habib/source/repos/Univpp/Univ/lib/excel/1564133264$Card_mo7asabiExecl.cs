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
    class Card_mo7asabiExecl
    {
        public ExcelHlper excel { get; set; }
        public card_mo7sabi card_mo7sabi { get; set; }
        public string Location { set; get; }
        Dictionary<string, string> Data = new Dictionary<string, string>();

        public Card_mo7asabiExecl(card_mo7sabi card_mo7sabi)
        {
            this.card_mo7sabi = card_mo7sabi;
        }

        public void CreateCard()
        {
            CreateP1();
        }
        public void CreateP1()
        {

            var p = new processes(card_mo7sabi.card.process);

            inserttype(card_mo7sabi.part.num_type, card_mo7sabi.cost, Data);
            
            Data["E8"] = p.Name;
            Data["E11"] = card_mo7sabi.subject;
            Data["E13"] = "الحصة رقم "+ card_mo7sabi.part.num+" : "+ card_mo7sabi.part.Name;
            Data["E15"] = " مؤسسة : "+ card_mo7sabi.client.Name;
            ///////////////// start top section ////////////
            Data["Z4"] = p.code_.NumProsess[0] + "";
            Data["Y4"] = p.code_.NumProsess[1] + "";
            Data["R4"] = p.code_.Subject + "";
            Data["Q4"] = p.code_.chapter[0] + "";
            Data["P4"] = p.code_.chapter[1] + "";
            Data["O4"] = p.code_.chapter[2] + "";
            Data["N4"] = p.code_.NK[2] + "";
            Data["M4"] = p.code_.NK[0].ToString().ToUpper();
            Data["L4"] = p.code_.NK[1].ToString().ToUpper();

            string year = card_mo7sabi.card.year1.year1.Year.ToString();
            Data["G4"] = year.ElementAt(2) + "";
            Data["F4"] = year.ElementAt(3) + "";
            string a = card_mo7sabi.card.num + "";
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
            /////////////////// finsh top section  //////////////

            Data["G32"] = card_mo7sabi.cost+"";
            Data["E36"] = card_mo7sabi.cost + "";
            Data["B36"] = card_mo7sabi.oldCost + "";
            Data["L36"] = (card_mo7sabi.oldCost- card_mo7sabi.cost) + "";
            



            ExcelHlper excelHlper = new ExcelHlper("mo7asabi_Template", new string[] { "p" });

            excelHlper.EditMenyCell("p", Data);
            excelHlper.SaveAs("mo7asabi_Template" + a+".xlsx");
            excelHlper.Close();
        }
        public static void inserttype(int numtype, Double cost, Dictionary<string, string> Data)
        {
            switch (numtype)
            {
                case 1:
                    Data["G20"] = String.Format("{0:0.00}", cost); break;
                case 2:
                    Data["G21"] = String.Format("{0:0.00}", cost); break;
                case 3:
                    Data["G22"] = String.Format("{0:0.00}", cost); break;
                case 4:
                    Data["G23"] = String.Format("{0:0.00}", cost); break;
                case 5:
                    Data["G24"] = String.Format("{0:0.00}", cost); break;
                case 6:
                    Data["G25"] = String.Format("{0:0.00}", cost); break;
                case 7:
                    Data["G26"] = String.Format("{0:0.00}", cost);

                    Data["G27"] = String.Format("{0:0.00}", cost); break;
                case 8:
                    Data["G26"] = String.Format("{0:0.00}", cost);

                    Data["G28"] = String.Format("{0:0.00}", cost); break;
                case 9:
                    Data["G26"] = String.Format("{0:0.00}", cost);

                    Data["G29"] = String.Format("{0:0.00}", cost); break;
                case 10:
                    Data["G26"] = String.Format("{0:0.00}", cost);
                    Data["G30"] = String.Format("{0:0.00}", cost); break;

            }

        }
        public class types
        {
            public type start { get; set; } = new type()
            {
                cost = 0,
                num = 1
            };

            public types()
            {
                for (int i = 2; i < 11; i++)
                {
                    var type = start;
                    start = new type()
                    {
                        cost = 0,
                        num = i,
                        next = type
                    };
                }
            }

            public void insert(int num, double d)
            {

                start.insert(num, d);
            }


            public double getcost(int num)
            {
                return start.getcost(num);
            }
            public void leadData(Dictionary<string, string> Data)
            {

                    start.leadData(Data);
                
            }

            public class type
            {
                public int num { get; set; }
                public double cost { get; set; }
                public type next { get; set; }
                public type()
                {


                }


                public void insert(int num, double d)
                {

                    if (this.num == num)
                    {
                        this.cost += d;
                        return;
                    }
                    else
                    {
                        if (next != null)
                            next.insert(num, d);
                    }

                }

                public double getcost(int num)
                {

                    if (this.num == num)
                    {
                        return this.cost;
                    }
                    else
                    {
                        if (next != null)
                            return next.getcost(num);
                    }
                    return 0;
                }
                public void leadData(Dictionary<string, string> Data)
                {

                    if (this.cost > 0)
                    {
                        inserttype(this.num, cost, Data);
                    }
                    if (next != null) {
                        next.leadData(Data);

                    }
                }

            }




        }

    }
    /*       inserttype(pr.num_type, pr.cost, Data);

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
*/

}
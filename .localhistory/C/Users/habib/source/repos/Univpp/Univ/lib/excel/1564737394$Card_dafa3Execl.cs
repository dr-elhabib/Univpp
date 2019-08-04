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
    class Card_dafa3Execl
    {
        public ExcelHlper excel { get; set; }
        public card_dafa3 card_dafa3 { get; set; }
        public client client { get; set; }
        public string Location { set; get; }
        Dictionary<string, string> Data = new Dictionary<string, string>();

        public Card_dafa3Execl(card_dafa3 card_dafa3)
        {
            this.card_dafa3 = card_dafa3;
            this.client = card_dafa3.part.card_mo7sabi.ToList().FirstOrDefault().client;
        }

        public void CreateCard()
        {
            excel = new ExcelHlper("dafa3_Template", new string[] { "p","p2" });
            CreateP1();
            CreateP2();
            excel.SaveAs( card_dafa3.location);
            excel.Close();

        }
        public void CreateP1()
        {

            var p = new processes(card_dafa3.part.process);

            inserttype(card_dafa3.part.num_type, card_dafa3.Cost, Data);
            
            Data["E8"] = p.Name;
            Data["E11"] = "إلتزام العقد المتعلق ب "+ card_dafa3.part.Name;
            if (card_dafa3.part.process.parts.ToList().Count == 1)
            {

                Data["E13"] = "الحصة رقم " + card_dafa3.part.num + " : " + card_dafa3.part.Name;
            }
            Data["E15"] = " مؤسسة : "+client.Name;
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

            string year = card_dafa3.part.card_mo7sabi.ToList().FirstOrDefault().card.year1.year1.Year.ToString();
            Data["G4"] = year.ElementAt(2) + "";
            Data["F4"] = year.ElementAt(3) + "";
            string a = card_dafa3.num + "";
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

            Data["G32"] = card_dafa3.Cost+"";
            Data["E36"] = card_dafa3.Cost + "";





            excel.EditMenyCell("p", Data);
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

        public void CreateP2()
        {
            var p = new processes(card_dafa3.part.process);
            Dictionary<string, string> Data1 = new Dictionary<string, string>();
            Data1["K9"] = DateTime.Now.Year+"";
            Data1["A19"] = client.Name;
            Data1["A22"] = client.address;
            Data1["E19"] = client.num_account;
            Data1["E21"] = client.bank;
            Data1["E23"] = client.gence;

            Data1["J20"] = p.code_.NK[0].ToString().ToUpper() + p.code_.NK[1].ToString().ToUpper() + p.code_.NK[2].ToString().ToUpper() ;

            Data1["K20"] =p.code_.chapter[0].ToString()+ p.code_.chapter[1].ToString() + p.code_.chapter[2].ToString();

            Data1["M20"] = p.process.date.Year+"";
            Data1["G31"] = card_dafa3.alcost;
            Data1["R19"] = card_dafa3.tswiya;
            excel.EditMenyCell("p2", Data1);

        }

    }


}
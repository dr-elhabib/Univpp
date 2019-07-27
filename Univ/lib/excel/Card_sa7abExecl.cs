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
    class Card_sa7abExecl
    {
        public ExcelHlper excel { get; set; }
        public card_sa7ab card_sa7ab { get; set; }
        public string Location { set; get; }
        Dictionary<string, string> Data = new Dictionary<string, string>();

        public Card_sa7abExecl(card_sa7ab card_sa7ab)
        {
            this.card_sa7ab = card_sa7ab;
        }

        
        public void CreateCard()
        {

            var p = new processes(card_sa7ab.card.process);

            types types = new types();
            foreach (p_sa7ab pr in card_sa7ab.p_sa7ab)
            {
                types.insert(pr.part.num_type, pr.cost);
             
            }

            types.leadData(Data);
            Data["E8"] = p.Name;
            Data["E11"] = " سحب المبلغ الملتزم به غير المسدد للعملية الى غاية: 31/ 12/ " + p.process.cards.ToList().LastOrDefault().year1.year1.Year;
         //       Data["F11"] = "أخذ بالحساب رقم للمقرر رقم "+p.process.num+" المؤرخ بتاريخ "+p.process.date.ToShortDateString();
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

            string year = card_sa7ab.card.year1.year1.Year.ToString();
            Data["G4"] = year.ElementAt(2) + "";
            Data["F4"] = year.ElementAt(3) + "";
            string a = card_sa7ab.card.num + "";
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

            Data["G32"] = card_sa7ab.cost+"";
            Data["E36"] = card_sa7ab.cost + "";

            Data["B36"] = card_sa7ab.old_cost + "";
            Data["L36"] = (card_sa7ab.old_cost+card_sa7ab.cost) + "";
            



            ExcelHlper excelHlper = new ExcelHlper("sa7ab_Template",new string[] { "p" });

            excelHlper.EditMenyCell("p", Data);
            excelHlper.SaveAs(card_sa7ab.card.location);
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

}
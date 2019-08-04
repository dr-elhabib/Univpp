using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class NewprocessesViewModel: BaseViewModel<process>
    {
        public string Name
        {
            get => Name; set
            {
                if (value.ToString().Length <0 )
                {
                    erour.Add("الرجاء كتابة إسم العملية ");

                }
            }
        }
        public string Code { get=>Code; set {
                if (value.ToString().Length != 15)
                {
                    erour.Add(" كود العملية  يجب أن يحتوي على 15 حرف");

                }
            } }
        public string Num
        {
            get => Name; set
            {
                if (value.ToString().Length < 0)
                {
                    erour.Add("الرجاء كتابة رقم الثابت للعملية ");

                }
            }
        }
        public DateTime date { get; set; } = DateTime.Now;
        private List<string> erour= new List<string>();
        public Command back { get; set; }
        public Command save { get; set; }
        public NewprocessesViewModel() {
            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });

                save = new Command(()=> {

             double totalCoast = 0;
              var p = new process() {
                      Name = Name,
                      date=date,
                      Code = Code,
                      num=Num,
                      NewCost=totalCoast,
                     location= Ico.getValue<IO>().CREATE_F_PRO(Code)
               }; 
                Ico.getValue<db>().GetUnivdb().processes.Add(p);
                Ico.getValue<db>().GetUnivdb().SaveChanges();
                Ico.getValue<ContentApp>().page = new PCrad();
                Ico.getValue<ContentApp>().clear();
            });

        }

        

    }
}

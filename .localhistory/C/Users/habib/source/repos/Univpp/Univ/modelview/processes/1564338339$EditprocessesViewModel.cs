using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class EditprocessesViewModel : BaseViewModel<process>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Num { get; set; }
        public DateTime date { get; set; } 
        public Command save { get; set; }
        public Command back { get; set; }
        private List<string> erour = new List<string>();

        public EditprocessesViewModel(process process)
        {
            this.Name = process.Name;
            this.date = process.date;
            this.Code = process.Code;
            this.Num = process.num;
     
            save = new Command(() =>
            {
            erour = new List<string>();
            if (Name.ToString().Length == 0)
            {
                erour.Add("الرجاء كتابة إسم العملية ");

            }
            if (Code.ToString().Length != 15)
            {
                erour.Add(" كود العملية  يجب أن يحتوي على 15 حرف" + Code.ToString().Length);

            }

            if (Num.ToString().Length == 0)
            {
                erour.Add("الرجاء كتابة رقم الثابت للعملية ");

            }


            if (erour.Count == 0)
            {

                var pr = Ico.getValue<db>().GetUnivdb().processes.Single(p => p.Id == process.Id);
                

                pr.Name = Name;
                pr.date = date;
                pr.Code = Code;
                pr.num = Num;
                Ico.getValue<db>().savedb();
                Ico.getValue<ContentApp>().back();

                }
                else
                {


                    OpenSample4Dialog();
                    Sample4Content = new Messagebox(erour, () => {
                        CancelSample4Dialog();

                    });

                }


            });
            back = new Command(() =>
            {
                Ico.getValue<ContentApp>().back();

            });


        }
    }    
}

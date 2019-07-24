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
    class EditprocessesViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Num { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        Action<object> actiondelete_type;
        Action actionadd_type;
        Action<object> actiondelete_part;
        Action actionadd_part;
        public Command save { get; set; }
        public Command back { get; set; }

        public EditprocessesViewModel(process process)
        {
            this.Name = process.Name;
            this.date = process.date;
            this.Code = process.Code;
            this.Num = process.num;
     
            save = new Command(() =>
            {
                MessageBox.Show(" data of " + process.Id);

                var pr = Ico.getValue<db>().GetUnivdb().processes.Single(p => p.Id == process.Id);

//                Ico.getValue<db>().GetUnivdb().parts.RemoveRange(pr.parts);


                pr.Name = Name;
                pr.date = date;
                pr.Code = Code;
                pr.num = Num;
                Ico.getValue<db>().savedb();
                MessageBox.Show("data svaed");
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges




                Ico.getValue<ContentApp>().page = new PCrad();
                Ico.getValue<ContentApp>().clear();
            });
            back = new Command(() =>
            {
                Ico.getValue<ContentApp>().back();
            });


        }
    }    
}

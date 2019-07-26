using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.lib
{
    class Date
    {
       private year _date { get; set; }
       private year _prev_date { get; set; }

        public Date() {
            var d = DateTime.Now;
            var dn = Ico.getValue<db>().GetUnivdb().years.ToList().LastOrDefault();  //  Where(Y => Y.year1.Year == d.Year).ToList().SingleOrDefault();
            if(dn!=null) {
               this._date = dn;
            }
             else
            {
                this._date = Ico.getValue<db>().GetUnivdb().years.Add(new year() {
                    year1=d
                });
                Ico.getValue<db>().savedb();
            }
            this._prev_date = Ico.getValue<db>().GetUnivdb().years.ToList().Where(Y => Y.year1.Year == (this._date.year1.Year - 1)).ToList().SingleOrDefault();
           
        }
        public year GetNowDate() {
            return _date;
        }
        public year GetPevDate() {

            return _prev_date;
        }
        
    }
}

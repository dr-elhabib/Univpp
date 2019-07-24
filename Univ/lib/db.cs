using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.lib
{
    public class db
    {
        public univdb dbu { get; set; }



        public db(){
            dbu=new univdb();
     
        }

        public void Relead() {
            dbu = new univdb();


        }
        public  void savedb()
        {

            try
            {
                dbu.SaveChanges();
                Relead();

            }

            catch (DbEntityValidationException ex)
            {

                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var dbValidationError in entityValidationErrors.ValidationErrors)
                    {
                        MessageBox.Show("PropertyName: " + dbValidationError.PropertyName + " ErrorMessage: " + dbValidationError.ErrorMessage);
                    }
                }
            }

        }

        public univdb GetUnivdb() {
            return dbu;
        }
        public void changedNumCard(card card) {
            foreach (var c in dbu.cards.ToList().Where(c => c.id_prosess == card.id_prosess && c.num > card.num))
                          c.num -= 1;
            }
    }

}

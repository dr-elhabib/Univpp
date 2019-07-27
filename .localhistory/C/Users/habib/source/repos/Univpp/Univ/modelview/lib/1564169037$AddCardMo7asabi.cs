using Univ.lib;
using Univ.lib.Enum;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.page;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modelview.lib
{
    class AddCardMo7asabi : BaseViewModel
    {
        public List<CommboxItem> list { get; set; }

        public List<ItemPart> item { get; set; }
         public  int idpart { get; set; }
         public  int idtype { get; set; }
         public double Cost { get; set; }
         public double nowCost { get; set; }

         public  Command save { get; set; }

         public AddCardMo7asabi(processes processes, Action _save, Action con)

           {
            nowCost = processes.process.NewCost;
            list = new List<CommboxItem>();
    
            foreach (part part in processes.process.parts.ToList())
            {
                item.Add(new ItemPart(part));
            }
            
        //    save =new  Command(async ()=> {
//                    _save();

          /*      await Task.Run(() => {
                     var card=  new Card_mo7sabi(null);
                  
                    card.CreateCard();                   
               });
  //              con();
  */
          //  });
    }
         

        public partial class ItemPart
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ItemPart(part part)
            {
                Id = part.Id;
                Name = part.Name;
            }
        }

        private string swichName(int numtype) {
            switch (numtype) {
                case 1:return "دراسات ";
                case 2:return " البناء  ";
                case 3:return " الأشغال العمومية";
                case 4:return " الآلات والتجهيزات";
                case 5:return " عتاد النقل";
                case 6:return " التكوين";
                case 8:return " مسح";
                case 9:return "  الإشهار";
                case 7:return "الربط  الكهربـائــي";
            }
            return "";
        }

        internal class CommboxItem
        {

            public int id { get; set; }
            public string name { get; set; }
            public TypeEnum Type { get; set; }
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univ.modeldb.model
{
    class TypePart
    {
         public int  numType { get; set; }
         public string  name { get; set; }
         public TypePart typePart { get; set; }
        public TypePart(int numtype) {

            this.numType = numtype;
            this.name = swichName(numtype);
            typePart = this;
        }

        private string swichName(int numtype)
        {
            switch (numtype)
            {
   
                case 1: return " دراسات ";
                case 2: return " البناء  ";
                case 3: return " الأشغال العمومية";
                case 4: return " الآلات والتجهيزات";
                case 5: return " عتاد النقل";
                case 6: return " التكوين";
                case 7: return " الإشهار";
                case 8: return " مسح";
                case 9: return " الربط  الكهربـائــي";
                case 10: return " طرقات";
            }
            return "";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.modeldb.model
{
    class processes
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public float Cost { get; set; } = 0;
        public double nowcost { get; set; } = 0;
        public Code_ code_ { get; set; }
        public process process { get; set; }
        public processes(process process)
        {
            Name = process.Name;
            nowcost = process.NewCost;
            Code = process.Code;
            
            code_ = new Code_(Code);
            this.process = process;
        }
    }

    public class Code_
    {
        
        public char Subject { get; set; }
        public char[] NK { get; set; }
        public char[] chapter { get; set; }
        public char[] NumProsess { get; set; }
        public Code_(string Code)
        {
            NK =new  char[3];
            chapter = new char[3];
            NumProsess = new char[2];
            NumProsess[1] = Code.ElementAt(14);
            NumProsess[0] = Code.ElementAt(13);
            chapter[2] = Code.ElementAt(5);
            chapter[1] = Code.ElementAt(4);
            chapter[0] = Code.ElementAt(3);
            NK[2] = Code.ElementAt(2);
            Subject = Code.ElementAt(6);
            NK[1] = Code.ElementAt(1);
            NK[0] = Code.ElementAt(0);

        }
    }
}
        


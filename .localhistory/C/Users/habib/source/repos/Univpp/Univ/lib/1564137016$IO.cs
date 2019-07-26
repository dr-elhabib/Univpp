using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.lib
{
    class IO
    {
       private string Path { get; set; }
       private string TemplatesPath { get; set; } 
        public IO(string path =null) {
            if (path == null)
            {
                this.Path = "C:\\ملفات";
                if (!System.IO.File.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
            }
            else {
                this.Path = path;
            }
          

        }
        public string GetPath() {
            return Path;
        }
        public string GetTemplatesPath() {
            return TemplatesPath;
        }
        public void SetPath(string path ) {
            this.Path = path;
        }

        public string CREATE_FOLDER(string file)
        {

            System.IO.Directory.CreateDirectory("C:\\app\\"+file);
            return "C:\\app\\" + file;
        }
        public string CREATE_F_PRO(string file)
        {

            System.IO.Directory.CreateDirectory(Path + "\\"+file);
            System.IO.Directory.CreateDirectory(Path + "\\"+file+"\\محاسبي");
            System.IO.Directory.CreateDirectory(Path + "\\"+file+"\\دفغ");
            System.IO.Directory.CreateDirectory(Path + "\\"+file+"\\سحب إلتزام");
            System.IO.Directory.CreateDirectory(Path + "\\"+file+"\\إلتزام قانوني");
            return Path + "\\" + file;
        }
    }
}

using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Univ.page.lib;

namespace Univ.lib
{
    class IO
    {
        private string Path { get; set; }
        private string TemplatesPath { get; set; } = "C:\\Users\\habib\\source\\repos\\Univpp\\Univ\\template";
        public IO(string path = null)
        {
            if (path == null)
            {
                this.Path = "C:\\ملفات";
                if (!System.IO.Directory.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
            }
            else
            {
                this.Path = path;
            }


        }
        public string GetPath()
        {
            return Path;
        }
        public string GetTemplatesPath()
        {
            return TemplatesPath;
        }
        public void SetPath(string path)
        {
            this.Path = path;
        }
        /*
        public string CREATE_FOLDER(string file)
        {

        }
*/
        public string CREATE_F_PRO(string file)
        {
            var P = Path + "\\" + file;
            if (!System.IO.Directory.Exists(P))
            {
                System.IO.Directory.CreateDirectory(P);
            }
            return P;
        }
        public string CREATE_F_mo7asabi(string file)
        {


            var P = file + "\\محاسبي";
            if (!System.IO.Directory.Exists(P))
            {
                System.IO.Directory.CreateDirectory(P);
            }
            return P;
        }
        public string CREATE_F_kanoni(string file)
        {

            var P = file + "\\إلتزام قانوني";
            if (!System.IO.Directory.Exists(P))
            {
                System.IO.Directory.CreateDirectory(P);
            }
            return P;
        }
        public string CREATE_F_sa7ab(string file)
        {

            var P = file + "\\سحب إلتزام";
            if (!System.IO.Directory.Exists(P))
            {
                System.IO.Directory.CreateDirectory(P);
            }
            return P;
        }
        public string CREATE_F_dafa3(string file)
        {

            var P = file + "\\دفغ";
            if (!System.IO.Directory.Exists(P))
            {
                System.IO.Directory.CreateDirectory(P);
            }
            return P;
        }

        public void DELETE_FILE(string file)
        {
            try
            {
                if (System.IO.File.Exists(file))
                {
                    System.IO.File.Delete(file);
                }
            }
            catch (Exception e) {

                Ico.getValue<ContentApp>().OpenSample4Dialog();
                Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "هنالك خطأ في العملية الرجاء التأكد الملف ليس قيد الاستخدام " }, Ico.getValue<ContentApp>().CancelSample4Dialog);
            }
        }
    }
}

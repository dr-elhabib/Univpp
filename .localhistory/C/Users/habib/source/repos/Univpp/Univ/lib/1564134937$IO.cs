using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ.lib
{
    class IO
    {
       private string Path { get; set; }
       private Dictionary<string, string> TemplatesPath { get; set; } 
        public IO(string path =null) {
            if (path == null)
            {
                this.Path = "C:\\app";
                if (!System.IO.File.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
            }
            else {
                this.Path = path;
            }
            TemplatesPath = new Dictionary<string, string>();
            TemplatesPath.Add("7isab", "../template/dafa3_Template.xlsx");
 var           App = new Microsoft.Office.Interop.Excel.Application();
            App.Visible = false;
            try
            {
                var xlBook = App.Workbooks.Open(@"template/dafa3_Template.xlsx");
            }
            catch(Exception r) { MessageBox.Show(r.GetType()+""); }
            /* if (!System.IO.File.Exists(TemplatesPath["7isab"]))
            {
                MessageBox.Show("no");
            }
            else {
                MessageBox.Show("yes");

            }*/

        }
        public string GetPath() {
            return Path;
        }
        public void SetPath(string path ) {
            this.Path = path;
        }

        public string CREATE_FOLDER(string file)
        {

            System.IO.Directory.CreateDirectory("C:\\app\\"+file);
            return "C:\\app\\" + file;
        }
    }
}

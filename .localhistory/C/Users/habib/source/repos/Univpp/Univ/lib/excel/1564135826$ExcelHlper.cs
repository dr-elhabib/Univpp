﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel=Microsoft.Office.Interop.Excel;


namespace Univ.lib
{
    class ExcelHlper
    {
        private string Path { get; set; }
        private string FileName { get; set; }
        private Excel.Application App { get; set; }
        private Dictionary<string,Excel.Worksheet> Worksheets { get; set; }
        public ExcelHlper(string FileName, string[] SheetNames)
        {
            this.FileName = FileName;
            this.Path = Ico.getValue<IO>().GetTemplatesPath();
            try
            {
                App = new Excel.Application();
                App.Visible = false;
                Worksheets = new Dictionary<string, Excel.Worksheet>();
                Console.Write(Path + FileName + ".xls");
                var xlBook = App.Workbooks.Open(@"" + Path + FileName + ".xlsx");
                foreach (string SheetName in SheetNames)
                   { 
                Worksheets[SheetName]=(Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.get_Item(SheetName); // Explicit cast is not required here

                }
            }
            catch (Exception e) {
                Console.Write(Path + FileName + ".xls");
                App.Quit();
            }
        }
        public void EditOneCell(string index,string value,string SheetName) {
            try
            {
                var Worksheet = Worksheets[SheetName];
                Excel.Range excelCell = (Excel.Range)Worksheet.get_Range(index);
                excelCell.Value2 = value;
            }
            catch (Exception e) {
                Console.Write(Path + FileName + ".xls"+"  "+e);
                App.Quit();
            }
        }
        public void EditMenyCell( string SheetName,Dictionary<string, string> maps) {
            var Worksheet = Worksheets[SheetName];

            foreach (var pair in maps)
            {
                Excel.Range excelCell = Worksheet.get_Range(pair.Key);
                excelCell.Value2 = pair.Value;
            }
        }
        public void SaveAs(string pathSAVE) {
            App.ActiveWorkbook.SaveAs(Path+pathSAVE);
        }
        public void Close() {
            App.ActiveWorkbook.Close();
            App.Quit();
        }
    }
}

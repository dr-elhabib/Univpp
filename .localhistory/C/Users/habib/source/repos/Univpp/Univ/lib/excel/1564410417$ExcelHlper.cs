using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel=Microsoft.Office.Interop.Excel;


namespace Univ.lib
{
    class ExcelHlper
    {
        private string FileName { get; set; }
        private Excel.Application App { get; set; }
        private Dictionary<string,Excel.Worksheet> Worksheets { get; set; }

        public ExcelHlper(string FileName, string[] SheetNames)
        {
            this.FileName = FileName;
         
            try
            {
                App = new Excel.Application();
                App.Visible = false;
                Worksheets = new Dictionary<string, Excel.Worksheet>();
                var xlBook = App.Workbooks.Open(@"" + Ico.getValue<IO>().GetTemplatesPath() +"\\"+ FileName + ".xlsx");
                foreach (string SheetName in SheetNames)
                   { 
                Worksheets[SheetName]=(Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.get_Item(SheetName); // Explicit cast is not required here

                }
            }
            catch (Exception e) {
                App.Quit();
            }
        }
        public void EditOneCell(string index,string value,string SheetName) {
            try
            {
                var Worksheet = Worksheets[SheetName];
                Worksheet.PageSetup.FitToPagesTall = true;
                Worksheet.PageSetup.FitToPagesWide = true;
                Excel.Range excelCell = (Excel.Range)Worksheet.get_Range(index);
                excelCell.Value2 = value;
            }
            catch (Exception e) {
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
            var p=pathSAVE+ ".xlsx";
            if (System.IO.File.Exists(p))
            {
                System.IO.File.Delete(p);
            }

            App.ActiveWorkbook.SaveAs(p);

        }
        public void Close() {
            App.ActiveWorkbook.Close();
            App.Quit();
        }
        public static void PrintFile(String s) {
            try
            {
                Excel.Application App = new Excel.Application();
                App.Visible = true;
                var xlBook = App.Workbooks.Open(s + ".xlsx");
               var Sheet = (Microsoft.Office.Interop.Excel.Worksheet)xlBook.Worksheets.get_Item("p"); // Explicit cast is not required here
             
                var _with1 = Sheet.PageSetup;
                // A4 papersize
                _with1.PaperSize = Excel.XlPaperSize.xlPaperA4;
                // Landscape orientation
                _with1.Orientation = Excel.XlPageOrientation.xlPortrait;
                // Fit Sheet on One Page 
                _with1.FitToPagesWide = 1;
                _with1.FitToPagesTall = 1;
                _with1.Zoom = false;
                //  _with1.FitToPagesTall = 1;
                // Normal Margins
                /*                _with1.LeftMargin = App.InchesToPoints(0.7);
                                _with1.RightMargin = App.InchesToPoints(0.7);
                                _with1.TopMargin = App.InchesToPoints(0.75);
                                _with1.BottomMargin = App.InchesToPoints(0.75);
                                _with1.HeaderMargin = App.InchesToPoints(0.3);
                                _with1.FooterMargin = App.InchesToPoints(0.3);
                  */
                object misValue = System.Reflection.Missing.Value;


                // Print the range
                Sheet.PrintOutEx(misValue, misValue, misValue, misValue,
                misValue, misValue, misValue, misValue);

            }
            catch (Exception e)
            {
            }


        }
    }
}

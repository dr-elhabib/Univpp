using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.lib;
using Univ.modeldb;
using System.Windows.Forms.Integration;
namespace Univ.modelview
{
  public  class SettingModelView:BaseViewModel<setting>
    {
        public string TempalePath { get; set; }
        public string PathFileSave { get; set; }
        public Command PathFileSavec { get; set; }
        public Command TempalePathc { get; set; }
        public DateTime date { get; set; }

     public   SettingModelView() {

            val = Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault();
            TempalePath = val.locationTem;
            PathFileSave =val.locationFile ;
            date = DateTime.Now;
            PathFileSavec =new Command (() => {
                var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                var folderResult = folderDialog.ShowDialog();
                if (folderResult.HasValue && folderResult.Value)
                {
                    PathFileSave = folderDialog.SelectedPath;
                }
            }); TempalePathc = new Command (() => {
                var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
                var folderResult = folderDialog.ShowDialog();
                if (folderResult.HasValue && folderResult.Value)
                {
                    PathFileSave = folderDialog.SelectedPath;
                }
            });
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.lib;
using Univ.modeldb;
using System.Windows.Forms.Integration;
using Univ.page.lib;

namespace Univ.modelview
{
  public  class SettingModelView:BaseViewModel<setting>
    {
        public string TempalePath { get; set; }
        public string PathFileSave { get; set; }
        public Command PathFileSavec { get; set; }
        public Command savecommand { get; set; }
        public Command TempalePathc { get; set; }
        public DateTime date { get; set; }
        private List<string> erour = new List<string>();


        public SettingModelView() {

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
                    TempalePath = folderDialog.SelectedPath;
                }
            });

            savecommand = new Command(() =>
            {

                erour = new List<string>();


               



                if (PathFileSave.ToString().Length == 0)
                {
                    erour.Add("الرجاء كتابة مسار حفظ الملفات        ");

                }

                if (TempalePath.ToString().Length == 0)
                {
                    erour.Add("الرجاء كتابة  مسار قوالب      ");

                }

                if (date == null)
                {
                    erour.Add("الرجاء تحديد تاريخ البرنامج  ");

                }
                else {

                    if (Ico.getValue<Date>().GetNowDate().year1.Year >date.Year)
                    {
                        erour.Add("الرجاء تحديد تاريخ صحيح  ");

                    }
                }

                Ico.getValue<ContentApp>().OpenSample4Dialog();
                if (erour.Count != 0)
                {

                    Ico.getValue<ContentApp>().Sample4Content = new Messagebox(erour, () =>
                    {

                        Ico.getValue<ContentApp>().CancelSample4Dialog();
                    });

                }
                else {

                    if (Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().date.Year != date.Year)
                    {
                        Ico.getValue<ContentApp>().Sample4Content = new YesOrNo("هل تريد تغير تاريخ البرنامج ذالك سيغر الكثر في البرناج الرجاء الحذر ...", () => {


                                Ico.getValue<db>().GetUnivdb().years.Add(new year() {
                                    year1 = date
                                });

                            Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().locationFile = PathFileSave;
                    Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().locationTem = TempalePath;
                    Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().date = date;

                    Ico.getValue<db>().savedb();
                    Ico.ResetValue<Date>(new Date());
                    Ico.ResetValue<setting>(Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault());
                    Ico.ResetValue<IO>(new IO());

                    Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> {"تم التعديل بنجاح.. " }, Ico.getValue<ContentApp>().CancelSample4Dialog);

                        }, () => {
                            Ico.getValue<ContentApp>().CancelSample4Dialog();
                        });
                    }
                    else
                    {

                        Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().locationFile = PathFileSave;
                        Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().locationTem = TempalePath;
                        Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault().date = date;

                        Ico.getValue<db>().savedb();
                        Ico.ResetValue<Date>(new Date());
                        Ico.ResetValue<setting>(Ico.getValue<db>().GetUnivdb().settings.ToList().FirstOrDefault());
                        Ico.ResetValue<IO>(new IO());

                        Ico.getValue<ContentApp>().Sample4Content = new Messagebox(new List<string> { "تم التعديل بنجاح.. " }, Ico.getValue<ContentApp>().CancelSample4Dialog);


                    }

                }
            });

            }
        }
}

using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.modelview.lib;
using Univ.page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Univ.page.lib;

namespace Univ.modelview
{
    class NewprocessesViewModel: BaseViewModel<process>
    {
        public string Name
        {
            get => Name; set
            {
                if (value.ToString().Length <0 )
                {
                    erour.Add("الرجاء كتابة إسم العملية ");

                }
            }
        }
        public string Code { get=>Code; set {
                if (value.ToString().Length != 15)
                {
                    erour.Add(" كود العملية  يجب أن يحتوي على 15 حرف");

                }
            } }
        public string Num
        {
            get => Name; set
            {
                if (value.ToString().Length < 0)
                {
                    erour.Add("الرجاء كتابة رقم الثابت للعملية ");

                }
            }
        }
        public DateTime date { get; set; } = DateTime.Now;
        private List<string> erour= new List<string>();
        public Command back { get; set; }
        public Command save { get; set; }
        public NewprocessesViewModel() {
            back = new Command(() => {
                Ico.getValue<ContentApp>().back();
            });

                save = new Command(()=> {
                    if (erour.Count != 0)
                    {
                        double totalCoast = 0;
                        var p = new process()
                        {
                            Name = Name,
                            date = date,
                            Code = Code,
                            num = Num,
                            NewCost = totalCoast,
                            location = Ico.getValue<IO>().CREATE_F_PRO(Code)
                        };
                        Ico.getValue<db>().GetUnivdb().processes.Add(p);
                        Ico.getValue<db>().GetUnivdb().SaveChanges();
                        Ico.getValue<ContentApp>().page = new PCrad();
                        Ico.getValue<ContentApp>().clear();

                    }
                    else
                    {


                        OpenSample4Dialog();
                        Sample4Content = new Messagebox(erour, () => {
                            CancelSample4Dialog();
                     
                        });

                    }
                });

        }

        public bool IsSample4DialogOpen
        {
            get { return _isSample4DialogOpen; }
            set
            {
                if (_isSample4DialogOpen == value) return;
                _isSample4DialogOpen = value;
            }
        }

        public UserControl Sample4Content
        {
            get { return _sample4Content; }
            set
            {
                if (_sample4Content == value) return;
                _sample4Content = value;
            }
        }
        private bool _isSample4DialogOpen;
        private UserControl _sample4Content;


        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }

    }
}

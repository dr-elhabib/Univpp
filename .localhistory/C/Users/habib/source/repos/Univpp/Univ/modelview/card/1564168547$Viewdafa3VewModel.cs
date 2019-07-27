
using Univ.lib;
using Univ.modeldb;
using Univ.modeldb.model;
using Univ.page;
using Univ.page.lib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Univ.modelview
{
    class Viewdafa3VewModel : BaseViewModel<part>
    {





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




        public process process { get; set; }
        public card card { get; set; }
        public Part part { get; set; }
               
        
        public string client { get; set; }
        public double newcost { get; set; }=0;
        public double old_cost { get; set; }=0;
        public String  visa { get; set; }

        public ObservableCollection<ItemDafa3> ItemDafa3S { get; set; }

        public Command back { get; set; }
        public Command AddDafa3 { get; set; }
        public Viewdafa3VewModel(part  part)
        {
            this.inTilData(part);
            this.client=part.card_kanoni.ToList().FirstOrDefault().client.Name;
            visa = "لم يتحصل على فيزا إلى حد الان ..";
            back =new  Command(()=> {
            Ico.getValue<ContentApp>().back();
              });

            AddDafa3 = new Command(()=> {
                Sample4Content = new Adddafa3(part, AcceptSample4Dialog, CancelSample4Dialog,()=> {
                    this.inTilData(Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p=>p.Id==part.Id).ToList().SingleOrDefault());
                });
                OpenSample4Dialog();

            });

            

        }

        public void inTilData(part part) {

            this.process = part.process;
            this.part = new Part(part);
            ItemDafa3S = CreateItem();

            this.newcost = (part.nowcost);
            this.old_cost = (part.mcost - part.nowcost);

        }
        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
            this.inTilData(Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.part.Id).ToList().SingleOrDefault());

        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }

        public ObservableCollection<ItemDafa3> CreateItem() {


            return new ObservableCollection<ItemDafa3>(part.part.card_dafa3.Select(c => new ItemDafa3(c)
            {
                action = (t) => {
                    newcost -= t;
                    this.inTilData(Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.part.Id).ToList().SingleOrDefault());
                },
                action_edit = (t) => {
                    Sample4Content = new Editdafa3(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();

                    this.inTilData(Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.part.Id).ToList().SingleOrDefault());
                },
                addtswiya = (t) => {
                    Sample4Content = new Addtswiya(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                    this.inTilData(Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.part.Id).ToList().SingleOrDefault());
                },
                edittswiyaaction = (t) => {
                    Sample4Content = new Edittswiya(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }


            }));
        }

    }


}

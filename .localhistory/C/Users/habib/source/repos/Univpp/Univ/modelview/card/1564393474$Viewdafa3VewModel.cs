
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
  public  class Viewdafa3VewModel : BaseViewModel<part>
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

            this.actionUP = () => {
                this.val = Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == part.Id).ToList().FirstOrDefault();
            };

            this.inTilData();
            this.client=part.card_kanoni.ToList().FirstOrDefault().client.Name;
            visa = "لم يتحصل على فيزا إلى حد الان ..";
            back =new  Command(()=> {
            Ico.getValue<ContentApp>().back();
              });

            AddDafa3 = new Command(()=> {
                Sample4Content = new Adddafa3(part, this,AcceptSample4Dialog, CancelSample4Dialog, this.inTilData);
                OpenSample4Dialog();

            });

            

        }

        public void inTilData() {
            this.actionUP();
            this.process = val.process;
            this.part = new Part(val);
            ItemDafa3S = CreateItem();

            this.newcost = (val.nowcost);
            this.old_cost = (val.mcost - val.nowcost);

        }
        public void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        public void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
            this.inTilData();

        }

        public void AcceptSample4Dialog()
        {
            OpenSample4Dialog();
            Sample4Content = new Progressbar();
        }
        public void Sample4Contentviw( UserControl us)
        {
            OpenSample4Dialog();
            Sample4Content = us;
        }

        public ObservableCollection<ItemDafa3> CreateItem() {


            return new ObservableCollection<ItemDafa3>(part.part.card_dafa3.Select(ct => new ItemDafa3(ct)
            {
                action = (t) => {

                    OpenSample4Dialog();

                    Sample4Content = new YesOrNo("هل أنت متأكد من قيامك بحذف هذه الحصة من العملية ,لا يمكن التراجع عن الحذف",
                       async () => {
                           AcceptSample4Dialog();
                           await Task.Run(() => {

                               Ico.getValue<db>().GetUnivdb().parts.ToList().Where(c => c.Id == ct.id_part).ToList().SingleOrDefault().nowcost -= t;
                               Ico.getValue<db>().GetUnivdb().card_dafa3.Remove(Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => c.Id == ct.Id).FirstOrDefault());
                               Ico.getValue<db>().savedb();

                               CancelSample4Dialog();

                           });


                       },
                        () => {

                            CancelSample4Dialog();
                        });
                },
                action_edit = (t) => {
                    Sample4Content = new Editdafa3(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();

                    this.inTilData();
                },
                addtswiya = (t) => {
                    Sample4Content = new Addtswiya(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                    this.inTilData();
                },
                edittswiyaaction = (t) => {
                    Sample4Content = new Edittswiya(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }


            }));
        }

    }


}

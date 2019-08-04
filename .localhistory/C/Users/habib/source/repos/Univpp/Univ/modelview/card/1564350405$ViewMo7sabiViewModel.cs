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
    class ViewMo7sabiViewModel : BaseViewModel<card_kanoni>
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
        public card_kanoni card_kanoni { get; set; }
        public Part part { get; set; }
               
        public double oldcost { get; set; } =0;

        public string client { get; set; }
        public double newcost { get; set; } =0;
        public String  visa { get; set; }

        public Command addmo7asabi { get; set; }
        public Command back { get; set; }

        public ObservableCollection<ItemMo7asabi> ItemMo7asabis { get; set; }

        public ViewMo7sabiViewModel(card_kanoni card_kanoni)
        {
            this.card_kanoni = card_kanoni;

            this.actionUP = () => {
                this.val = Ico.getValue<db>().GetUnivdb().card_kanoni.ToList().Where(p => p.id == card_kanoni.id).ToList().FirstOrDefault();
            };
            inTilData();

            back = new  Command(()=> {
                Ico.getValue<ContentApp>().back();
            });
             
        }



        private void OpenSample4Dialog()
        {
            IsSample4DialogOpen = true;
        }

        private void CancelSample4Dialog()
        {
            IsSample4DialogOpen = false;
            this.inTilData();
        }

        private void AcceptSample4Dialog()
        {
            Sample4Content = new Progressbar();
        }


        public void inTilData()
        {
            this.UpDate();
            this.process = val.card.process;
            this.part = new Part(val.part);
            this.client = val.client.Name;
            this.newcost = val.part.process.NewCost;
            ItemMo7asabis = new ObservableCollection<ItemMo7asabi>(val.part.card_mo7sabi.Select(card_mo7sabi => new ItemMo7asabi(card_mo7sabi)
            {
                action_edit = (t) => {
                    Sample4Content = new Editmo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                },
                start=()=>{

                    OpenSample4Dialog();

                    Sample4Content = new YesOrNo("هل أنت متأكد من قيامك بحذف هذه البطاقة من الحصة ,لا يمكن التراجع عن الحذف",
                       async () => {
                           AcceptSample4Dialog();
                           await Task.Run(() => {


                Ico.getValue<db>().GetUnivdb().processes.ToList().Where(p => p.Id == card_mo7sabi.part.Id_Pro).ToList().SingleOrDefault().NewCost += card_mo7sabi.cost;
                Ico.getValue<db>().changedNumCard(card_mo7sabi.card);
                Ico.getValue<db>().GetUnivdb().card_dafa3.RemoveRange(get_data(card_mo7sabi));
                IEnumerable<card_dafa3> get_data(card_mo7sabi card)
                {

                    var cs = Ico.getValue<db>().GetUnivdb().card_dafa3.ToList().Where(c => (c.id_part == card_mo7sabi.id_part) && (c.tswiya == null) && (c.date > card_mo7sabi.card.date));

                    foreach (var c in cs)
                    {
                        Ico.getValue<db>().GetUnivdb().parts.ToList().Where(p => p.Id == card_mo7sabi.id_part).First().nowcost -= c.Cost;
                    }
                    return cs;
                }
            foreach (var c in Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.part.Id_Pro == Ico.getValue<db>().GetUnivdb().cards.
            ToList().Where(cl => cl.Id == card_mo7sabi.id_card).ToList().SingleOrDefault().id_prosess && c.card.date > Ico.getValue<db>().GetUnivdb().cards.
            ToList().Where(cl => cl.Id == card_mo7sabi.id_card).ToList().SingleOrDefault().date)) {
                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(ca => ca.Id==c.Id).ToList().FirstOrDefault().oldCost += card_mo7sabi.cost;
                    Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(ca => ca.Id == c.Id).ToList().FirstOrDefault().num -= 1;
                }
          
                Ico.getValue<db>().GetUnivdb().card_mo7sabi.Remove(Ico.getValue<db>().GetUnivdb().card_mo7sabi.
                ToList().Where(c => c.Id == card_mo7sabi.Id).ToList().SingleOrDefault());
                Ico.getValue<db>().GetUnivdb().cards.Remove(Ico.getValue<db>().GetUnivdb().cards.
                ToList().Where(c => c.Id == card_mo7sabi.id_card).ToList().SingleOrDefault());
                Ico.getValue<db>().savedb();
             
                               CancelSample4Dialog();

                           });

                           
                       },
                        () => {

                            CancelSample4Dialog();
                        });
                },
                end= CancelSample4Dialog,
                addtashira = (t) => {
                    Sample4Content = new Addtashira_mo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                    this.inTilData();
                },
                edittashiraaction = (t) => {
                    Sample4Content = new Edittashira_mo7asabi(t, AcceptSample4Dialog, CancelSample4Dialog);
                    OpenSample4Dialog();
                }



            }));

            addmo7asabi = new Command(() => {
                
                var card = Ico.getValue<db>().GetUnivdb().card_mo7sabi.ToList().Where(c => c.id_part == card_kanoni.id_part).ToList().FirstOrDefault();
                var dn = 0.0;
                foreach (var p in Ico.getValue<db>().GetUnivdb().parts.ToList().Where(pr => pr.Id_Pro == pr.Id_Pro).ToList())
                {
                    dn += p.mcost - p.nowcost;
                }

                if (card != null && card.card.year != Ico.getValue<Date>().GetNowDate()?.Id && Ico.getValue<db>().GetUnivdb().card_sa7ab.ToList().Where(c =>
                   c.card.id_prosess == card_kanoni.card.id_prosess&&c.card.year== Ico.getValue<Date>().GetNowDate().Id).ToList().ToList().Count == 0&& dn != 0)
                {
                    
                    MessageBox.Show(" الرجاء التأكد من إستخراج بظاقة سحب إلتزام مسبقاا  ");
                }
                else {

                    Sample4Content = new Addmo7asabi(card_kanoni, this, AcceptSample4Dialog, CancelSample4Dialog) {
                        Sample4Content= Sample4Content
                    };
                    OpenSample4Dialog();
                }
            });

        }
        public void Sample4Contentviw(UserControl us)
        {
            OpenSample4Dialog();
            Sample4Content = us;
        }
    }
    }

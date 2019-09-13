﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univ.lib;
using Univ.modeldb;
using Univ.page;

namespace Univ.modelview
{
   public class ViewAccountViewModel:BaseViewModel<user>
    {
        public string name { get; set; }
        public string username { get; set; }
        public string date_gust { get; set; }
        public string date_edit { get; set; }
        public Command edit { get; set; }
        public ViewAccountViewModel() {

            this.actionUP = () => {
                val = Ico.getValue<db>().GetUnivdb().users.ToList().Where(c => c.id == Ico.getValue<user>().id).ToList().FirstOrDefault();
            };
            inTilData();

            name = val.name;
            username = val.username;
            date_gust = val.date_gust.GetDateTimeFormats()[0];
            date_edit = val.date_edit.GetDateTimeFormats()[0];
            edit=new Command(()=>{
                Ico.getValue<ContentApp>().page = new EditAccount(val);

            });
        }
       public void inTilData() {
            actionUP();

        }
    }
}
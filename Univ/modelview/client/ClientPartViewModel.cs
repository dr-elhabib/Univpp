﻿using Univ.lib;
using Univ.modeldb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univ.modelview
{
    class ClientPartViewModel:BaseViewModel
    {

        public string Name { get; set; }
        public string numaccount { get; set; }
        public string agence { get; set; }
        public string bank { get; set; }
        public string address { get; set; }

        public CommandPar delete { get; set; }
        public Command edit { get; set; }
        public Action<object> deleteAc { get; set; }
        public Action editAc { get; set; }

        public ClientPartViewModel( client client) {

            this.Name = client.Name;
            this.numaccount = client.num_account;
            this.agence = client.gence;
            this.bank = client.bank;
            this.address = client.address;
            delete = new CommandPar((item) => {
                deleteAc(item);
            });
            edit = new Command(() => {
                editAc();
            });

        }
    }
}
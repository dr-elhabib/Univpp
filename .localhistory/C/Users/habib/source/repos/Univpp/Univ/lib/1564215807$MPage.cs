﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Univ.lib
{
    class MPage<BaseViewModel>  : Page
    {
       public Action Reload { get; set; }
        
       }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Univ
{
    class IsHoverProperty : BaseAttachedProperty<IsHoverProperty, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("no");

        }
    }
}

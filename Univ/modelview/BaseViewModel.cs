using System;
using System.ComponentModel;
using System.Windows;

namespace Univ

{
    public class BaseViewModel<T> : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public T val { get; set; }
        public Action actionUP { get; set; }
        public void UpDate() {
            actionUP();
        }
    }
}

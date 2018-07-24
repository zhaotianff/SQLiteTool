using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteTool.Util
{

    /// <summary>
    /// NotifyPropertyChange Base Class
    /// </summary>
    public class NotifyPropertyBase : INotifyPropertyChanged
    {       
        public void RaiseChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;        
    }
}

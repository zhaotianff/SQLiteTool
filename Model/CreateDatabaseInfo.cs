using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteTool.Model
{
    class CreateDatabaseInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private string filePath;
        private bool isNameFocused;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                RaiseChange("Name");
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }

            set
            {
                filePath = value;
                RaiseChange("FilePath");
            }
        }


        public bool IsNameFocused
        {
            get 
            {
                return isNameFocused; 
            }
            set 
            { 
                isNameFocused = value;
                RaiseChange("IsNameFocused");
            }
        }

        public void RaiseChange(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this,new PropertyChangedEventArgs(property));
            }
        }
    }
}

using SQLiteTool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteTool.Model;

namespace SQLiteTool.ViewModels
{
    class CreateDatabaseViewModel : NotifyPropertyBase
    {
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLiteTool.Model;
using SQLiteTool.Util;

namespace SQLiteTool
{
    class MainWindowViewModel : NotifyPropertyBase
    {
        private List<DatabaseItem> databaseItemList;
        public List<DatabaseItem> DatabaseItemList
        {
            get { return databaseItemList; }
            set
            {
                databaseItemList = value;
                RaiseChange("DatabaseItemList");
            }
        }
    }
}

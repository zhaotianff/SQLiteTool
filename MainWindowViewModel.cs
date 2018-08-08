using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLiteTool.Model;
using SQLiteTool.Util;
using System.Windows.Input;
using SQLiteTool.Commands;
using SQLiteTool.Views;

namespace SQLiteTool
{
    class MainWindowViewModel : NotifyPropertyBase
    {

        public ICommand ShowCreateDatabaseDialog { get; private set; }
   
        public bool IsCreateDialogShow { get; set; }

        
        public MainWindowViewModel()
        {
            IsCreateDialogShow = false;

            ShowCreateDatabaseDialog = new DelegateCommand(ShowCreateDialog,()=> {return  !IsCreateDialogShow; });
        }

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




        public void ShowCreateDialog()
        {
            CreateDatabase dialog = new Views.CreateDatabase();
            dialog.ShowDialog();
        }
    }
}

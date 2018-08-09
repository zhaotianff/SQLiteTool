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
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace SQLiteTool
{
    class MainWindowViewModel : NotifyPropertyBase
    {
        GlobalData globalData;
        public ICommand ShowCreateDatabaseDialog { get; private set; }

        public ICommand SelectDBItemCommand { get; private set; }

        public ICommand OpenDatabaseCommand { get; private set; }

        public ICommand ExecuteQueryCommand { get; private set; }
   
        public bool IsCreateDialogShow { get; set; }

        public MainWindowViewModel()
        {
            IsCreateDialogShow = false;

            globalData = GlobalData.CreateInstance();

            ShowCreateDatabaseDialog = new DelegateCommand(ShowCreateDialog, () => { return !IsCreateDialogShow; });
            SelectDBItemCommand = new DelegateCommand<RoutedEventArgs>(SelectDBFunc);
            OpenDatabaseCommand = new DelegateCommand(OpenDatabase, () => {return (SelectedDBItem != null && SelectedDBItem.OpenState == false && SelectedDBItem.Children == null); });
            ExecuteQueryCommand = new DelegateCommand(ExecuteQuery,()=> { return !string.IsNullOrEmpty(SQLStr); });

            DatabaseItemList = LoadDatabaseList();
            StatusInfo = Properties.Resources.Txt_Status_Ready;
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

        private string statusInfo;
        public string StatusInfo
        {
            get
            {
                return statusInfo;
            }

            set
            {
                statusInfo = value;
                RaiseChange("StatusInfo");
            }
        }     

        public void ShowCreateDialog()
        {
            CreateDatabase dialog = new Views.CreateDatabase();
            dialog.ShowDialog();
        }

        private DatabaseItem selectedDBItem;

        public DatabaseItem SelectedDBItem
        {
            get
            {
                return selectedDBItem;
            }
            set
            {
                selectedDBItem = value;
                RaiseChange("SelectedDBItem");
            }
        }

        private DataView queryResultTable;

        public DataView QueryResultTable
        {
            get
            {
                return queryResultTable;
            }

            set
            {
                queryResultTable = value;
                RaiseChange("QueryResultTable");
            }
        }

        public string SQLStr
        {
            get
            {
                return sQLStr;
            }

            set
            {
                sQLStr = value;
                RaiseChange("SQLStr");
            }
        }

        private string sQLStr;


        public List<DatabaseItem> LoadDatabaseList()
        {
            List<DatabaseItem> list = new List<DatabaseItem>();
            List<DatabaseItem> childList = new List<DatabaseItem>();

            try
            {
                globalData.xmlHelper.OpenXml(globalData.ConfigFilePath);

                IEnumerable<System.Xml.Linq.XElement> eles = globalData.xmlHelper.GetElementByNodeName("DatabaseList/LocalDBList/Item");

                if(eles != null)
                {
                    foreach (var item in eles)
                    {
                        DatabaseItem dbItem = new DatabaseItem();
                        dbItem.Children = null;
                        dbItem.Description = item.Element("Description").Value;
                        dbItem.DisplayName = item.Element("DisplayName").Value;
                        dbItem.FilePath = item.Element("FilePath").Value;
                        childList.Add(dbItem);
                    }
                }

                list.Add(new DatabaseItem()
                {
                    Children = childList,
                    DisplayName = Properties.Resources.Txt_LocalDB,                    
                });

            }
            catch (Exception ex)
            {
                globalData.log.Error(ex.Message);
            }
            return list;
        }

        public void RefreshDatabaseList()
        {
            DatabaseItemList = LoadDatabaseList();
        }

        private void SelectDBFunc(RoutedEventArgs e)
        {
            var treeViewItem = VisualTreeUtil.VisualTreeUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem == null) return;


            DatabaseItem item = treeViewItem.Header as DatabaseItem;
            if(item != null)
            {            
                SelectedDBItem = item;               
            }               
        }

        private void OpenDatabase()
        {            
            var openedItem = DatabaseItemList.Where(x => x.OpenState == true).FirstOrDefault();

            if (openedItem != null && openedItem.FilePath != SelectedDBItem.FilePath)
                globalData.dbHelper.CloseLocalDB(openedItem.FilePath);

            var result = globalData.dbHelper.OpenLocalDB(SelectedDBItem.FilePath);

            if(result == true)
            {
                StatusInfo =Properties.Resources.Txt_OpenDB + "[" + SelectedDBItem.DisplayName + "]" + Properties.Resources.Txt_Status_Success + "(" + SelectedDBItem.FilePath + ")";
            }
            else
            {
                StatusInfo = SelectedDBItem.DisplayName + Properties.Resources.Txt_OpenDB;
            }
        }

        public void ExecuteQuery()
        {
            QueryResultTable = globalData.dbHelper.ExecuteQuery(SQLStr).DefaultView;
        }
    }
}

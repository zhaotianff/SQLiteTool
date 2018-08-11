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
using System.Windows.Documents;

namespace SQLiteTool
{
    class MainWindowViewModel : NotifyPropertyBase
    {
        GlobalData globalData;

        private List<DatabaseItem> databaseItemList;
        private string statusInfo;
        private string queryResultCount;
        private DatabaseItem selectedDBItem;
        private string sQLStr;
        private DataView queryResultTable;

        public ICommand ShowCreateDatabaseDialog { get; private set; }
        public ICommand ShowAttatchDatabaseDialog { get; private set; }
        public ICommand SelectDBItemCommand { get; private set; }
        public ICommand OpenDatabaseCommand { get; private set; }
        public ICommand ExecuteQueryCommand { get; private set; }
        public ICommand FetchSQLStrCommand { get; private set; }  

        public ICommand DeleteDBCommand { get; private set; }
        public bool IsCreateDialogShow { get; set; }

        public List<DatabaseItem> DatabaseItemList
        {
            get { return databaseItemList; }
            set
            {
                databaseItemList = value;
                RaiseChange("DatabaseItemList");
            }
        }
               
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

        public string QueryResultCount
        {
            get
            {
                return Properties.Resources.Txt_Query_Result.Replace("[Num]", queryResultCount);
            }

            set
            {
                queryResultCount = value;
                RaiseChange("QueryResultCount");
            }
        }

        public MainWindowViewModel()
        {
            IsCreateDialogShow = false;

            globalData = GlobalData.CreateInstance();

            ShowCreateDatabaseDialog = new DelegateCommand(ShowCreateDialog);
            ShowAttatchDatabaseDialog = new DelegateCommand(ShowAttatchDialog);
            SelectDBItemCommand = new DelegateCommand<RoutedEventArgs>(SelectDBFunc);
            OpenDatabaseCommand = new DelegateCommand(OpenDatabase, () => { return (SelectedDBItem != null && SelectedDBItem.OpenState == false && SelectedDBItem.Children == null); });
            ExecuteQueryCommand = new DelegateCommand(ExecuteQuery, () => { return !string.IsNullOrEmpty(SQLStr); });
            FetchSQLStrCommand = new DelegateCommand<RoutedEventArgs>(FetchSQLStr);
            DeleteDBCommand = new DelegateCommand(DeleteDatabase,()=> { return SelectedDBItem != null && SelectedDBItem.Children == null; });

            DatabaseItemList = LoadDatabaseList();
            StatusInfo = Properties.Resources.Txt_Status_Ready;
        }

        public void ShowCreateDialog()
        {
            CreateDatabase dialog = new Views.CreateDatabase();
            dialog.ShowDialog();
        }

        public void ShowAttatchDialog()
        {
            AttatchDatabase dialog = new AttatchDatabase();
            dialog.ShowDialog();
        }

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
            //TODO
            List<DatabaseItem> childrenItem = DatabaseItemList.First().Children;
            var openedItem = childrenItem.Where(x => x.OpenState == true).FirstOrDefault();

            if (openedItem != null && openedItem.FilePath != SelectedDBItem.FilePath)
            {
                globalData.dbHelper.CloseLocalDB(openedItem.FilePath);
                openedItem.OpenState = false;
                var index = childrenItem.IndexOf(openedItem);
                childrenItem[index] = openedItem;
                DatabaseItemList[0].Children = childrenItem;
            }

            var result = globalData.dbHelper.OpenLocalDB(SelectedDBItem.FilePath);

            if(result == true)
            {
                StatusInfo =Properties.Resources.Txt_OpenDB + "[" + SelectedDBItem.DisplayName + "]" + Properties.Resources.Txt_Status_Success + "(" + SelectedDBItem.FilePath + ")";
                SelectedDBItem.OpenState = true;
            }
            else
            {
                StatusInfo = SelectedDBItem.DisplayName + Properties.Resources.Txt_OpenDB;
            }
        }

        public void ExecuteQuery()
        {
            QueryResultTable = globalData.dbHelper.ExecuteQuery(SQLStr).DefaultView;

            QueryResultCount = QueryResultTable.Count.ToString();
        }

        public void FetchSQLStr(RoutedEventArgs args)
        {
            RichTextBox rTbox = args.Source as RichTextBox;

            if(rTbox != null)
            {
                FlowDocument flowDocument = rTbox.Document;
                TextRange a = new TextRange(flowDocument.ContentStart, flowDocument.ContentEnd);

                SQLStr = a.Text;
            }
        }

        public void DeleteDatabase()
        {

        }
    }
}

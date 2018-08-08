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
        GlobalData globalData;
        public ICommand ShowCreateDatabaseDialog { get; private set; }
   
        public bool IsCreateDialogShow { get; set; }   

        public MainWindowViewModel()
        {
            IsCreateDialogShow = false;

            globalData = GlobalData.CreateInstance();

            ShowCreateDatabaseDialog = new DelegateCommand(ShowCreateDialog,()=> {return  !IsCreateDialogShow; });

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
    }
}

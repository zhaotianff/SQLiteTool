using SQLiteTool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteTool.Model;
using System.Windows.Input;
using SQLiteTool.Commands;
using System.Windows;
using System.Xml.Linq;

namespace SQLiteTool.ViewModels
{
    public class CreateDatabaseViewModel : NotifyPropertyBase
    {
        private string alias;
        private string filePath;
        private bool isNameFocused;
        private bool? createDialogResult;

        GlobalData globalData = GlobalData.CreateInstance();

        public string Alias
        {
            get
            {
                return alias;
            }

            set
            {
                alias = value;
                RaiseChange("Alias");
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

        public bool? CreateDialogResult
        {
            get
            {
                return createDialogResult;
            }

            set
            {
                createDialogResult = value;
                RaiseChange("CreateDialogResult");
            }
        }

        public ICommand CreateDatabaseCommand { get; private set; }

        public ICommand BrowseDBPathCommand { get; private set; }

        public ICommand CloseDialogCommand { get; private set; }


        public CreateDatabaseViewModel()
        {           
            CreateDatabaseCommand = new DelegateCommand(CreateDatabase,()=> { return (!string.IsNullOrEmpty(FilePath) && !string.IsNullOrEmpty(Alias)); });
            BrowseDBPathCommand = new DelegateCommand(BrowseDBPath);
            CloseDialogCommand = new DelegateCommand(CloseDialog);

            CreateDialogResult = null;
        }


        public void CreateDatabase()
        {
            if (System.IO.File.Exists(FilePath + "\\" + Alias + ".db"))
            {
                MessageBox.Show(Properties.Resources.Txt_DBExist);
                Alias = "";
                IsNameFocused = false;
                IsNameFocused = true;
                return;
            }
            bool result = GlobalData.CreateInstance().dbHelper.CreateDBFile(Alias, FilePath);
            if (result == false)
            {
                globalData.mainWindowViewModel.ShowStatusInfo(Properties.Resources.Txt_CreateDBFault);
            }
            else
            {

                globalData.mainWindowViewModel.ShowStatusInfo(Properties.Resources.Txt_CreateDBSuccess);
            }

            UpdateDBConfig(FilePath, Alias, Alias);
            globalData.mainWindowViewModel.RefreshDatabaseList();
            CreateDialogResult = true;           
        }

        public void BrowseDBPath()
        {
            System.Windows.Forms.FolderBrowserDialog folderBorwser = new System.Windows.Forms.FolderBrowserDialog();
            folderBorwser.Description = Properties.Resources.Txt_ChooseDBPath;
            if (folderBorwser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePath = folderBorwser.SelectedPath;
            }
        }

        public void CloseDialog()
        {
            CreateDialogResult = false;
        }

        public void UpdateDBConfig(string filePath,string name,string description)
        {
            try
            {
                globalData.xmlHelper.OpenXml(globalData.ConfigFilePath);
                globalData.xmlHelper.GetDocument().Root.Element("LocalDBList").Add(new XElement(
                    "Item",new XElement("FilePath", filePath + "\\" + name + ".db"),new XElement("DisplayName", name),new XElement("Description", description)
                    ));
                globalData.xmlHelper.Save();
            }
            catch(Exception ex)
            {
                GlobalData.CreateInstance().log.Error(ex.Message);
            }

        }
    }
}

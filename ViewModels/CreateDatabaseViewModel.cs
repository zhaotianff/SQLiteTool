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

namespace SQLiteTool.ViewModels
{
    public class CreateDatabaseViewModel : NotifyPropertyBase
    {
        private string name;
        private string filePath;
        private bool isNameFocused;
        private bool? createDialogResult;

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
            CreateDatabaseCommand = new DelegateCommand(CreateDatabase,()=> { return (!string.IsNullOrEmpty(FilePath) && !string.IsNullOrEmpty(Name)); });
            BrowseDBPathCommand = new DelegateCommand(BrowseDBPath);
            CloseDialogCommand = new DelegateCommand(CloseDialog);

            CreateDialogResult = null;
        }


        public void CreateDatabase()
        {
            if (System.IO.File.Exists(FilePath + "\\" + Name))
            {
                MessageBox.Show(Properties.Resources.Txt_DBExist);
                Name = "";
                IsNameFocused = false;
                IsNameFocused = true;
                return;
            }
            bool result = GlobalData.CreateInstance().dbHelper.CreateDBFile(Name, FilePath);
            if (result == false)
            {
                MessageBox.Show(Properties.Resources.Txt_CreateDBFault);
            }
            else
            {

                MessageBox.Show(Properties.Resources.Txt_CreateDBSuccess);
            }
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
    }
}

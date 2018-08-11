using SQLiteTool.Commands;
using SQLiteTool.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SQLiteTool.ViewModels
{
    class AttatchDatabaseViewModel : NotifyPropertyBase
    {
        private string filePath;
        private bool? attatchDialogResult;
        private string alias;

        GlobalData globalData = GlobalData.CreateInstance();

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

        public bool? AttatchDialogResult
        {
            get
            {
                return attatchDialogResult;
            }

            set
            {
                attatchDialogResult = value;
                RaiseChange("AttatchDialogResult");
            }
        }

        public ICommand AttatchDatabaseCommand { get; private set; }

        public ICommand BrowseDBPathCommand { get; private set; }

        public ICommand CloseDialogCommand { get; private set; }


        public AttatchDatabaseViewModel()
        {
            AttatchDatabaseCommand = new DelegateCommand(AttatchDatabase, () => { return (!string.IsNullOrEmpty(FilePath) && !string.IsNullOrEmpty(Alias)); });
            BrowseDBPathCommand = new DelegateCommand(BrowseDBPath);
            CloseDialogCommand = new DelegateCommand(CloseDialog);

            AttatchDialogResult = null;
        }

        public void AttatchDatabase()
        {
            try
            {
                var checkExistResult = globalData.mainWindowViewModel.DatabaseItemList[0].Children.Where(x => x.FilePath == FilePath).FirstOrDefault();
                if(checkExistResult != null)
                {
                    System.Windows.MessageBox.Show(Properties.Resources.Txt_DBExist);
                    return;
                }

                globalData.createDatabaseViewModel.UpdateDBConfig(FilePath,Alias,Alias);
                globalData.mainWindowViewModel.RefreshDatabaseList();
                globalData.mainWindowViewModel.ShowStatusInfo(Properties.Resources.Txt_Dialog_Attatch + Properties.Resources.Txt_Status_Success);
            }
            catch (Exception ex)
            {
                globalData.mainWindowViewModel.ShowStatusInfo(Properties.Resources.Txt_Dialog_Attatch + Properties.Resources.Txt_Status_Failed);
                globalData.log.Error(ex.Message);
            }
            AttatchDialogResult = true;
        }

        public void BrowseDBPath()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.Filter = Properties.Resources.Txt_Dialog_Browse_Filter + "|*.db|" + Properties.Resources.Txt_Dialog_Browse_All + "|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Title = Properties.Resources.Txt_Dialog_Attatch;
            if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
            }
        }

        public void CloseDialog()
        {
            AttatchDialogResult = false;
        }
    }
}

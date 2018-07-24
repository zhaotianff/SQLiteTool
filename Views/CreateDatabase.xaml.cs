using SQLiteTool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SQLiteTool.Util;
using SQLiteTool.ViewModels;

namespace SQLiteTool.Views
{
    /// <summary>
    /// CreateDatabase.xaml 的交互逻辑
    /// </summary>
    public partial class CreateDatabase : Window
    {
        CreateDatabaseViewModel context;
        
        public CreateDatabase()
        {
            InitializeComponent();
            context = new CreateDatabaseViewModel();
            this.DataContext = context;
        }

        #region Command
        private void CreateDatabase_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (context != null)
            {
                if (!string.IsNullOrEmpty(context.FilePath) && !string.IsNullOrEmpty(context.Name))
                {
                    e.CanExecute = true;
                }
            }
        }

        private void CreateDatabase_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (System.IO.File.Exists(context.FilePath + "\\" + context.Name))
            {
                MessageBox.Show(Properties.Resources.Txt_DBExist);
                context.Name = "";
                context.IsNameFocused = false;
                context.IsNameFocused = true;
                return;
            }
            bool result = GlobalData.CreateInstance().dbHelper.CreateDBFile(context.Name, context.FilePath);
            if(result == false)
            {
                MessageBox.Show(Properties.Resources.Txt_CreateDBFault);
            }
            else
            {

                MessageBox.Show(Properties.Resources.Txt_CreateDBSuccess);
            }
            this.Close();
        }

        private void BrowseDBPath_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBorwser = new System.Windows.Forms.FolderBrowserDialog();
            folderBorwser.Description = Properties.Resources.Txt_ChooseDBPath;
            if(folderBorwser.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                context.FilePath = folderBorwser.SelectedPath;
            }
        }
        #endregion

     
    }
}

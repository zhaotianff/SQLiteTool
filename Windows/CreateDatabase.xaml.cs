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

namespace SQLiteTool.Windows
{
    /// <summary>
    /// CreateDatabase.xaml 的交互逻辑
    /// </summary>
    public partial class CreateDatabase : Window
    {
        CreateDatabaseInfo context;
        
        public CreateDatabase()
        {
            InitializeComponent();
            context = new CreateDatabaseInfo();
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
            bool result = GlobalData.CreateInstance().dbHelper.CreateDBFile(context.Name, context.FilePath);
            if(result == false)
            {
                MessageBox.Show("创建失败");
            }
            this.Close();
        }
        #endregion
    }
}

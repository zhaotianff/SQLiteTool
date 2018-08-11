using SQLiteTool.Util;
using SQLiteTool.ViewModels;
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

namespace SQLiteTool.Views
{
    /// <summary>
    /// AttatchDatabase.xaml 的交互逻辑
    /// </summary>
    public partial class AttatchDatabase : Window
    {
        AttatchDatabaseViewModel context;
        public AttatchDatabase()
        {
            InitializeComponent();
            context = new AttatchDatabaseViewModel();
            GlobalData.CreateInstance().attatchDatabaseViewModel = context;
            this.DataContext = context;
        }
    }
}

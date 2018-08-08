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
     
    }
}

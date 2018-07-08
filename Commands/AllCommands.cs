using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SQLiteTool.Commands
{
    public static class AllCommands
    {
        #region Function

        private static RoutedUICommand createDatabase = new RoutedUICommand("Create Database", "CreateDatabase", typeof(AllCommands),
            new InputGestureCollection { new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Alt, "Ctrl + Alt+ N") });

        public static RoutedUICommand CreateDatabase
        {
            get
            {
                return createDatabase;
            }
            set
            {
                createDatabase = value;
            }
        }
        #endregion
    }
}

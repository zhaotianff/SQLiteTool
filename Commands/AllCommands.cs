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

        /// <summary>
        /// Show Create Database Dialog, Ctrl + Alt + N
        /// </summary>
        public static RoutedUICommand showCreateDatabaseDialog  = new RoutedUICommand("Show Create Database Dialog", "ShowCreateDatabaseDialog", typeof(AllCommands),
            new InputGestureCollection { new KeyGesture(Key.N, ModifierKeys.Control | ModifierKeys.Alt, "Ctrl + Alt + N") });

        public static RoutedUICommand ShowCreateDatabaseDialog
        {
            get
            {
                return showCreateDatabaseDialog;
            }
            set
            {
                showCreateDatabaseDialog = value;
            }
        }

        /// <summary>
        /// Create Database
        /// </summary>
        public static RoutedUICommand createDatabase = new RoutedUICommand("Create Database","CreateDatabase",typeof(AllCommands));

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

        /// <summary>
        /// Browse Database Path
        /// </summary>
        public static RoutedUICommand browseDBPath = new RoutedUICommand("Browse Database Path","BrowseDBPath",typeof(AllCommands));

        public static RoutedUICommand BrowseDBPath
        {
            get
            {
                return browseDBPath;
            }
        }

        #endregion
    }
}


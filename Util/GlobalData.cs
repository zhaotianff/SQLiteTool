using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using SQLiteTool.Model;
using NLog;
using SQLiteTool.ViewModels;

namespace SQLiteTool.Util
{
    class GlobalData
    {
        private static object obj = new object();
        private static GlobalData _globalData;

        public DBHelper dbHelper;
        public XmlHelper xmlHelper;

        public Logger log = LogManager.GetLogger("logger");

        public CreateDatabaseViewModel createDatabaseViewModel;
        public MainWindowViewModel mainWindowViewModel;

        /// <summary>
        /// Singleton class, get GlobalData Instance
        /// </summary>
        /// <returns></returns>
        public static GlobalData CreateInstance()
        {
            if(_globalData == null)
            {
                lock(obj)
                {
                    if (_globalData == null)
                        _globalData = new GlobalData();
                }
            }
            return _globalData;
        }

        public GlobalData()
        {
            dbHelper = new DBHelper(log);
            xmlHelper = new XmlHelper();
        }

    }
}

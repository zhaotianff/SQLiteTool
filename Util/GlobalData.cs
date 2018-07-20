using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using SQLiteTool.Model;
using NLog;

namespace SQLiteTool.Util
{
    class GlobalData
    {
        private static object obj = new object();
        private static GlobalData _globalData;

        public DBHelper dbHelper;
        public XmlHelper xmlHelper;

        public Logger log = LogManager.GetLogger("logger");

        public List<DatabaseItem> dbList = new List<DatabaseItem>();

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
            dbHelper = new DBHelper();
            xmlHelper = new XmlHelper();
        }

    }
}

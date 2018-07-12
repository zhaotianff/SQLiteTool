using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace SQLiteTool.Util
{
    class GlobalData
    {
        private static object obj = new object();
        private static GlobalData _globalData;

        public DBHelper dbHelper;
        public XmlHelper xmlHelper;

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

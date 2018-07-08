using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;

namespace SQLiteTool.Util
{
    class DBHelper
    {
        private void CreateDB(string dbName, string DbFilePath)
        {

        }

        private bool OpenLocalDB(string dbName)
        {
            try
            {
                string conStr = "server=.database=" + dbName;
                SQLiteConnection con = new SQLiteConnection(conStr);
                con.Open();
                if (con.State == ConnectionState.Open)
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        private bool CloseLocalDB(string dbName)
        {
            return true;
        }

    }
}

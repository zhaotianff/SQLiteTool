using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace SQLiteTool.Util
{
    class DBHelper
    {
        /// <summary>
        /// Create Database File
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="DbFilePath"></param>
        /// <returns></returns>
        public bool CreateDBFile(string dbName, string DbFilePath)
        {
            string dbFullPath = DbFilePath + "\\" + dbName;

            Process p = new Process();
            ProcessStartInfo psInfo = new ProcessStartInfo();
            psInfo.FileName = "type nul>";
            psInfo.Arguments = dbFullPath;
            p.StartInfo = psInfo;
            p.Start();
            if (System.IO.File.Exists(dbFullPath))
                return true;
            else
                return false;          
        }

        public bool OpenLocalDB(string dbName)
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

        public bool CloseLocalDB(string dbName)
        {
            return true;
        }

    }
}

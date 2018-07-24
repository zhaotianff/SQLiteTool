using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using SQLiteTool.Model;
using NLog;

namespace SQLiteTool.Util
{
    class DBHelper
    {
        private Logger log = GlobalData.CreateInstance().log;

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
            psInfo.FileName = "cmd";        
            psInfo.CreateNoWindow = true;
            psInfo.UseShellExecute = false;
            psInfo.RedirectStandardInput = true;   
            psInfo.RedirectStandardOutput = true;  
            psInfo.RedirectStandardError = true;  
            p.StartInfo = psInfo;
            p.Start();
            p.StandardInput.WriteLine("type nul>" + dbFullPath + "&exit");
            p.StandardInput.AutoFlush = true;
            p.WaitForExit();
            p.Close();
            if (System.IO.File.Exists(dbFullPath))
                return true;
            else
                return false;          
        }

        public bool UpdateDBList(DatabaseItem item)
        {

            return false;
        }

        public List<DatabaseItem> GetDatabaseList()
        {
            List<DatabaseItem> list = new List<DatabaseItem>();
            try
            {

            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            return list;
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

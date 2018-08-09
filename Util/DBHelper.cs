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
        private Logger log;
        SQLiteConnection con;

        public DBHelper(Logger _log)
        {
            log = _log;
        }
        /// <summary>
        /// Create Database File
        /// </summary>
        /// <param name="dbName"></param>
        /// <param name="DbFilePath"></param>
        /// <returns></returns>
        public bool CreateDBFile(string dbName, string DbFilePath)
        {
            string dbFullPath = DbFilePath + "\\" + dbName + ".db";

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

        public bool OpenLocalDB(string dbPath)
        {
            try
            {
                string conStr = string.Format("Data Source={0};Version=3;", dbPath);
                con= new SQLiteConnection(conStr);
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

        public bool CloseLocalDB(string dbPath)
        {
            return true;
        }

        public DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql,con);
                sda.Fill(dt);
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }
            return dt;
        }

    }
}

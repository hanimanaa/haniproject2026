using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.OleDb;

namespace ViewModel
{
    public class DBFunctuins
    {
        protected OleDbConnection conObj;
        protected OleDbCommand cmd;
        protected OleDbDataReader reader;
     

        public DBFunctuins()
        {
            conObj = new OleDbConnection();
            cmd = new OleDbCommand();
        }

     
        public OleDbConnection GenerateConnection(string dbFileName)
        {
            try
            {
                if (dbFileName.Contains(".mdb"))
                    conObj.ConnectionString =
                        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        Path() + "\\App_Data\\" + dbFileName;
                else
                    conObj.ConnectionString =
                        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        Path() + "\\App_Data\\" + dbFileName;
                conObj.Open();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                conObj.Close();
            }
            return conObj;
        }
       
        private static string Path()
        {
            String[] arguments = Environment.GetCommandLineArgs();
            string s;
            if (arguments.Length == 1)
            { s = arguments[0]; }
            else
            {
                s = arguments[1];
                s = s.Replace("/service:", "");
            }
            string[] ss = s.Split('\\');

            int x = ss.Length - 4;
            ss[x] = "ViewModel";
            Array.Resize(ref ss, x+1 );

            string str = String.Join("\\", ss);
            return str;
        }
 
        public DataTable Select(string sqlString, string dbFileName)
        {
            conObj = GenerateConnection(dbFileName);
            DataTable dt = null;
            DataSet dsUser = new DataSet();
            try
            {
                OleDbDataAdapter daObj = new OleDbDataAdapter(sqlString, conObj);
                daObj.Fill(dsUser);
                dt = dsUser.Tables[0];
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("error: " + ex.Message);
            }
            finally
            {
                conObj.Close();
            }
            return dt;
        }


        public OleDbCommand GenerateOleDBCommand(string sqlStr, string dbFileName)
        {
            conObj = GenerateConnection(dbFileName);
            cmd = new OleDbCommand(sqlStr, conObj);
            return cmd;
        }

        public int ChangeTable(string sqlString, string dbFileName)
        {
            int records = 0;
            cmd = GenerateOleDBCommand(sqlString, dbFileName);
            conObj.Open();
            try
            {
                records = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("error: " + ex.Message);
            }
            finally
            {
                conObj.Close();
            }
            return records;
        }
    }
}
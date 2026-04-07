using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace ViewModel
{
    public class CityDB : DBFunctuins
    {
        City c = null;
        private CityList list = new CityList();
       // DBFunctuins dbf = new DBFunctuins();

        public CityDB() : base() { }

        private City CreateModel(City c)
        {
            c.cityNum = int.Parse(reader["cityNum"].ToString());
            c.cityName = reader["cityName"].ToString();
            return c;
        }

        // Add City
        public int AddCity(City c)
        {
            string insertSql = string.Format("Insert into CityTbl "
                + "(cityNum,cityName) values ({0},'{1}')", c.cityNum, c.cityName);
            return base.ChangeTable(insertSql, "MyDatabase.accdb");
        }

        // Select All Cities
        public CityList SelectAllCities()
        {
            try
            {
                string sqlStr = "Select * From CityTbl";
                cmd = GenerateOleDBCommand(sqlStr, "MyDatabase.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    c = new City();
                    list.Add(CreateModel(c));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (this.conObj.State == System.Data.ConnectionState.Open)
                    this.conObj.Close();
            }
            return list;
        }


        // Select City By City Num
        public City SelectCityByNum(int num)
        {
            list = SelectAllCities();
            City c = list.Find(item => item.cityNum == num);
            return c;
        }


        // City Exist
        public bool CityExist(int cityNum)
        {
            DataTable dt = null;
            string sqlStr = "Select * from CityTbl " + "where cityNum=" + cityNum + "";
            dt = base.Select(sqlStr, "MyDatabase.accdb");
            if (dt == null)
                return false;
            return (dt.Rows.Count > 0);
        }

        // Order By City Name
        //**
        public List<City> OrderByCityName()
        {
            list = SelectAllCities();
            return list.OrderBy(item => item.cityName).ToList();
        }

        // update City
        public int UpdateCity(City c)
        {
            string updateSql = string.Format("Update CityTbl SET "
                + "cityName='" + c.cityName 
                + "' where cityNum=" + c.cityNum + "");

            return base.ChangeTable(updateSql, "MyDatabase.accdb");
        }
        // Delete City By cityNum
        public int DeleteCityByNum(int cityNum)
        {
            string delSql = string.Format("Delete from CityTbl "
                + "where cityNum=" + cityNum + "");
            return base.ChangeTable(delSql, "MyDatabase.accdb");
        }
    }
}

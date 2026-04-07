using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace ViewModel
{
    public class UserDB : DBFunctuins
    {
        User user = null;
        private UserList list = new UserList();
        //DBFunctuins dbf = new DBFunctuins();

        public UserDB() : base() { }

        private User CreateModel (User c)
        {
            user.userEmail = reader["userEmail"].ToString();
            user.userPassword = reader["userPassword"].ToString();
            user.fName = reader["fName"].ToString();
            user.lName = reader["lName"].ToString();

            

            string birthday = ((DateTime)reader["birthday"]).ToString("dd/MM/yyyy");
            string format = "dd/MM/yyyy";
            user.birthday = DateTime.ParseExact(birthday, format, System.Globalization.CultureInfo.InvariantCulture);

            user.gender = reader["gender"].ToString();
            user.tel = reader["tel"].ToString();
                       
            int cityNum = int.Parse(reader["cityNum"].ToString());
            CityDB cdb = new CityDB();
            user.city = cdb.SelectCityByNum(cityNum);

            user.message = bool.Parse(reader["message"].ToString());

            return c;
        }

        // Add User
        public int AddUser(User c)
        {
            string insertSql = string.Format("Insert into UserTbl "
                + "(userEmail,userPassword,fName,lName,birthday,gender,tel,cityNum,message)"
                + " values ('{0}','{1}','{2}','{3}',#{4}#,'{5}','{6}',{7},{8})"
                , c.userEmail, c.userPassword, c.fName, c.lName, c.birthday,c.gender,c.tel,c.city.cityNum,c.message);

            return base.ChangeTable(insertSql, "MyDatabase.accdb");
        }
  
        // Delete User By Email
        public int DeleteUserByEmail(string uEmail)
        {
            string delSql = string.Format("Delete from UserTbl " 
                + "where userEmail='" + uEmail + "'");
            return base.ChangeTable(delSql, "MyDatabase.accdb");
        }
        // update User
        public int UpdateUser(User c)
        {
            string updateSql = string.Format("Update UserTbl SET "
                + "userPassword='" + c.userPassword + "' ,fName='" + c.fName + "' ,lName='" + c.lName
                + "' ,birthday=#" + c.birthday + "# ,gender='" + c.gender + "' ,tel='" + c.tel +"' ,cityNum=" + c.city.cityNum + " ,message=" + c.message
                + " where userEmail='" + c.userEmail + "'");

            return base.ChangeTable(updateSql, "MyDatabase.accdb");
        }
        // Select Users
        private UserList SelectUsers(string sqlStr)
        {
            try
            {
                cmd=GenerateOleDBCommand(sqlStr, "MyDatabase.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    user = new User();
                    list.Add(CreateModel(user));
                }
            }
            catch(Exception ex)
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

        // Select All Users
        public UserList SelectAllUsers()
        {
            string sqlStr = "Select * From UserTbl";
            UserList list = SelectUsers(sqlStr);
            return list;
        }

        // Search Users
        public UserList SearchUsers(User user)
        {        
            string sqlStr = "Select * From UserTbl where " +
             "userEmail ='" + user.userEmail + "' OR " +
             "fName ='" + user.fName + "' OR " +
             "lName ='" + user.lName + "' OR " +
             "tel ='" + user.tel + "' OR " +
             "cityNum =" + user.city.cityNum + "";

            UserList list = SelectUsers(sqlStr);
            return list;
        }

        // Select User FullName By Email
        // return value by using DBFunctuins select function 
        public string SelectUserFullNameByEmail(string uEmail)
        {
            DataTable dt = null;
            string sqlStr = "Select fName,lName From UserTbl where userEmail = '" + uEmail + "'";
            dt = base.Select(sqlStr, "MyDatabase.accdb");
            if (dt == null)
                return "user not found";
            return dt.Rows[0][0].ToString()+ " " + dt.Rows[1][0].ToString();
        }

        // Select User By Email
        // return value by using list Find function 
        public User SelectUserByEmail(string uEmail)
        {
            list = SelectAllUsers();
            User c = list.Find(item => item.userEmail == uEmail);
            return c;
        }

        // User Exist
        public bool UserExist(string uEmail)
        {
            DataTable dt = null;
            string sqlStr = "Select * from UserTbl " + "where userEmail='" + uEmail + "'";
            dt = base.Select(sqlStr, "MyDatabase.accdb");
            if (dt == null)
                return false;
            return (dt.Rows.Count>0);
        }

        // Count Users
        public int CountUsers()
        {
            return SelectAllUsers().Count;
        }
    
    }
}

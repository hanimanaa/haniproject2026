using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace WpfHobbies
{
    public class Validation
    {
        public Validation() { }

        // בדיקת תקינות
        public static bool IsValidString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }
            return true;
        }
        public static bool IsValidNumber(string num)
        {
            // number
            string numRegex = @"^\d+$";
            if (string.IsNullOrWhiteSpace(num))
            {
                return false;
            }
            else
            {
                if (!Regex.IsMatch(num, numRegex))
                    return false;
                return true;
            }
        }
        public static bool IsValidEmail(string email)
        {
            string emailRegex = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            else
            {
                if (!Regex.IsMatch(email, emailRegex))
                    return false;
                return true;
            }
        }
        public static bool IsValidPhone(string phone)
        {
            // number
            string numRegex = @"^\d+$";
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }
            else
            {
                if (!Regex.IsMatch(phone, numRegex) || phone.Length != 10)
                    return false;
                return true;
            }
        }
        public static bool IsValidPassword(string password)
        {
            // 0-9 a-z A-Z
            string passwordRegex = @"^[0-9a-zA-Z]+$";
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }
            else
            {
                if (!Regex.IsMatch(password, passwordRegex))
                    return false;
                return true;
            }
        }  

    }
}

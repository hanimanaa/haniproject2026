using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Model;
using ViewModel;


namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        AdminDB adminDB = new AdminDB();
        CityDB cityDB = new CityDB();
        UserDB userDB = new UserDB();
        ProductDB productDB = new ProductDB();
        OrderDB orderDB = new OrderDB();
        CategoryDB categoryDB = new CategoryDB();
        MailBoxDB mailBoxDB = new MailBoxDB();




        // AdminDB
        public bool AdminExist(string uEmail, string uPassword)
        {
            return adminDB.AdminExist(uEmail,uPassword);
        }

        // OrderDB
        public int AddOrder(Order order)
        {
            return orderDB.AddOrder(order);
        }     
        public OrderList SelectAllOrders()
        {
            return orderDB.SelectAllOrders();
        }
        public OrderList SelectOrdersByUserEmail(string email)
        {
            return orderDB.SelectOrdersByUserEmail(email);
        }
        public int DeleteOrderByNum(int num)
        {
            return orderDB.DeleteOrderByNum(num);
        }

        public int UpdateOrder(Order o)
        {
            return orderDB.UpdateOrder(o);
        }

        public OrderList SelectOrdersByOrderDate(string userEmail, DateTime orderDate)
        {
            return orderDB.SelectOrdersByOrderDate(userEmail, orderDate);
        }
        public OrderList SelectOrdersByOrderStatus(string userEmail, string orderStatus)
        {
            return orderDB.SelectOrdersByOrderStatus(userEmail, orderStatus);
        }


        // CategoryDB

        public CategoryList SelectAllCategories()
        {
            return categoryDB.SelectAllCategories();
        }
        
        public Category SelectCategoryByNum(int num)
        {
            return categoryDB.SelectCategoryByNum(num);
        }

        // UserDB
        public int AddUser(User c)
        {
            return userDB.AddUser(c);
        }
        public int DeleteUserByEmail(string uEmail)
        {
            return userDB.DeleteUserByEmail(uEmail);
        }
        public int UpdateUser(User c)
        {
            return userDB.UpdateUser(c);
        }       
        public UserList SelectAllUsers()
        {
            return userDB.SelectAllUsers();
        }
        public string SelectUserFullNameByEmail(string uEmail)
        {
            return userDB.SelectUserFullNameByEmail(uEmail);
        }
        public User SelectUserByEmail(string uEmail)
        {
            return userDB.SelectUserByEmail(uEmail);
        }
        public UserList SearchUsers(User user)
        {
            return userDB.SearchUsers(user);
        }
        public bool UserExist(string uEmail)
        {
            return userDB.UserExist(uEmail);
        }
        public int CountUsers()
        {
            return userDB.CountUsers();
        }



        // CityDB       
        public int AddCity(City c)
        {
            return cityDB.AddCity(c);
        }
        public CityList SelectAllCities()
        {
            return cityDB.SelectAllCities();
        }
        public City SelectCityByNum(int num)
        {
            return cityDB.SelectCityByNum(num);
        }
        public int UpdateCity(City c)
        {
            return cityDB.UpdateCity(c);
        }
        public int DeleteCityByNum(int cityNum)
        {
            return cityDB.DeleteCityByNum(cityNum);
        }


        public bool CityExist(int cityNum)
        {
            return cityDB.CityExist(cityNum);
        }
        public List<City> OrderByCityName()
        {
            return cityDB.OrderByCityName();
        }

        // ProductDB     
        public int AddProduct(Product p)
        {
            return productDB.AddProduct(p);
        }     
        public ProductList SelectAllProducts()
        {
            return productDB.SelectAllProducts();
        }        
        public Product SelectProductByNum(int num)
        {
            return productDB.SelectProductByNum(num);
        }

        // MailBoxDB        
        public int AddMailBox(MailBox mailBox)
        {
            return mailBoxDB.AddMailBox(mailBox);
        }
        public int DeleteMailBoxByNum(int num)
        {
            return mailBoxDB.DeleteMailBoxByNum(num);
        }    
        public MailBoxList SelectAllMailBox()
        {
            return mailBoxDB.SelectAllMailBox();
        }


        //*****************************************************************
        public int Add (int x, int y)
        {
            return x + y;
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

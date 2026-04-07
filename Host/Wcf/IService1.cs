using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ViewModel;
using Model;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        // AdminDB 
        [OperationContract]
        bool AdminExist(string uEmail, string uPassword);


        // orderDB
        [OperationContract]
        int AddOrder(Order order);

        [OperationContract]
        OrderList SelectAllOrders();

        [OperationContract]
        OrderList SelectOrdersByUserEmail(string email);

        [OperationContract]
        int DeleteOrderByNum(int num);

        [OperationContract]
        int UpdateOrder(Order o);

        [OperationContract]
        OrderList SelectOrdersByOrderDate(string userEmail, DateTime orderDate);

        [OperationContract]
        OrderList SelectOrdersByOrderStatus(string userEmail, string orderStatus);

        // CategoryDB
        [OperationContract]
        CategoryList SelectAllCategories();

        [OperationContract]
        Category SelectCategoryByNum(int num);      


        // CityDB
        [OperationContract]
        int AddCity(City c);

        [OperationContract]
        CityList SelectAllCities();

        [OperationContract]
        City SelectCityByNum(int num);

        [OperationContract]
        bool CityExist(int cityNum);

        [OperationContract]
        List<City> OrderByCityName();

        [OperationContract]
        int UpdateCity(City c);

        [OperationContract]
        int DeleteCityByNum(int cityNum);


        // UserDB      
        [OperationContract]
        int AddUser(User c);

        [OperationContract]
        int DeleteUserByEmail(string uEmail);

        [OperationContract]
        int UpdateUser(User c);

        [OperationContract]
        UserList SelectAllUsers();

        [OperationContract]
        string SelectUserFullNameByEmail(string uEmail);

        [OperationContract]
        User SelectUserByEmail(string uEmail);

        [OperationContract]
        bool UserExist(string uEmail);

        [OperationContract]
        UserList SearchUsers(User user);

        [OperationContract]
        int CountUsers();


        // ProductDB    
        [OperationContract]
        int AddProduct(Product p);

        [OperationContract]
        ProductList SelectAllProducts();

        [OperationContract]
        Product SelectProductByNum(int num);

        // MailBoxDB
        [OperationContract]
        int AddMailBox(MailBox mailBox);

        [OperationContract]
        int DeleteMailBoxByNum(int num);

        [OperationContract]
        MailBoxList SelectAllMailBox();



        //**********************************************************************

        [OperationContract]
        int Add(int x , int y);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceLibrary1.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

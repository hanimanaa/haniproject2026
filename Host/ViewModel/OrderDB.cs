using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace ViewModel
{
    public class OrderDB : DBFunctuins
    {
        private Order order = null;
        private OrderList list = new OrderList();
        //DBFunctuins dbf = new DBFunctuins();

        public OrderDB() : base() { }

        private Order CreateModel(Order order)
        { 
            order.orderNum =int.Parse(reader["orderNum"].ToString());

            string orderDate = ((DateTime)reader["orderDate"]).ToString("dd/MM/yyyy");
            string format = "dd/MM/yyyy";
            order.orderDate = DateTime.ParseExact(orderDate, format, System.Globalization.CultureInfo.InvariantCulture);

            order.quantity = int.Parse(reader["quantity"].ToString());
            order.orderStatus = reader["orderStatus"].ToString();

            UserDB udb = new UserDB();
            string userEmail = reader["userEmail"].ToString();
            order.user = udb.SelectUserByEmail(userEmail);

            ProductDB pdb = new ProductDB();
            int productNum = int.Parse(reader["productNum"].ToString());
            order.product = pdb.SelectProductByNum(productNum);

            return order;
        }

        // Add Order
        public int AddOrder(Order order)
        {
            string insertSql = string.Format("Insert into OrderTbl "
                + "(orderDate,userEmail,productNum,quantity,orderStatus)" 
                +" values (#{0}#,'{1}',{2},{3},'{4}')"
                , order.orderDate, order.user.userEmail, order.product.productNum, order.quantity, order.orderStatus);
            return base.ChangeTable(insertSql, "MyDatabase.accdb");
        }
        // Select Orders
        private OrderList SelectOrders(string sqlStr)
        {          
            try
            {                
                cmd = GenerateOleDBCommand(sqlStr, "MyDatabase.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    order = new Order();
                    list.Add(CreateModel(order));
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

        // Select All Orders
        public OrderList SelectAllOrders()
        {
            string sqlStr = "Select * From OrderTbl";
            OrderList list = SelectOrders(sqlStr);
            return list;
        }

        // Select Orders By Order Date
        //**
        public OrderList SelectOrdersByOrderDate(string userEmail , DateTime orderDate)
        {
            string sqlStr = "Select * From OrderTbl where userEmail='"+ userEmail + "' and orderDate=#" + orderDate + "#";
            OrderList list = SelectOrders(sqlStr);
            return list;
        }

        // Select Orders By Order status
        //**
        public OrderList SelectOrdersByOrderStatus(string userEmail, string orderStatus)
        {
            string sqlStr = "Select * From OrderTbl where userEmail='" + userEmail + "' and orderStatus='" + orderStatus + "'";
            OrderList list = SelectOrders(sqlStr);
            return list;
        }

        // Select Orders By User Email
        public OrderList SelectOrdersByUserEmail(string email)
        {
            OrderList list = new OrderList();
            Order order = null;
            try
            {
                string sqlStr = "Select * From OrderTbl where userEmail='"+ email + "'";
                cmd = GenerateOleDBCommand(sqlStr, "MyDatabase.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    order = new Order();
                    list.Add(CreateModel(order));
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
        // update Order
        public int UpdateOrder(Order o)
        {
            string updateSql = string.Format("Update OrderTbl SET "
                + "productNum=" + o.product.productNum + " ,quantity=" + o.quantity + " ,orderStatus='" + o.orderStatus + "'"
                + " where orderNum=" + o.orderNum + "");
            return base.ChangeTable(updateSql, "MyDatabase.accdb");
        }


        // Delete Order By num
        public int DeleteOrderByNum(int num)
        {
            string delSql = string.Format("Delete from OrderTbl "
                + "where orderNum=" + num + "");
            return base.ChangeTable(delSql, "MyDatabase.accdb");
        }

    }
}

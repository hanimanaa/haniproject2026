using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class ProductDB : DBFunctuins
    {
        Product product = null;
        ProductList ProductsList = new ProductList();

        public ProductDB() : base() { }

        private Product CreateModel(Product product)
        {
            product.productNum = int.Parse(reader["productNum"].ToString());
            product.productName = reader["productName"].ToString();
            product.price = double.Parse(reader["price"].ToString());
            product.imageUrl = reader["imageUrl"].ToString();
            product.description = reader["description"].ToString();

            int catNum = int.Parse(reader["catNum"].ToString());
            CategoryDB categoryDB = new CategoryDB();
            product.category = categoryDB.SelectCategoryByNum(catNum);

            product.expiredDate = DateTime.Parse(reader["expiredDate"].ToString());
            product.vegan = (reader["vegan"].ToString());

            return product;
        }

        public int AddProduct(Product product)
        {
            string insertSql = string.Format("Insert into ProductTbl "
                + "(productNum,productName,price,imageUrl,description,catNum,expiredDate,vegan)"
                + " values ({0},'{1}',{2},'{3}','{4}',{5},{6},{7})"
                ,product.productNum,product.productName,product.price,product.imageUrl,
                product.description,product.category.catNum,product.expiredDate,product.vegan);

            return base.ChangeTable(insertSql, "Database2026.accdb");

        }
        // Update product
        public int UpdateProduct(Product product)
        {
            string updateSql = string.Format("Update ProductTbl SET "
                + "productName={0},price={1},imageUrl={2},description={3}"
                + ",catNum={4},expiredDate={5},vegan={6}"
                + " where productNum={7}"
                ,product.productName, product.price, product.imageUrl,
                product.description, product.category.catNum, product.expiredDate,
                product.vegan, product.productNum);

            return base.ChangeTable(updateSql, "Database2026.accdb");
        }

        // Delete Product By ProductNum
        public int DeleteProductByProductNum(int productNum)
        {
            string delSql = string.Format("Delete from ProductTbl "
                + "where productNum= {0}", productNum) ;
            return base.ChangeTable(delSql, "Database2026.accdb");
        }
    }
}

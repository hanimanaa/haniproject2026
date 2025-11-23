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
            product.vegan = (reader["imageUrl"].ToString());





            return product;
        }
    }
}

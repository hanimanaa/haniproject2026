using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ViewModel
{
    public class CategoryDB : DBFunctuins
    {
        Category category = null;
        CategoryList categoriesList = new CategoryList();

        public CategoryDB() : base() { }

        private Category CreateModel(Category category)
        {
            category.catNum = int.Parse(reader["catNum"].ToString());
            category.catName =reader["catName"].ToString();
            return category;
        }

       
    }
}

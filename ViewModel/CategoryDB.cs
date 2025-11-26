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
        // Add new Category
        public int AddCategory(Category category)
        {
            string insertSql = string.Format("Insert into CategoryTbl "
                + "(catNum,catName)"
                + " values ({0},'{1}')"
                , category.catNum, category.catName);
            return base.ChangeTable(insertSql, "Database2026.accdb");

        }
        // Delete Category By CatNum
        public int DeleteCategoryByCatNum(int catNum)
        {
            string delSql = string.Format("Delete from CategoryTbl "
                + "where catNum= {0}", catNum);
            return base.ChangeTable(delSql, "Database2026.accdb");
        }
        // Update Category 
        public int UpdateCategory(Category category)
        {
            string updateSql = string.Format("Update CategoryTbl SET "
                + "catName='{0}'"  
                + " where catNum={1}", category.catName, category.catNum);
            return base.ChangeTable(updateSql, "Database2026.accdb");
        }

        // Select Categories
        private CategoryList SelectCategories(string sqlStr)
        {
            try
            {
                cmd = GenerateOleDBCommand(sqlStr, "Database2026.accdb");
                conObj.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    category = new Category();
                    categoriesList.Add(CreateModel(category));
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
            return categoriesList;
        }

        // Select All Categories
        public CategoryList SelectAllCategories()
        {
            string sqlStr = "Select * From CategoryTbl";
            CategoryList list = SelectCategories(sqlStr);
            return list;
        }


    }
}

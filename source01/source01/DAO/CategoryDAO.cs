using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using source01.EF;

namespace source01.DAO
{
    public class CategoryDAO
    {
        OnlineShopDb db = null;
        public CategoryDAO()
        {
            db  = new OnlineShopDb();
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
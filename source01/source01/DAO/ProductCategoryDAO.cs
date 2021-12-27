using source01.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class ProductCategoryDAO
    {
        OnlineShopDb db = null;
        public ProductCategoryDAO()
        {
            db = new OnlineShopDb();
        }
        public List<ProductCategory> GetCategory()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x=>x.DisplayOrder).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
using source01.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class ToursDAO
    {
        OnlineShopDb db = null;
        public ToursDAO()
        {
            db = new OnlineShopDb();
        }
        public List<Product> ListAll(int id)
        {
            return db.Products.Where(x => x.Status == true && x.Warranty == id).ToList();
        }
    }
}
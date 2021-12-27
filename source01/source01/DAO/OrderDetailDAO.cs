using source01.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class OrderDetailDAO
    {
        OnlineShopDb db = null;
        public OrderDetailDAO()
        {
            db = new OnlineShopDb();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }catch(Exception er)
            {
                return false;
            }
        }
    }
}
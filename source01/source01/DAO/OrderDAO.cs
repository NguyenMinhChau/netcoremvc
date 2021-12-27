using source01.EF;
using source01.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class OrderDAO
    {
        OnlineShopDb db = null;
        public OrderDAO()
        {
            db = new OnlineShopDb();
        }
        public long Insert(OrderProduct order)
        {
            db.OrderProducts.Add(order);
            db.SaveChanges();
            return order.ID;
        }
        public IEnumerable<RevunesVM> ListAll(DateTime? fromDate, DateTime? toDate)
        {
            var paramsmeter = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };
            return db.Database.SqlQuery<RevunesVM>("proc_ThongKe @fromDate,@toDate", paramsmeter);
        }
        public IEnumerable<RevenusSP> ThongKeSP(DateTime? fromDate, DateTime? toDate)
        {
            var paramsmeter = new SqlParameter[] {
                new SqlParameter("@fromDate", fromDate),
                new SqlParameter("@toDate", toDate)
            };
            return db.Database.SqlQuery<RevenusSP>("pro_ThongKeSP @fromDate,@toDate", paramsmeter);
        }
    }
}
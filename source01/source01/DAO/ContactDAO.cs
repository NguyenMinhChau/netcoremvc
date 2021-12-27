using source01.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class ContactDAO
    {
        OnlineShopDb db = null;
        public ContactDAO()
        {
            db = new OnlineShopDb();
        }
        public Contact GetActiveContext()
        {
            return db.Contacts.Single(x=>x.Status==true);
        }
        public int InsertFeedBack(Feedback fb)
        {
            db.Feedbacks.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }

    }

}
using source01.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class SlideDAO
    {
        OnlineShopDb db = null;
        public SlideDAO()
        {
            db = new OnlineShopDb();
        }
        public List<Slide> GetSlide()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
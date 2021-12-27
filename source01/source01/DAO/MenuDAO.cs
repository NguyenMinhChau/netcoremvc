using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using source01.EF;

namespace source01.DAO
{
    public class MenuDAO
    {
        OnlineShopDb db = null;
        public MenuDAO()
        {
            db = new OnlineShopDb();
        }
        public List<Menu> ListByGroupID(int groupID)
        {
            return db.Menus.Where(x => x.TypeID == groupID && x.Status ==true).OrderBy(x=>x.DisplayOrder).ToList();
        }
    }
}
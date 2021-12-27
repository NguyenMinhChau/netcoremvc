using source01.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class ContentDAO
    {
        OnlineShopDb db = null;
        public ContentDAO()
        {
            db = new OnlineShopDb();
        }
        public long Insert(Content entity)
        {
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(Content entity)
        {
            try
            {
                var user = db.Contents.Find(entity.ID);
                user.Name = entity.Name;
                user.MetaTitle = entity.MetaTitle;
                user.Description = entity.Description;
                user.Image = entity.Image;
                user.CategoryID = entity.CategoryID;
                user.Detail = entity.Detail;
                user.Warranty = entity.Warranty;
                user.CreatedDate = DateTime.Now;
                user.CreatedBy = entity.CreatedBy;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = entity.ModifiedBy;
                user.MetaDescriptions = entity.MetaDescriptions;
                user.Status = entity.Status;
                user.TopHot = entity.TopHot;
                db.SaveChanges();
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception er)
            {
                return false;
            }
        }
        public Content ViewDetail(int id)
        {
            return db.Contents.Find(id);
        }
        public IEnumerable<Content> ListAll()
        {
            return db.Contents;
        }
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }
    }
}
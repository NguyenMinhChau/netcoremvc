using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace source01.DAO
{
    public class SalesProduct
    {
        public bool SellProduct(long productId, int quantity)
        {
            var product = new ProductDAO().GetByID(productId);
            if (product.Quantity < quantity)
            {
                return false;
            }
            product.Quantity -= quantity;
            return true;
        }
    }
}
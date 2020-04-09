using Scrypt;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Data
{
    public class PuchaseUpdates
    {
        protected DataContext dbcontext;
        public PuchaseUpdates(DataContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public string AddNewPurchase(string productid, string userid, DateTime createddate)
        {
            PurchaseDetails newpurchase = new PurchaseDetails()
            {
                Id = Guid.NewGuid().ToString(),
                ActivationCode = Guid.NewGuid().ToString(),
                ProductId = productid,
                UserId = userid,
                CreatedDate = createddate
            };

            return newpurchase.ActivationCode;
        }

        public List<PurchaseDetails> ViewPastTransaction(string userid)
        {
            return dbcontext.purchaseDetails.Where(x => x.UserId == userid).ToList();
        }

        
    }
}

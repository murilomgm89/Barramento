using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class Product
    {
        public static Entity.Product GetCatalogProduct(int idPriceGroup)
        {
            using (var context = new Entity.OiWeb())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var query = from _P in context.Products
                            join _PP in context.PlanProducts on _P.idProduct equals _PP.idProduct
                            join _PF in context.ProductFamilies on _P.idProductFamily equals _PF.idProductFamily                            
                            join _PGS in context.PlanGroups on _PP.idPlan equals _PGS.idPlan
                            join _Pc in context.Prices on _PP.idPlan equals _Pc.idPlan
                            where
                                _Pc.idPriceLoyalty == 1 && _Pc.idTypeClient == 1 && _Pc.idPaymentMethod == 1 && _Pc.idPriceGroup == idPriceGroup
                                && _PP.isVisible == true && _PGS.idPriceGroup == idPriceGroup
                            select _P;
               
                query = query.Include(a => a.PlanProducts.Select(b => b.Prices));
                query = query.Include(a => a.ProductFamily);
                return query.FirstOrDefault();
            }
        }

        public static Entity.Product GetProduct(int idProduct)
        {
            using (var context = new Entity.OiWeb())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var query = from _P in context.Products
                            join _PP in context.PlanProducts on _P.idProduct equals _PP.idProduct
                            join _PF in context.ProductFamilies on _P.idProductFamily equals _PF.idProductFamily
                            join _PGS in context.PlanGroups on _PP.idPlan equals _PGS.idPlan                            
                            where
                                _P.idProduct == idProduct
                            select _P;
                query = query.Include(a => a.PriceGroups);
                query = query.Include(a => a.PlanProducts);
                query = query.Include(a => a.PlanProducts.Select(p => p.PlanGroups));               
                query = query.Include(a => a.ProductFamily);
                return query.FirstOrDefault();
            }
        }
    }
}

using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class CustomData
    {

        public static Entity.CustomData GetCustomData(int id_Data)
        {           
            using (var context = new OiWebDB())
            {
                return context.CustomDatas.Where(cw => cw.idData == id_Data).FirstOrDefault();
            }
        }

        public static IEnumerable<Entity.CustomData> GetCustomDataPage(string namePage,int idVersion ,int idGroup)
        {
            //Get CustomData Page ( List Componentes da Pagina )
            using (var context = new OiWebDB())
            {
                 var query = from _C in context.CustomDatas
                             join _CP in context.ComponentPages on _C.idComponentType equals _CP.idComponentType
                             join _P in context.Pages on _CP.idPage equals _P.idPage
                            where
                                _P.name == namePage &&
                                _C.idGroup == idGroup &&
                                _C.idVersion == idVersion
                             select _C;
                query =  query.Include(cds => cds.ComponentType);
                return query.ToList();
            }
        }
    }
}

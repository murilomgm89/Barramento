using OiWeb.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;

namespace OiWeb.Business
{
    public class GroupModal
    {
        public static List<Entity.GroupModalPage> GetGroupModalPage(int idPage, int idCity)
        {
            using (var context = new OiWebDB())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var query = from _P in context.GroupModalPages
                            join _PP in context.GroupModals on _P.idGroupModal equals _PP.idGroupModal
                            join _PF in context.GroupModalCities on _P.idGroupModal equals _PF.idGroupModal
                            join _PG in context.Modals on _P.idModal equals _PG.idModal
                            where
                                _P.idPage == idPage && _PF.idCity == idCity
                            select _P; 
                query = query.Include(a => a.Page);
                query = query.Include(a => a.GroupModal);
                query = query.Include(a => a.Modal);
                return query.ToList();
            }
        }
    }
}

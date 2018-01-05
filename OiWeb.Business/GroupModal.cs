using System;
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
            using (var context = new Entity.OiWeb())
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

        public static List<Entity.Modal> GetAll()
        {
            using (var context = new Entity.OiWeb())
            {
                return context.Modals.ToList();
            }
        }

        public static List<Entity.GroupModal> GetAllGroupModal()
        {
            using (var context = new Entity.OiWeb())
            {
                return context.GroupModals.ToList();
            }
        }

        public static void Insert(Entity.Modal entity)
        {
            entity.dtCreate = DateTime.Now;

            using (var context = new Entity.OiWeb())
            {
                context.Modals.Add(entity);
                context.SaveChanges();
            }
        }

        public static void InsertGroupModal(Entity.GroupModal entity)
        {
            entity.dtCreate = DateTime.Now;

            using (var context = new Entity.OiWeb())
            {
                context.GroupModals.Add(entity);
                context.SaveChanges();
            }
        }

        public static Entity.Modal GetById(int id)
        {
            using (var context = new Entity.OiWeb())
            {
                context.Configuration.ProxyCreationEnabled = false;
                return context
                    .Modals
                    .Include(t=> t.GroupModalPages)
                    .Include(t => t.GroupModalPages.Select(s=> s.Page))
                    .Include(t => t.GroupModalPages.Select(s => s.GroupModal))
                    .FirstOrDefault(w =>w.idModal == id);
            }
        }

        public static Entity.GroupModal GetByIdGroupModal(int id)
        {
            using (var context = new Entity.OiWeb())
            {

                context.Configuration.ProxyCreationEnabled = false;
                return context.GroupModals
                    .Include(t => t.GroupModalPages)
                    .Include(t => t.GroupModalCities)
                    .Include(t => t.GroupModalPages.Select(s => s.Page))
                    .Include(t => t.GroupModalPages.Select(s => s.GroupModal))
                    .FirstOrDefault(w => w.idGroupModal == id);
            }
        }

        public static void UpdateModal(Entity.Modal data)
        {
            using (var context = new Entity.OiWeb())
            {

                var modal = context.Modals.Find(data.idModal);

                if (modal != null)
                {

                    modal.name = data.name;
                    modal.description = data.description;

                    context.Modals.Attach(modal);
                    var entry = context.Entry(modal);
                    entry.Property(e => e.name).IsModified = true;
                    entry.Property(e => e.description).IsModified = true;

                    context.SaveChanges();
                }


            }
        }
        public static void UpdateGroupModal(Entity.GroupModal data)
        {
            using (var context = new Entity.OiWeb())
            {

                var groupModal = context.GroupModals.Find(data.idGroupModal);

                if (groupModal != null)
                {

                    groupModal.name = data.name;
                    groupModal.description = data.description;

                    context.GroupModals.Attach(groupModal);
                    var entry = context.Entry(groupModal);
                    entry.Property(e => e.name).IsModified = true;
                    entry.Property(e => e.description).IsModified = true;

                    context.SaveChanges();
                }


            }
        }

        public static void DesvincularPage(int idModal, int idPage)
        {
            using (var context = new Entity.OiWeb())
            {

                var dataGroupModalPages = context.GroupModalPages.Where(p => p.idModal == idModal && p.idPage == idPage).ToList();
                if (dataGroupModalPages != null)
                {
                    context.GroupModalPages.RemoveRange(dataGroupModalPages);
                    context.SaveChanges();
                }
            }
        }

        public static void DesvincularGroup(int idModal, int idGroupModal)
        {
            using (var context = new Entity.OiWeb())
            {

                var dataGroupModalPages = context.GroupModalPages.Where(p => p.idModal == idModal && p.idGroupModal == idGroupModal).ToList();
                if (dataGroupModalPages != null)
                {
                    context.GroupModalPages.RemoveRange(dataGroupModalPages);
                    context.SaveChanges();
                }
            }
        }

        public static void ExcludeModal(int idModal)
        {
            using (var context = new Entity.OiWeb())
            {

                var dataGroupModalPages = context.GroupModalPages.Where(p => p.idModal == idModal).ToList();
                if (dataGroupModalPages != null)
                {
                    context.GroupModalPages.RemoveRange(dataGroupModalPages);
                    context.SaveChanges();
                }

                var dataModal = context.Modals.Find(idModal);
                if (dataModal != null)
                {
                    context.Modals.Remove(dataModal);
                    context.SaveChanges();
                }
            }
        }

        public static void ExcludeGroupModal(int idGroupModal)
        {
            using (var context = new Entity.OiWeb())
            {
                var dataGroupModalPages = context.GroupModalPages.Where(p => p.idGroupModal == idGroupModal).ToList();
                if (dataGroupModalPages != null)
                {
                    context.GroupModalPages.RemoveRange(dataGroupModalPages);
                    context.SaveChanges();                    
                }

                var dataGroupModalCities = context.GroupModalCities.Where(p => p.idGroupModal == idGroupModal).ToList();
                if (dataGroupModalCities != null)
                {
                    context.GroupModalCities.RemoveRange(dataGroupModalCities);
                    context.SaveChanges();                    
                }
                
                var data = context.GroupModals.Find(idGroupModal);
                if (data != null)
                {
                    context.GroupModals.Remove(data);
                    context.SaveChanges();
                }
            }
        }
    }
}

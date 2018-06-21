using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace Repositories
{
    public class GroupRepository:IDisposable
    {
        private readonly WebContext _context = new WebContext();

        public IQueryable<Group> GetAll()
        {
            try
            {
                return _context.Groups;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public Group GetById(int id)
        {
            try
            {
                return _context.Groups.FirstOrDefault(s => s.GroupId == id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<Group> GetBySemester(int semesterId)
        {
            try
            {
                return _context.Groups.Where(i => i.Class.SemesterId == semesterId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public bool Add(Group group)
        {
            try
            {
                var old = group.Members.ToList();
                group.Members.Clear();
                foreach (var mb in old)
                {
                    group.Members.Add(_context.Students.FirstOrDefault(s=>s.StudentId.Equals(mb.StudentId)));
                }
                   
                //_context.Entry(group).State = EntityState.Added;
                _context.Groups.Add(group);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(Group group)
        {
            try
            {
                _context.Entry(group).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(Group group)
        {
            var curr = GetById(group.GroupId);
            if (curr == null)
                return false;

            try
            {
                _context.Groups.Remove(curr);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

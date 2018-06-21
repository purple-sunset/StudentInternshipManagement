using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Utilities;

namespace Repositories
{
    public class LearningClassStudentRepository : IDisposable
    {
        private readonly WebContext _context=new WebContext();

        public IQueryable<LearningClassStudent> GetAll()
        {
            try
            {
                return _context.LearningClassStudents;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public LearningClassStudent GetById(int classId, string studentId)
        {
            try
            {
                return _context.LearningClassStudents.FirstOrDefault(s => (s.ClassId == classId && s.StudentId.Equals(studentId)));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<LearningClassStudent> GetByClass(int classId)
        {
            try
            {
                return _context.LearningClassStudents.Where(s => s.ClassId == classId);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }

        public IQueryable<LearningClassStudent> GetByStudent(string studentId)
        {
            try
            {
                return _context.LearningClassStudents.Where(s => s.StudentId.Equals(studentId));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public IQueryable<LearningClassStudent> GetByTeacher(string teacherId)
        {
            try
            {
                var groups = _context.Groups.Where(g => g.TeacherId.Equals(teacherId));
                var students = groups.SelectMany(g => g.Members).Select(s => s.StudentId);
                return GetAll().Where(l => students.Contains(l.StudentId));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return null;
            }
        }
        public bool Add(LearningClassStudent learningClassStudent)
        {
            try
            {
                _context.LearningClassStudents.Add(learningClassStudent);
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Update(LearningClassStudent learningClassStudent)
        {
            try
            {
                _context.Entry(learningClassStudent).State = EntityState.Modified;
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }

        public bool Delete(LearningClassStudent learningClassStudent)
        {
            var curr = GetById(learningClassStudent.ClassId, learningClassStudent.StudentId);
            if (curr == null)
                return false;

            try
            {
                _context.LearningClassStudents.Remove(curr);
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

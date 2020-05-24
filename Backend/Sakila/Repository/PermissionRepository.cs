using Sakila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Repository
{
    public interface IPermissionRepository
    {
        List<Guid> ListUserIdByCourse(Guid courseId);
        List<Guid> ListCourseIdByUser(Guid userId);
        CoursePermission GetCoursePermission(Guid userId, Guid courseId);
        bool CreateCoursePermission(Guid userId, Guid courseId, bool writePermission, bool authorizePermission, bool join);
        bool DeleteCoursePermission(CoursePermission permission);
        bool UpdateCoursePermission(CoursePermission permission);
        int GetTotalCourseJoinPermission(Guid courseId);
    }

    public class PermissionRepository : IPermissionRepository
    {

        private SakilaContext DbContext;

        public PermissionRepository(SakilaContext sakilaContext)
        {
            DbContext = sakilaContext;
        }

        public List<Guid> ListUserIdByCourse(Guid courseId)
        {
            return DbContext.CoursePermission.Where(x => x.CourseId == courseId).Select(x => x.UserId).ToList();
        }

        public List<Guid> ListCourseIdByUser(Guid userId)
        {
            return DbContext.CoursePermission.Where(x => x.UserId == userId).Select(x => x.CourseId).ToList();
        }

        public CoursePermission GetCoursePermission(Guid userId, Guid courseId)
        {
            return DbContext.CoursePermission.Where(x => x.UserId == userId && x.CourseId == courseId).FirstOrDefault();
        }

        public bool CreateCoursePermission(Guid userId, Guid courseId, bool writePermission, bool authorizePermission, bool join)
        {
            try
            {
                DbContext.CoursePermission.Add(new CoursePermission 
                { 
                    UserId = userId, 
                    CourseId = courseId, 
                    Write = writePermission, 
                    Authorize = authorizePermission, 
                    Join = join 
                });
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteCoursePermission(CoursePermission permission)
        {
            try
            {
                DbContext.CoursePermission.Remove(permission);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCoursePermission(CoursePermission permission)
        {
            try
            {
                DbContext.CoursePermission.Update(permission);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetTotalCourseJoinPermission(Guid courseId)
        {
            return DbContext.CoursePermission.Where(x => x.Join && x.CourseId == courseId).Count();
        }
    }
}

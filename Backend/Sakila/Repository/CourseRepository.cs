using Sakila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Repository
{
    public interface ICourseRepository
    {
        List<Course> ListOnlineCourses();
        List<Course> ListJoinedCourses(Guid userId);
        List<Course> ListOwnedCourses(Guid userId);
        Course Get(Guid courseId);
        bool Create(Course course);
        bool Delete(Course course);
        bool Update(Course course);
    }

    public class CourseRepository : ICourseRepository
    {
        private SakilaContext DbContext;
        
        public CourseRepository(SakilaContext sakilaContext)
        {
            DbContext = sakilaContext;
        }

        public List<Course> ListOnlineCourses()
        {
            return DbContext.Course.Where(x => !x.Private).ToList();
        }

        public List<Course> ListJoinedCourses(Guid userId)
        {
            var permission = DbContext.CoursePermission.Where(x => x.UserId == userId && x.Join).Select(x => x.CourseId);
            return DbContext.Course.Where(x => !x.Private && permission.Contains(x.Id)).ToList();
        }

        public List<Course> ListOwnedCourses(Guid userId)
        {
            var permission = DbContext.CoursePermission.Where(x => x.UserId == userId && x.Write).Select(x => x.CourseId);
            return DbContext.Course.Where(x => permission.Contains(x.Id)).ToList();
        }

        public Course Get(Guid courseId)
        {
            return DbContext.Course.Where(x => x.Id == courseId).FirstOrDefault();
        }

        public bool Create(Course course)
        {
            try
            {
                DbContext.Course.Add(course);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Course course)
        {
            try
            {
                DbContext.Course.Remove(course);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Course course)
        {
            try
            {
                DbContext.Course.Update(course);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

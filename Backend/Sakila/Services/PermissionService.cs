using Sakila.Models;
using Sakila.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Services
{
    public interface IPermissionService
    {
        CoursePermission GetCoursePermission(Guid userId, Guid courseId);
        bool JoinCourse(Guid userId, Guid courseId);
        bool LeaveCourse(Guid userId, Guid courseId);
        int GetTotalJoinPermissions(Guid courseId);
        bool CreateCustomCoursePermission(Guid userId, Guid courseId, bool writePermission, bool authorizePermission, bool joinPermission);
    }

    public class PermissionService : IPermissionService
    {

        private IPermissionRepository permissionRepository;
        private IChapterRepository chapterRepository;

        public PermissionService(IPermissionRepository permissionRepository, IChapterRepository chapterRepository)
        {
            this.permissionRepository = permissionRepository;
            this.chapterRepository = chapterRepository;
        }

        public CoursePermission GetCoursePermission(Guid userId, Guid courseId)
        {
            return permissionRepository.GetCoursePermission(userId, courseId);
        }

        public bool JoinCourse(Guid userId, Guid courseId)
        {
            CoursePermission permission = GetCoursePermission(userId, courseId);
            if (permission == null)
            {
                return permissionRepository.CreateCoursePermission(userId, courseId, false, false, true);
            } 
            else
            {
                permission.Join = true;
                return permissionRepository.UpdateCoursePermission(permission);
            }
        }

        public bool CreateCustomCoursePermission(Guid userId, Guid courseId, bool writePermission, bool authorizePermission, bool joinPermission)
        {
            if (GetCoursePermission(userId, courseId) != null)
            {
                return false;
            }
            return permissionRepository.CreateCoursePermission(userId, courseId, writePermission, authorizePermission, joinPermission);
        }

        public bool LeaveCourse(Guid userId, Guid courseId)
        {
            CoursePermission permission = GetCoursePermission(userId, courseId);
            if (permission == null)
            {
                return true;
            }
            if (!permission.Write && !permission.Authorize)
            {
                return permissionRepository.DeleteCoursePermission(permission) && chapterRepository.DeleteProgress(userId, courseId);
            }
            else
            {
                permission.Join = false;
                return permissionRepository.UpdateCoursePermission(permission) && chapterRepository.DeleteProgress(userId, courseId);
            }
        }

        public int GetTotalJoinPermissions(Guid courseId)
        {
            return permissionRepository.GetTotalCourseJoinPermission(courseId);
        }
    }
}

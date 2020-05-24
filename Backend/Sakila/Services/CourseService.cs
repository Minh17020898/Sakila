using Sakila.Controllers.ChapterController;
using Sakila.Controllers.CourseController;
using Sakila.Models;
using Sakila.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Services
{
    public interface ICourseService
    {
        List<OnlineCourseDTO> ListOnlineCourses();
        List<JoinedCourseDTO> ListJoinedCourses(Guid userId);
        List<OnlineCourseDTO> ListOwnedCourses(Guid userId);
        CourseDetailDTO GetOnlineCourseDetail(Guid userId, Guid courseId);
        string CreateCourse(Guid userId, OnlineCourseDTO newCourse);
        EditCourseDetailDTO GetEditCourseDetail(Guid userId, Guid courseId);
        bool DeleteCourse(Guid userId, Guid courseId);
        bool UpdateCourse(Guid userId, EditCourseDetailDTO courseDetailDTO);
    }

    public class CourseService : ICourseService
    {
        private ICourseRepository courseRepository;
        private IUserRepository userRepository;

        private IPermissionService permissionService;
        private IChapterService chapterService;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository, IPermissionService permissionService, IChapterService chapterService)
        {
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
            this.permissionService = permissionService;
            this.chapterService = chapterService;
        }

        public List<OnlineCourseDTO> ListOnlineCourses()
        {
            return courseRepository.ListOnlineCourses().Select(x => new OnlineCourseDTO { Id = x.Id, Name = x.Name, ImageIndex = x.ImageIndex, Description = x.Description }).ToList();
        }

        public List<JoinedCourseDTO> ListJoinedCourses(Guid userId)
        {
            List<JoinedCourseDTO> response = courseRepository.ListJoinedCourses(userId).Select(x => new JoinedCourseDTO 
            { 
                Id = x.Id, 
                Name = x.Name, 
                ImageIndex = x.ImageIndex, 
                Description = x.Description, 
                CurrentScore = 0, 
                TotalScore = 0 
            }).ToList();
            response.ForEach(x =>
            {
                List<ChapterInProgressDTO> chapters = chapterService.ListChaptersInProgress(userId, x.Id);
                int current = 0;
                int total = 0;
                chapters.ForEach(y =>
                {
                    current += y.CurrentScore;
                    total += y.TotalScore;
                });
                x.CurrentScore = current;
                x.TotalScore = total;
            });
            return response;
        }

        public List<OnlineCourseDTO> ListOwnedCourses(Guid userId)
        {
            return courseRepository.ListOwnedCourses(userId).Select(x => new OnlineCourseDTO { Id = x.Id, ImageIndex = x.ImageIndex, Name = x.Name, Description = x.Description }).ToList();
        }

        public CourseDetailDTO GetOnlineCourseDetail(Guid userId, Guid courseId)
        {
            Course course = courseRepository.Get(courseId);
            CoursePermission permission = permissionService.GetCoursePermission(userId, courseId);
            return new CourseDetailDTO
            {
                Name = course.Name,
                Description = course.Description,
                Owner = userRepository.Get(course.OwnerId).Username,
                DateCreated = course.DateCreated,
                Participants = permissionService.GetTotalJoinPermissions(course.Id),
                IsJoined = permission == null ? false : permission.Join,
                ImageIndex = course.ImageIndex
            };
        }

        public EditCourseDetailDTO GetEditCourseDetail(Guid userId, Guid courseId)
        {
            Course course = courseRepository.Get(courseId);
            if (course == null)
            {
                return null;
            }
            return new EditCourseDetailDTO
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Private = course.Private,
                ImageIndex = course.ImageIndex
            };
        }

        public string CreateCourse(Guid userId, OnlineCourseDTO newCourse)
        {
            Course course = new Course
            {
                Id = Guid.NewGuid(),
                OwnerId = userId,
                Name = newCourse.Name,
                Description = newCourse.Description,
                DateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                ImageIndex = newCourse.ImageIndex,
                Private = true
            };
            string response = "";
            if (courseRepository.Create(course))
            {
                response = course.Id.ToString();
                permissionService.CreateCustomCoursePermission(userId, course.Id, true, true, false);
            }
            return response;
        }

        public bool DeleteCourse(Guid userId, Guid courseId)
        {
            Course course = courseRepository.Get(courseId);
            return courseRepository.Delete(course);
        }

        public bool UpdateCourse(Guid userId, EditCourseDetailDTO courseDetailDTO)
        {
            Course course = courseRepository.Get(courseDetailDTO.Id);
            if (course == null)
            {
                return false;
            }
            course.Name = courseDetailDTO.Name;
            course.Description = courseDetailDTO.Description;
            course.Private = courseDetailDTO.Private;
            course.ImageIndex = courseDetailDTO.ImageIndex;
            return courseRepository.Update(course);
        }
    }
}

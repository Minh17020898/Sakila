using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakila.Models;
using Sakila.Services;

namespace Sakila.Controllers.CourseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private IChapterService chapterService;
        private ICourseService courseService;
        private IPermissionService permissionService;

        public CourseController(IChapterService chapterService, ICourseService courseService, IPermissionService permissionService)
        {
            this.chapterService = chapterService;
            this.courseService = courseService;
            this.permissionService = permissionService;
        }

        [HttpGet, Route("list/online")] 
        public List<OnlineCourseDTO> ListOnlineCourses()
        {
            return courseService.ListOnlineCourses();
        }

        [HttpGet, Route("list/owned/{userId}")]
        public List<OnlineCourseDTO> ListOwnedCourses(Guid userId)
        {
            return courseService.ListOwnedCourses(userId);
        }

        [HttpGet, Route("list/joined/{userId}")]
        public List<JoinedCourseDTO> ListJoinedCourses(Guid userId)
        {
            return courseService.ListJoinedCourses(userId);
        }

        [HttpPost, Route("join/{userId}")]
        public string JoinCourse(Guid userId, [FromBody] Guid courseId)
        {
            return permissionService.JoinCourse(userId, courseId) ? "Success" : "Fail";
        }

        [HttpPost, Route("leave/{userId}")]
        public string LeaveCourse(Guid userId, [FromBody] Guid courseId)
        {
            return permissionService.LeaveCourse(userId, courseId) ? "Success" : "Fail";
        }

        [HttpPost, Route("get/online/{userId}")]
        public CourseDetailDTO GetOnlineCourseDetail(Guid userId, [FromBody] Guid courseId)
        {
            return courseService.GetOnlineCourseDetail(userId, courseId);
        }

        [HttpPost, Route("get/edit/{userId}")]
        public EditCourseDetailDTO GetEditCourseDetail(Guid userId, [FromBody] Guid courseId)
        {
            return courseService.GetEditCourseDetail(userId, courseId);
        }

        [HttpPost, Route("create/{userId}")]
        public string CreateCourse(Guid userId, [FromBody] OnlineCourseDTO course) {
            string response = courseService.CreateCourse(userId, course);
            return response.Equals("") ? "Fail" : "Success:" + response;
        }

        [HttpPost, Route("delete/{userId}")]
        public string DeleteCourse(Guid userId, [FromBody] Guid courseId)
        {
            return courseService.DeleteCourse(userId, courseId) ? "Success" : "Fail";
        }

        [HttpPost, Route("update/{userId}")]
        public string UpdateCourse(Guid userId, [FromBody] EditCourseDetailDTO courseDetailDTO)
        {
            return courseService.UpdateCourse(userId, courseDetailDTO) ? "Success" : "Fail";
        }
    }
}
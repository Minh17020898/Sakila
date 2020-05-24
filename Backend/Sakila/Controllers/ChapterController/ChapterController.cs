using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Sakila.Services;

namespace Sakila.Controllers.ChapterController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private IChapterService chapterService;

        public ChapterController(IChapterService chapterService)
        {
            this.chapterService = chapterService;
        }

        [HttpPost, Route("list")]
        public List<ChapterDTO> ListChapters([FromBody] Guid courseId)
        {
            return chapterService.ListChapters(courseId);
        }

        [HttpPost, Route("list/{userId}")]
        public List<ChapterInProgressDTO> ListChaptersInProgress(Guid userId, [FromBody] Guid courseId)
        {
            return chapterService.ListChaptersInProgress(userId, courseId);
        }

        [HttpPost, Route("create")]
        public string CreateChapter([FromBody] ChapterDTO chapter)
        {
            string response = chapterService.CreateChapter(chapter);
            return response.Equals("") ? "Fail" : "Success:" + response;
        }

        [HttpPost, Route("delete/{userId}")]
        public string DeleteChapter(Guid userId, [FromBody] Guid chapterId)
        {
            return chapterService.DeleteChapter(chapterId) ? "Success" : "Fail";
        }

        [HttpPost, Route("submit/{userId}")]
        public string SubmitProgress(Guid userId, [FromBody] ProgressDTO progress)
        {
            return chapterService.SubmitProgress(userId, progress) ? "Success" : "Fail";
        }

        [HttpPost, Route("update/{userId}")]
        public string UpdateChapter(Guid userId, [FromBody] ChapterDTO chapter)
        {
            return chapterService.UpdateChapter(userId, chapter) ? "Success" : "Fail";
        }
    }
}
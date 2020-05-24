using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sakila.Services;

namespace Sakila.Controllers.QuestionController
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionService questionService;

        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        [HttpPost, Route("list/{userId}")]
        public List<QuestionDTO> ListQuestions(Guid userId, [FromBody] Guid chapterId)
        {
            return questionService.ListQuestions(chapterId);
        }

        [HttpPost, Route("update/{userId}")]
        public string UpdateQuestion(Guid userId, [FromBody] QuestionDTO question)
        {
            return questionService.UpdateQuestion(question) ? "Success" : "Fail";
        }

        [HttpPost, Route("create/{userId}")]
        public string CreateQuestion(Guid userId, [FromBody] QuestionDTO question)
        {
            string responce = questionService.CreateQuestion(question);
            return responce.Equals("") ? "Fail" : "Success:" + responce;
        }
        
        [HttpPost, Route("delete/{userId}")]
        public string DeleteQuestion(Guid userId, [FromBody] Guid questionId)
        {
            return questionService.DeleteQuestion(questionId) ? "Success" : "Fail";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sakila.Controllers.QuestionController;
using Sakila.Models;
using Sakila.Repository;

namespace Sakila.Services
{
    public interface IQuestionService
    {
        List<QuestionDTO> ListQuestions(Guid chapterId);
        bool UpdateQuestion(QuestionDTO question);
        string CreateQuestion(QuestionDTO question);
        bool DeleteQuestion(Guid questionId);
    }

    public class QuestionService : IQuestionService
    {
        private IQuestionRepository questionRepository;

        public QuestionService(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        public string CreateQuestion(QuestionDTO question)
        {
            Question newQuestion = new Question
            {
                Id = Guid.NewGuid(),
                Content1 = question.Content1,
                Content2 = question.Content2,
                ChapterId = question.Id,
                Index = questionRepository.CurrentIndex(question.Id) + 1
            };
            return questionRepository.Create(newQuestion) ? newQuestion.Id.ToString() : "";
        }

        public bool DeleteQuestion(Guid questionId)
        {
            Question question = questionRepository.Get(questionId);
            if (question == null)
            {
                return true;
            }
            return questionRepository.Delete(question);
        }

        public List<QuestionDTO> ListQuestions(Guid chapterId)
        {
            List<QuestionDTO> response = new List<QuestionDTO>();
            List<Question> questions = questionRepository.ListByChapterId(chapterId);
            questions.ForEach(x =>
            {
                response.Add(new QuestionDTO
                {
                    Id = x.Id,
                    Content1 = x.Content1,
                    Content2 = x.Content2
                });
            });
            return response;
        }

        public bool UpdateQuestion(QuestionDTO question)
        {
            Question ques = questionRepository.Get(question.Id);
            if (ques == null)
            {
                return false;
            }
            ques.Content1 = question.Content1;
            ques.Content2 = question.Content2;
            return questionRepository.Update(ques);
        }
    }
}

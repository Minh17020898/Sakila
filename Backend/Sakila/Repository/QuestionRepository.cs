using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sakila.Controllers.QuestionController;
using Sakila.Models;

namespace Sakila.Repository
{
    public interface IQuestionRepository
    {
        bool Create(Question newQuestion);
        bool Delete(Question question);
        List<Question> ListByChapterId(Guid chapterId);
        Question Get(Guid id);
        bool Update(Question ques);
        int CurrentIndex(Guid chapterId);
    }

    public class QuestionRepository : IQuestionRepository
    {
        private SakilaContext DbContext;

        public QuestionRepository(SakilaContext sakilaContext)
        {
            DbContext = sakilaContext;
        }

        public bool Create(Question newQuestion)
        {
            try
            {
                DbContext.Question.Add(newQuestion);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(Question question)
        {
            try
            {
                DbContext.Question.Remove(question);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Question Get(Guid id)
        {
            return DbContext.Question.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<Question> ListByChapterId(Guid chapterId)
        {
            return DbContext.Question.Where(x => x.ChapterId == chapterId).OrderBy(x => x.Index).ToList();
        }

        public bool Update(Question question)
        {
            try
            {
                DbContext.Question.Update(question);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int CurrentIndex(Guid chapterId)
        {
            var query = DbContext.Question.Where(x => x.ChapterId == chapterId).Select(x => x.Index);
            return query.Count() == 0 ? 0 : query.Max();
        }
    }
}

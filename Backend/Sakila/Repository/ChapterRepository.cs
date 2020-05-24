using Sakila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Services
{
    public interface IChapterRepository
    {
        List<Chapter> ListChaptersByCourseId(Guid courseId);
        List<ChapterProgress> ListChapterProgresses(Guid userId, List<Chapter> chapters);
        ChapterProgress GetChapterProgress(Guid userId, Guid chapterId);
        bool CreateProgress(ChapterProgress progress);
        bool UpdateProgress(ChapterProgress progress);
        int GetChapterScore(Guid chapterId);
        bool CreateChapter(Chapter chapter);
        bool DeleteChapter(Guid chapterId);
        int CurrentIndex(Guid courseId);
        bool UpdateChapter(Chapter c);
        Chapter Get(Guid id);
        bool DeleteProgress(Guid userId, Guid courseId);
    }

    public class ChapterRepository : IChapterRepository
    {
        private SakilaContext DbContext;

        public ChapterRepository(SakilaContext sakilaContext)
        {
            DbContext = sakilaContext;
        }

        public List<Chapter> ListChaptersByCourseId(Guid courseId)
        {
            return DbContext.Chapter.Where(x => x.CourseId == courseId).OrderBy(x => x.Index).ToList();
        }

        public List<ChapterProgress> ListChapterProgresses(Guid userId, List<Chapter> chapters)
        {
            List<Guid> chapterIds = chapters.Select(x => x.Id).ToList();
            return DbContext.ChapterProgress.Where(x => x.UserId == userId && chapterIds.Contains(x.ChapterId)).ToList();
        }

        public int GetChapterScore(Guid chapterId)
        {
            return DbContext.Question.Where(x => x.ChapterId == chapterId).Count() * 2;
        }

        public bool CreateChapter(Chapter chapter)
        {
            try
            {
                DbContext.Chapter.Add(chapter);
                DbContext.SaveChanges();
                return true;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteChapter(Guid chapterId)
        {
            try
            {
                Chapter chapter = DbContext.Chapter.Where(x => x.Id == chapterId).FirstOrDefault();
                if (chapter == null)
                {
                    return true;
                }
                DbContext.Chapter.Remove(chapter);
                DbContext.SaveChanges();
                return true;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        public int CurrentIndex(Guid courseId)
        {
            var query = DbContext.Chapter.Where(x => x.CourseId == courseId).Select(x => x.Index);
            return query.Count() == 0 ? 0 : query.Max();
        }

        public ChapterProgress GetChapterProgress(Guid userId, Guid chapterId)
        {
            return DbContext.ChapterProgress.Where(x => x.UserId == userId && x.ChapterId == chapterId).FirstOrDefault();
        }

        public bool CreateProgress(ChapterProgress progress)
        {
            try
            {
                DbContext.ChapterProgress.Add(progress);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateProgress(ChapterProgress progress)
        {
            try
            {
                DbContext.ChapterProgress.Update(progress);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateChapter(Chapter c)
        {
            try
            {
                DbContext.Chapter.Update(c);
                DbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Chapter Get(Guid id)
        {
            return DbContext.Chapter.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool DeleteProgress(Guid userId, Guid courseId)
        {
            var chapterId = DbContext.Chapter.Where(x => x.CourseId == courseId).Select(x => x.Id);
            var chapterProgress = DbContext.ChapterProgress.Where(x => x.UserId == userId && chapterId.Contains(x.ChapterId));
            try
            {
                DbContext.ChapterProgress.RemoveRange(chapterProgress);
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

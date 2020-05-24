using Sakila.Controllers.ChapterController;
using Sakila.Models;
using Sakila.Repository;
using System;
using System.Collections.Generic;

namespace Sakila.Services
{
    public interface IChapterService
    {
        List<ChapterInProgressDTO> ListChaptersInProgress(Guid userId, Guid courseId);
        List<ChapterDTO> ListChapters(Guid courseId);
        string CreateChapter(ChapterDTO chapter);
        bool DeleteChapter(Guid chapterId);
        bool SubmitProgress(Guid userId, ProgressDTO progress);
        bool UpdateChapter(Guid userId, ChapterDTO chapter);
    }

    public class ChapterService : IChapterService
    {
        private IChapterRepository chapterRepository;

        public ChapterService(IChapterRepository chapterRepository)
        {
            this.chapterRepository = chapterRepository;
        }

        public string CreateChapter(ChapterDTO chapter)
        {
            if (chapter.Id == null)
            {
                return "";
            }
            Chapter newChapter = new Chapter
            {
                Id = Guid.NewGuid(),
                CourseId = chapter.Id,
                Index = chapterRepository.CurrentIndex(chapter.Id) + 1,
                Name = chapter.Name
            };
            return chapterRepository.CreateChapter(newChapter) ? newChapter.Id.ToString() : "";
        }

        public bool DeleteChapter(Guid chapterId)
        {
            return chapterRepository.DeleteChapter(chapterId);
        }

        public List<ChapterDTO> ListChapters(Guid courseId)
        {
            List<ChapterDTO> result = new List<ChapterDTO>();
            List<Chapter> chapters = chapterRepository.ListChaptersByCourseId(courseId);
            foreach (Chapter chapter in chapters)
            {
                result.Add(new ChapterDTO
                {
                    Id = chapter.Id,
                    Name = chapter.Name
                });
            }
            return result;
        }

        public List<ChapterInProgressDTO> ListChaptersInProgress(Guid userId, Guid courseId)
        {
            List<Chapter> chapters = chapterRepository.ListChaptersByCourseId(courseId);
            List<ChapterProgress> chapterProgresses = chapterRepository.ListChapterProgresses(userId, chapters);
            List<ChapterInProgressDTO> result = new List<ChapterInProgressDTO>();
            for (int i = 0; i < chapters.Count; i++)
            {
                ChapterProgress progress = chapterProgresses.Find(x => x.ChapterId == chapters[i].Id);
                int total = chapterRepository.GetChapterScore(chapters[i].Id);
                result.Add(new ChapterInProgressDTO
                {
                    Id = chapters[i].Id,
                    Name = chapters[i].Name,
                    CurrentScore = progress == null ? 0 : Math.Min(progress.Score, total),
                    TotalScore = total
                });          
            };
            return result;
        }

        public bool SubmitProgress(Guid userId, ProgressDTO progressDTO)
        {
            ChapterProgress progress = chapterRepository.GetChapterProgress(userId, progressDTO.ChapterId);
            if (progress == null)
            {
                return chapterRepository.CreateProgress(new ChapterProgress
                {
                    UserId = userId,
                    ChapterId = progressDTO.ChapterId,
                    Score = progressDTO.Score
                });
            }
            else
            {
                progress.Score = Math.Max(progress.Score, progressDTO.Score);
                return chapterRepository.UpdateProgress(progress);
            }
        }

        public bool UpdateChapter(Guid userId, ChapterDTO chapter)
        {
            Chapter c = chapterRepository.Get(chapter.Id);
            if (c == null)
            {
                return false;
            }
            c.Name = chapter.Name;
            return chapterRepository.UpdateChapter(c);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Repositories.TeacherArticleRepository;
using DataAccess.Context.EntityFramework;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.TeacherArticleRepository
{
    public class EfTeacherArticleDal : EfEntityRepositoryBase<TeacherArticle, SimpleContextDb>, ITeacherArticleDal
    {
        public async Task<TeacherArticleDto> GetDto(int Id)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherArticle in context.TeacherArticles
                             join teacher in context.Teachers on teacherArticle.TeacherId equals teacher.Id
                             join user in context.Users on teacher.UserId equals user.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             join article in context.Articles on teacherArticle.ArticleId equals article.Id
                             where teacherArticle.Id == Id
                             select new TeacherArticleDto
                             {
                                 Id = teacherArticle.Id,
                                 TeacherId = teacherArticle.TeacherId,
                                 ArticleId = teacherArticle.ArticleId,
                                 Title = article.Title,
                                 Description = article.Description,
                                 FileUrl = article.FileUrl,
                                 Date = article.Date,
                             };
                return await result.FirstOrDefaultAsync();
            }
        }

        public async Task<List<TeacherArticleListDto>> GetListDto()
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherArticle in context.TeacherArticles
                             join teacher in context.Teachers on teacherArticle.TeacherId equals teacher.Id
                             join user in context.Users on teacher.UserId equals user.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             join article in context.Articles on teacherArticle.ArticleId equals article.Id
                             select new TeacherArticleListDto
                             {
                                 Id = teacherArticle.Id,
                                 TeacherId = teacherArticle.TeacherId,
                                 ArticleId = teacherArticle.ArticleId,
                                 Title = article.Title,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Department = department.Name,
                                 Date = article.Date,
                             };
                return await result.ToListAsync();
            }
        }

        public async Task<List<TeacherArticleListDto>> GetListByUserId(int UserId)
        {
            using (var context = new SimpleContextDb())
            {
                var result = from teacherArticle in context.TeacherArticles
                             join teacher in context.Teachers on teacherArticle.TeacherId equals teacher.Id
                             join user in context.Users on teacher.UserId equals user.Id
                             join department in context.Departments on teacher.DepartmentId equals department.Id
                             join article in context.Articles on teacherArticle.ArticleId equals article.Id
                             where user.Id == UserId
                             select new TeacherArticleListDto
                             {
                                 Id = teacherArticle.Id,
                                 TeacherId = teacherArticle.TeacherId,
                                 ArticleId = teacherArticle.ArticleId,
                                 Title = article.Title,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Department = department.Name,
                                 Description = article.Description,
                                 Date = article.Date,
                             };
                return await result.ToListAsync();
            }
        }
    }
}
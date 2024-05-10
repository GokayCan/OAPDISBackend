using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TeacherArticleDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? File { get; set; }
        public string FileUrl { get; set; }
        public DateTime Date { get; set; }
    }
}
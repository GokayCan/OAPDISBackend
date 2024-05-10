using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }//başlık
        public string Description { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? FileUrl { get; set; }//birden fazla olabilir bu
    }
}
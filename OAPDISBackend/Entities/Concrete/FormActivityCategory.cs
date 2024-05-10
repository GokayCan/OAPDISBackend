using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class FormActivityCategory
    {
        public int Id { get; set; }
        public int FormActivityId { get; set; }
        public string Name { get; set; }
        public int FacultyPoint { get; set; }
        public int CriteriaPoint { get; set; }
        public int ActivityCount { get; set; }
    }
}
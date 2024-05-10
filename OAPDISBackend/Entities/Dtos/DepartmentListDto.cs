using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class DepartmentListDto : Department
    {
        public string FacultyName { get; set; }
        public string ProjectCount { get; set; }
    }
}
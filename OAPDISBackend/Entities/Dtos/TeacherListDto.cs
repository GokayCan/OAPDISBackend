using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class TeacherListDto : TeacherDto
    {
        public string DepartmentName { get; set; }
    }
}
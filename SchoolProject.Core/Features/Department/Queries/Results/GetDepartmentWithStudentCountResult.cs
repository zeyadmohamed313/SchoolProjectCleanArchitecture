using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Results
{
    public class GetDepartmentWithStudentCountResult
    {
        public string Name {  get; set; }
        public int StudentCount {  get; set; }
    }
}

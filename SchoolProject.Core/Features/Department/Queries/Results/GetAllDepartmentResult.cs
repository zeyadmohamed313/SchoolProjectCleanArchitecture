using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Results
{
    public class GetAllDepartmentResult
    {
        public int DID {  get; set; }
        public string Name {  get; set; }
        public GetAllDepartmentResult(int dID, string name)
        {
            DID = dID;
            Name = name;
        }
    }
}

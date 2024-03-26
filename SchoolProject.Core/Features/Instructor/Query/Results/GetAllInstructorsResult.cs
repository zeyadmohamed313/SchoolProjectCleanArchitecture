using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructor.Query.Results
{
    public class GetAllInstructorsResult
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public string? Image { get; set; }
        public string? DepartmentName { get; set; }
        public  List<SubjectsForEachInstructor> subjectsForEachInstructors { get; set; }
    }
    public class SubjectsForEachInstructor
    {
      public string SubjectName {  get; set; }
    }

}

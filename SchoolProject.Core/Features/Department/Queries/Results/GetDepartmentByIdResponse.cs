using Microsoft.Identity.Client;
using SchoolProject.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Results
{
	public class GetDepartmentByIdResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ManagerName {  get; set; }
		public PaginatedResult<StudentResponse>? StudentListPaginated { get; set; }
		public List<SubjectResponse>? Subjects { get; set; }
		public List<InstructorResponse>? Instructors { get; set; }

	}
	public class StudentResponse
	{
	  public int Id { get; set; }
	  public string Name { get; set; }
        public StudentResponse(int Id, string Name)
        {
			this.Id = Id;
			this.Name= Name;
        }
    }
	public class SubjectResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public class InstructorResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}

using SchoolProject.Data.Entites;
using SchoolProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IInstructorServices
    {
        public Task<Instructor> GetInstructorById(int id);
        public Task<bool> IsInstructorExsists(int id);
        public Task<string> AddInstructor(Instructor instructor);
        public Task<bool> IsInstructorArExists(string name);
        public Task<bool> IsInstructorEnExists(string name);
        public IQueryable<Instructor> FilterDepartmentPaginatedQuerable(InstructorOrderingEnum orderingEnum, string Search);
        public Task<string> UpdateInstuctor(Instructor instructor);
        public Task DeleteInstructor(Instructor instructor);
        public  Task<List<Instructor>> GetAllInstructors();
    }
}

using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface IClassServices
    {
        public Task<string> AddClass(Class _class);
        public Task<string> RemoveClass(int Id);
        public Task<List<Class>>? GetClassListAsync();
        public Task<Class> GetClassByIdAsync(int id);
        public  Task<List<Class>>? GetClassesByStudentAsync(int id);
        public Task<List<Class>>? GetClassesByInstructorAsync(int id);
        public Task<string> AddStudentToClassAsync(int StudId,int ClassId);
        public Task<string> RemoveStudentFromClassAsync(int StudId, int ClassId);
        public Task<string> AddInstructorToClassAsync(int InstructorId, int ClassId);
        public Task<string> RemoveInstructorFromClassAsync(int InstructorId, int ClassId);
        public Task<string> GetClassAvailableSpacesAsync(int Id);
        public  Task<bool> IsClassExsists(string name);
        public Task<Class> GetByIdWithStudents(int Id);
        public Task<Class> GetByIdWithInstructors(int Id);




    }
}

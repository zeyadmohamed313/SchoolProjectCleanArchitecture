using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Abstracts
{
    public interface ISubjectServices
    {
        public Task<string> AddSubject(Subjects subjects);
        public Task<string> RemoveSubject(int Id);
        public Task<List<Subjects>>? GetSubjectListAsync();
        public Task<Subjects> GetSubjectByIdAsync(int id);
        public Task<List<Subjects>>? GetSubjectsByStudentAsync(int id);
        public Task<List<Subjects>>? GetSubjectsByInstructorAsync(int id);
        public Task<string> AddStudentToSubjectAsync(int StudId, int SubId);
        public Task<string> RemoveStudentFromSubjectAsync(int StudId, int SubId);
        public Task<string> AddInstructorToSubjectAsync(int InstructorId, int SubId);
        public Task<string> RemoveInstructorFromSubjectAsync(int InstructorId, int SubId);
        public Task<bool> IsSubjectArExsists(string name);
        public Task<bool> IsSubjectEnExsists(string name);

    }
}

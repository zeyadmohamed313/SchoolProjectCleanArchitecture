using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entites;
using SchoolProject.Infrustructure.Migrations;
using SchoolProject.Infrustructure.UnitOfwork;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Service.Implementations
{
    public class SubjectServices : ISubjectServices
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public SubjectServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #endregion
        #region HandleFunctions
        public  async Task<string> AddInstructorToSubjectAsync(int InstructorId, int SubId)
        {
            // GetTheInstructor
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(InstructorId);
            // Null Checking
            if (instructor == null) return "InstructorNotFound";
            // Get The Class 
            var Subject = await _unitOfWork.Subjects.GetByIdWithInstructors(SubId);
            if (Subject == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Subject.Ins_Subjects.Any(e => e.InsId == InstructorId);
            if (IsExsist) return "Already Exsists";
            // Add Instructor To Class
            var insub = new Ins_Subject()
            { InsId = instructor.InsId,SubId=Subject.SubID };
            Subject.Ins_Subjects.Add(insub);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<string> AddStudentToSubjectAsync(int StudId, int SubId)
        {
            // GetTheInstructor
            var Student = await _unitOfWork.Students.GetByIdAsync(StudId);
            // Null Checking
            if (Student == null) return "StudentNotFound";
            // Get The Class 
            var Subject = await _unitOfWork.Subjects.GetByIdWithStudents(SubId);
            if (Subject == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Subject.StudentsSubjects.Any(e => e.StudID == Student.StudID);
            if (IsExsist) return "Already Exsists";
            // Add Instructor To Class
            var StdSub = new StudentSubject()
            { StudID = Student.StudID, SubID = Subject.SubID };
            Subject.StudentsSubjects.Add(StdSub);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<string> AddSubject(Subjects subject)
        {
            await _unitOfWork.Subjects.AddAsync(subject);
            return "Success";
        }

        public async Task<Subjects> GetSubjectByIdAsync(int id)
        {
            return await _unitOfWork.Subjects.GetByIdAsync(id);
        }

        public async Task<List<Subjects>>? GetSubjectListAsync()
        {
            return await _unitOfWork.Subjects.GetTableAsTracking().ToListAsync();
        }

        public async Task<List<Subjects>>? GetSubjectsByInstructorAsync(int id)
        {
            // GetTheInstructor
            var InstructorSubjects = await _unitOfWork.InstructorSubjects.GetTableNoTracking().Include(e => e.Subject).Where(e => e.InsId == id).ToListAsync();
            var Subjects = InstructorSubjects.Select(e => e.Subject).ToList();
            // Null Checking
            if (Subjects == null) return null;
            return Subjects;
        }

        public async Task<List<Subjects>>? GetSubjectsByStudentAsync(int id)
        {
            // GetTheStudent
            var studentSubjects = await _unitOfWork.StudentSubjects.GetTableNoTracking().Include(e=>e.Subject).Where(e=>e.StudID==id).ToListAsync();
            var Subjects = studentSubjects.Select(e=>e.Subject).ToList();
            // Null Checking
            if (Subjects == null) return null;
            return Subjects;
        }

        public async Task<bool> IsSubjectArExsists(string name)
        {
            // Check if there is student with the same Arabic Name  
            var ClassCheck = _unitOfWork.Subjects.GetTableNoTracking()
                .Where(x => x.SubjectNameAr.Equals(name)).FirstOrDefault();
            if (ClassCheck == null) return false;
            return true;
        }
        public async Task<bool> IsSubjectEnExsists(string name)
        {
            // Check if there is student with the same Arabic Name  
            var ClassCheck = _unitOfWork.Subjects.GetTableNoTracking()
                .Where(x => x.SubjectNameEn.Equals(name)).FirstOrDefault();
            if (ClassCheck == null) return false;
            return true;
        }

        public async Task<string> RemoveInstructorFromSubjectAsync(int InstructorId, int SubId)
        {
            // GetTheInstructor
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(InstructorId);
            // Null Checking
            if (instructor == null) return "InstructorNotFound";
            // Get The Class 
            var Sub = await _unitOfWork.Subjects.GetByIdWithInstructors(SubId);
            if (Sub == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Sub.Ins_Subjects.Any(e => e.InsId == InstructorId);
            if (!IsExsist) return "AlreadyNotExsists";
            // Add Instructor To Class
            var insub = new Ins_Subject()
            { InsId = instructor.InsId, SubId = Sub.SubID };
            var todelete= await _unitOfWork.InstructorSubjects.GetInstructorSubject(insub.InsId,insub.SubId);
            await _unitOfWork.InstructorSubjects.DeleteAsync(todelete);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<string> RemoveStudentFromSubjectAsync(int StudId, int SubId)
        {
            // GetTheStudent
            var Student = await _unitOfWork.Students.GetByIdAsync(StudId);
            // Null Checking
            if (Student == null) return "StudentNotFound";
            // Get The Class 
            var Subject = await _unitOfWork.Subjects.GetByIdWithStudents(SubId);
            if (Subject == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Subject.StudentsSubjects.Any(e => e.StudID == Student.StudID);
            if (!IsExsist) return "AlreadyNotExsists";
            // Add Instructor To Class
            var StdSub = new StudentSubject()
            { StudID = Student.StudID, SubID = Subject.SubID };
            var todelete = await _unitOfWork.StudentSubjects.GetStudentSubject(StdSub.StudID, StdSub.SubID);
            await _unitOfWork.StudentSubjects.DeleteAsync(todelete);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<string> RemoveSubject(int Id)
        {
            var subject = await _unitOfWork.Subjects.GetByIdAsync(Id);
            if (subject == null)
                return "Null";
            await _unitOfWork.Subjects.DeleteAsync(subject);
            return "Success";
        }
        #endregion
    }

}

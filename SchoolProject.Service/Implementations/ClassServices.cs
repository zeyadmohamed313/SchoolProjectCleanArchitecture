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
    public class ClassServices : IClassServices
    {

        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        #endregion
        #region Constructor
        public ClassServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        #endregion
        #region HandleFunctions
        public async Task<bool> IsClassExsists(string name)
        {
            // Check if there is student with the same Arabic Name  
            var ClassCheck = _unitOfWork.Classes.GetTableNoTracking()
                .Where(x => x.Name.Equals(name)).FirstOrDefault();
            if (ClassCheck == null) return false;
            return true;
        }
        public async Task<string> AddClass(Class _class)
        {
            await _unitOfWork.Classes.AddAsync(_class);
            return "Success";
        }
        public async Task<string> AddInstructorToClassAsync(int InstructorId, int ClassId)
        {
            // GetTheInstructor
            var instructor=await _unitOfWork.Instructors.GetByIdAsync(InstructorId);
            // Null Checking
            if(instructor == null) return "InstructorNotFound";
            // Get The Class 
            var Class = await _unitOfWork.Classes.GetByIdWithInstructors(ClassId);
            if (Class == null)return "ClassNotFound";
            // Check If Exsists
            var IsExsist =  Class.Instructors.Any(e=>e.InsId== InstructorId);
            if (IsExsist) return "Already Exsists";
            // Add Instructor To Class
            Class.Instructors.Add(instructor);
            _unitOfWork.Complete();
            return "Success";

        }

        public async Task<string> AddStudentToClassAsync(int StudId, int ClassId)
        {
            // GetTheStudent
            var Student  = await _unitOfWork.Students.GetByIdAsync(StudId);
            // Null Checking
            if (Student == null) return "StudentNotFound";
            // Get The Class 
            var Class = await _unitOfWork.Classes.GetByIdWithStudents(ClassId);
            if (Class == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Class.Students.Any(e => e.StudID == StudId);
            if (IsExsist) return "Already Exsists";
            // Add Instructor To Class
            Class.Students.Add(Student);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<string> GetClassAvailableSpacesAsync(int Id)
        {
            // Get The Class 
           var Class =  await _unitOfWork.Classes.GetByIdAsync(Id);
            if (Class == null)
                return "Null";
            return Class.AvailablePlaces.ToString();
            
        }

        public async Task<Class> GetClassByIdAsync(int id)
        {
            var Class = await _unitOfWork.Classes.GetByIdAsync(id);
            return Class;
        }

        public async Task<List<Class>>? GetClassesByInstructorAsync(int id)
        {
            // GetTheInstructor
            var instructor = await _unitOfWork.Instructors.GetInstructorWithClassesAsync(id);
            // Null Checking
            if (instructor == null) return null;
            // Get The Class 
            var Classes = instructor.classes;
            return Classes;
        }

        public async Task<List<Class>>? GetClassesByStudentAsync(int id)
        {
            // GetTheInstructor
            var Student = await _unitOfWork.Students.GetStudentWithClasses(id);
            // Null Checking
            if (Student == null) return null;
            // Get The Class 
            var Classes = Student.EnrolledClasses;
            return Classes;
        }

        public async Task<List<Class>>? GetClassListAsync()
        {
            return await _unitOfWork.Classes.GetTableNoTracking().ToListAsync();
        }

        public async Task<string> RemoveClass(int Id)
        {
            var Class = await _unitOfWork.Classes.GetByIdAsync(Id);
            if (Class == null)
                return "Null";
            await _unitOfWork.Classes.DeleteAsync(Class);
            return "Success";
        }

        public async Task<string> RemoveInstructorFromClassAsync(int InstructorId, int ClassId)
        {
            // GetTheInstructor
            var instructor = await _unitOfWork.Instructors.GetByIdAsync(InstructorId);
            // Null Checking
            if (instructor == null) return "InstructorNotFound";
            // Get The Class 
            var Class = await _unitOfWork.Classes.GetByIdWithInstructors(ClassId);
            if (Class == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Class.Instructors.Any(e => e.InsId == InstructorId);
            if (!IsExsist) return "AlreadyNotExsists";
            // Add Instructor To Class
            Class.Instructors.Remove(instructor);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<string> RemoveStudentFromClassAsync(int StudId, int ClassId)
        {
            // GetTheStudent
            var Student = await _unitOfWork.Students.GetByIdAsync(StudId);
            // Null Checking
            if (Student == null) return "StudentNotFound";
            // Get The Class 
            var Class = await _unitOfWork.Classes.GetByIdWithStudents(ClassId);
            if (Class == null) return "ClassNotFound";
            // Check If Exsists
            var IsExsist = Class.Students.Any(e => e.StudID == StudId);
            if (!IsExsist) return "AlreadyNotExsists";
            // Add Instructor To Class
            Class.Students.Remove(Student);
            _unitOfWork.Complete();
            return "Success";
        }

        public async Task<Class> GetByIdWithStudents(int Id)
        {
            return await _unitOfWork.Classes.GetByIdWithStudents(Id);
        }

        public async Task<Class> GetByIdWithInstructors(int Id)
        {
            return await _unitOfWork.Classes.GetByIdWithInstructors(Id);
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.AppMetaData
{
	public class Router
	{
		public const string SignleRoute = "/{id}";
		public const string root = "Api";
		public const string version = "V1";
		public const string Rule = root + "/" + version + "/";
		public static class StudentRouting
		{
			public const string Prefix = Rule + "Student";
			public const string List = Prefix + "/List";
			public const string GetByID = Prefix + SignleRoute;
			public const string Create = Prefix + "/Create";
			public const string Edit = Prefix + "/Edit";
			public const string Delete = Prefix + "/Delete" + SignleRoute;
			public const string Paginated = Prefix + "/Paginated";


		}
		public static class ClassRouting
		{
			public const string Prefix = Rule + "Class";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + "/GetById";
            public const string GetAvailableSpaces = Prefix + "/GetAvailableSpaces";
			public const string GetClassesForStudent = Prefix + "/GetClassesForStudent";
            public const string GetClassesForInstructor = Prefix + "/GetClassesForInstructor";
			public const string AddClass = Prefix + "/AddClass";
            public const string DeleteClass = Prefix + "/DeleteClass";
			public const string AddStudentToClass = Prefix + "/AddStudentToClass";
            public const string RemoveStudentFromClass = Prefix + "/RemoveStudentFromClass";
            public const string AddInstructorToClass = Prefix + "/AddInstructorToClass";
            public const string RemoveInstructorFromClass = Prefix + "/RemoveInstructorFromClass";



        }
        public static class SubjectRouting
        {
            public const string Prefix = Rule + "Subject";
            public const string List = Prefix + "/List";
            public const string GetByID = Prefix + "/GetById";
            public const string GetAvailableSpaces = Prefix + "/GetAvailableSpaces";
            public const string GetSubjectsForStudent = Prefix + "/GetSubjectsForStudent";
            public const string GetSubjectsForInstructor = Prefix + "/GetSubjectsForInstructor";
            public const string AddSubject = Prefix + "/AddSubject";
            public const string DeleteSubject = Prefix + "/DeleteSubject";
            public const string AddSubjectToStudent = Prefix + "/AddSubjectToStuent";
            public const string RemoveSubjectFromStudent = Prefix + "/RemoveSubjectFromStudent";
            public const string AddSubjectToInstructor = Prefix + "/AddSubjectToInstructor";
            public const string RemoveSubjectFromInstructor = Prefix + "/RemoveSubjectFromInstructor";



        }
        public static class DepartmentRouting
		{
			public const string Prefix = Rule + "Department";
			public const string GetByID = Prefix + "/id";
            public const string Create = Prefix + "/Create";
			public const string GetAllDepartmentsPaginated = Prefix + "/GetAllDepartmentsPaginated";
			public const string Update = Prefix + "/Update";
			public const string Delete = Prefix + "/Delete";
			public const string GetDepartmentStudentsCount = Prefix + "/GetDepartmentStudentsCount";
			public const string GetDepartmentWithStudentsStoredProcedure = Prefix + "/GetDepartmentWithStudentsStoredProcedure";

        }
		public static class InstructorRouting 
		{
            public const string Prefix = Rule + "Instructor";
            public const string GetByID = Prefix + "/id";
			public const string AddInstructor = Prefix + "/AddInstructor";
			public const string GetAllInstructors = Prefix + "/GetAllInstructors";
			public const string UpdateInstructor = Prefix + "/UpdateInstructor";
			public const string DeleteInstructor = Prefix + "/DeleteInstructor";

        }
        public static class ApplicationUserRouting
		{
			public const string Prefix = Rule + "User";
			public const string Create = Prefix + "/Create";
			public const string Paginated = Prefix + "/Paginated";
			public const string GetByID = Prefix + SignleRoute;
			public const string Edit = Prefix + "/Edit";
			public const string Delete = Prefix + "/Delete" + SignleRoute;
			public const string ChangePassword = Prefix + "Change-Password";
		}
		public static class AuthenticationRouting
		{
			public const string Prefix = Rule + "Authentication";
			public const string SignIn = Prefix + "/SignIn";
			public const string RefreshToken = Prefix + "/RefreshToken";
			public const string ValidateToken = Prefix + "/ValidateToken";
            public const string ConfirmEmail = "/Api/Authentication/ConfirmEmail";
			public const string SendResetPasswordCode = Prefix + "/SendResetPasswordCode";
			public const string ConfirmResetPasswordCode = Prefix + "/ConfirmResetPasswordCode";
			public const string ResetPassword = Prefix + "/ResetPassword";

        }
        public static class AuthorizationRouting
		{
			public const string Prefix = Rule + "Authentication";
			public const string Create = Prefix + "/Role"+"/Create";
            public const string Edit = Prefix + "/Role" + "/Edit";
            public const string Delete = Prefix + "/Role" + "/Delete/{id}";
			public const string GetRoleList = Prefix + "/Role" + "/GetRoleList";
			public const string GetRoleById = Prefix + "/Role" + "/GetRoleById/{id}";
			public const string ManageUserRoles = Prefix + "/Role" + "/ManageUserRoles/{Id}";
            public const string UpdateUserRoles = Prefix + "/Role" + "/UpdateUserRoles";
			public const string ManageUserClaims = Prefix + "/Claim" + "/ManageUserClaims/{userId}";
			public const string UpdateUserClaims = Prefix + "/Claim" + "/UpdateUserClaims";




        }

        public static class EmailsRoute
        {
            public const string Prefix = Rule + "EmailsRoute";
            public const string SendEmail = Prefix + "/SendEmail";
        }
    }
}

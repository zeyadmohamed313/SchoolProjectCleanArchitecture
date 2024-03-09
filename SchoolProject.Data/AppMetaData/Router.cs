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
		public static class DepartmentRouting
		{
			public const string Prefix = Rule + "Department";
			public const string GetByID = Prefix + "/id";
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
            
        }
    }
}

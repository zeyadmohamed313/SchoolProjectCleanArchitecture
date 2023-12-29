﻿using System;
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

		}
	}
}

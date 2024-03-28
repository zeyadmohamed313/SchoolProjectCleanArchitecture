
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SchoolProject.Data.Entites;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Abstractions;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entites.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EntityFrameworkCore.EncryptColumn.Extension;
using SchoolProject.Data.Entites.Views;
using SchoolProject.Data.Results.StoredProceduresResult;

namespace SchoolProject.Infrustructure.Context
{
	public class ApplicationDbContext : IdentityDbContext< User, Role,int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>,IdentityUserToken<int>>
	{
        #region Fields
        private readonly IEncryptionProvider _encryptionProvider;
        #endregion
        public ApplicationDbContext()
		{

		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
            _encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-8QKV55J\\SQLEXPRESS;Initial Catalog=CollegeManagementSystem;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// configuring using fluent api
			//has the highest precedence over conventions and data annotations.
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.UseEncryption(_encryptionProvider);
            modelBuilder.Entity<GetDepartmentWithStudentsResult>(entity =>
            {
                entity.HasNoKey();
            }); // this is how i can use it in my stored procedure with out having a table for it

        }
		public DbSet<User> Users {  get; set; }
		public DbSet<UserRefreshToken> userRefreshToken {  get; set; }
		public DbSet<Student> Students {  get; set; }
		public DbSet<Department>Departments { get; set; }
		public DbSet<Subjects>Subjects { get; set; }
		public DbSet<DepartmetSubject> departmetSubjects { get; set; }
		public DbSet<StudentSubject> StudentSubjects { get; set; }
		public DbSet<Class> classes {  get; set; }

        #region Views
        public DbSet<ViewDepartment> ViewDepartment { get; set; }

        #endregion

    }
}

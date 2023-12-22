
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using SchoolProject.Data.Entites;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Abstractions;
using System.Reflection;

namespace SchoolProject.Infrustructure.Context
{
	public class ApplicationDbContext : DbContext
    {
		public ApplicationDbContext()
		{

		}
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=DESKTOP-8QKV55J\\SQLEXPRESS;Initial Catalog=SchoolMangment;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
			base.OnConfiguring(optionsBuilder);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// configuring using fluent api
			//has the highest precedence over conventions and data annotations.
			base.OnModelCreating(modelBuilder);
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}

		public DbSet<Student> Students {  get; set; }
		public DbSet<Department>Departments { get; set; }
		public DbSet<Subjects>Subjects { get; set; }
		public DbSet<DepartmetSubject> departmetSubjects { get; set; }
		public DbSet<StudentSubject> StudentSubjects { get; set; }


	}
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure.Configuration
{
	public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
	{
		public void Configure(EntityTypeBuilder<Department> builder)
		{
			/*builder.HasKey(d => d.DID);
			builder.Property(d => d.DNameAr).HasMaxLength(500);

			builder.HasMany(d => d.Students)
			.WithOne(s => s.Department)
			.HasForeignKey(d => d.DID)
			.OnDelete(DeleteBehavior.Restrict);

			 builder.HasOne(d => d.Instructor)
			.WithOne(ins => ins.DepartmentManger)
			.HasForeignKey<Department>(d => d.DID)
			.OnDelete(DeleteBehavior.Restrict);
			*/
			// not sure here
			
			   /* builder
				.HasOne(x => x.Instructor)
				.WithOne(x => x.DepartmentManger)
				.HasForeignKey<Department>(x => x.InsManger)
				.OnDelete(DeleteBehavior.Restrict);*/
		}
	}
}

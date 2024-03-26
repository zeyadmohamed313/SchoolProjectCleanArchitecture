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
	public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
	{
		public void Configure(EntityTypeBuilder<Instructor> builder)
		{
           /*builder
			.HasOne(x => x.Supervisor)
			 .WithMany(x => x.Instructors)
			  .HasForeignKey(x => x.SupervisorId)
				.OnDelete(DeleteBehavior.Restrict);*/
		}
	}
}

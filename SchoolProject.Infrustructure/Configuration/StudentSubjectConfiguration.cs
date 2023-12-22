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
	public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
	{
		public void Configure(EntityTypeBuilder<StudentSubject> builder)
		{
			    builder
				.HasKey(x => new { x.SubID, x.StudID });

			builder.HasOne(SS => SS.Student)
					.WithMany(S => S.StudentSubjects)
					.HasForeignKey(SS => SS.StudID);

			builder.HasOne(SS => SS.Subject)
			.WithMany(S => S.StudentsSubjects)
			.HasForeignKey(SS => SS.SubID);

		}
	}
}

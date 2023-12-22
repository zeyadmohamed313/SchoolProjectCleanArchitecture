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
	public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmetSubject>
	{
		public void Configure(EntityTypeBuilder<DepartmetSubject> builder)
		{
			builder.HasKey(x => new { x.SubID, x.DID });

			builder.HasOne(ds => ds.Department)
					.WithMany(d => d.DepartmentSubjects)
					.HasForeignKey(ds => ds.DID);

			builder.HasOne(ds => ds.Subject)
			.WithMany(s => s.DepartmetsSubjects)
			.HasForeignKey(ds => ds.SubID);
		}
	}
}

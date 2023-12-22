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
	public class Ins_SubjectConfiguration : IEntityTypeConfiguration<Ins_Subject>
	{
		public void Configure(EntityTypeBuilder<Ins_Subject> builder)
		{
			builder
		   .HasKey(x => new { x.SubId, x.InsId });

			builder.HasOne(IS => IS.instructor)
					.WithMany(d => d.Ins_Subjects)
					.HasForeignKey(IS => IS.InsId);

			builder.HasOne(IS => IS.Subject)
			.WithMany(s => s.Ins_Subjects)
			.HasForeignKey(IS => IS.SubId);
		}
	}
}

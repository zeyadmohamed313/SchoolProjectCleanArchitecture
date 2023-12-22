using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entites
{
	public class Ins_Subject
	{
		[Key]
		public int InsId { get; set; }
		[Key]
		public int SubId { get; set; }
		[ForeignKey(nameof(InsId))]
		[InverseProperty("Ins_Subjects")]
		public Instructor? instructor { get; set; }
		[ForeignKey(nameof(SubId))]
		[InverseProperty("Ins_Subjects")]
		public Subjects? Subject { get; set; }
	}
}

using SchoolProject.Data.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entites
{
	public class Subjects:GeneralLocalizableEntity
	{
		public Subjects()
		{
			StudentsSubjects = new HashSet<StudentSubject>();
			DepartmetsSubjects = new HashSet<DepartmetSubject>();
			Ins_Subjects = new HashSet<Ins_Subject>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SubID { get; set; }
		[StringLength(500)]
		public string? SubjectNameAr { get; set; }
		[StringLength(500)]
		public string? SubjectNameEn { get; set; }

		public int? Period { get; set; }
		[InverseProperty("Subject")]
		public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
		[InverseProperty("Subject")]
		public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
		[InverseProperty("Subject")]
		public virtual ICollection<Ins_Subject> Ins_Subjects {  get; set; } 
	}
}

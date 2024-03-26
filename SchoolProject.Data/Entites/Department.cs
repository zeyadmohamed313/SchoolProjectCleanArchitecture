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
	public partial class Department:GeneralLocalizableEntity
	{
		public Department()
		{
			Students = new HashSet<Student>();
			DepartmentSubjects = new HashSet<DepartmetSubject>();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int DID { get; set; }
		[StringLength(500)]
		public string? DNameAr { get; set; }
		[StringLength(500)]
		public string? DNameEn { get; set; }
		//public int? InsManger {  get; set; }
		[InverseProperty("Department")]
		public virtual ICollection<Student>? Students { get; set; }
		[InverseProperty("Department")]
		public virtual ICollection<DepartmetSubject>? DepartmentSubjects { get; set; }
		[InverseProperty("Department")]
		public virtual ICollection<Instructor>? Instructors { get; set; }
		[ForeignKey("InsManger")]
		[InverseProperty("DepartmentManger")]
		public virtual Instructor? Instructor { get; set; }

	}
}

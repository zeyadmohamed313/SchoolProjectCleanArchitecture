using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entites
{
	public class Student:GeneralLocalizableEntity
	{
		public Student()
		{
			StudentSubjects = new HashSet<StudentSubject>();
			EnrolledClasses = new List<Class>();
		}

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int StudID { get; set; }
		[StringLength(200)]
		public string? NameAr { get; set; }
		[StringLength(200)]
		public string? NameEn { get; set; }
		[StringLength(500)]
		public string? Address { get; set; }
		[StringLength(500)]
		public string? Phone { get; set; }
		public int? DID { get; set; }

		[ForeignKey("DID")]
		[InverseProperty(nameof(Department.Students))]
		public virtual Department? Department { get; set; }
		[InverseProperty("Student")]
		public virtual ICollection<StudentSubject> StudentSubjects {  get; set; }
		public List<Class> EnrolledClasses {  get; set; }
	}
}

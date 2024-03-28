using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Data.Entites
{
    public class Class
    {
        public Class()
        {
            Students = new List<Student>();
            Instructors = new List<Instructor>();
            AvailablePlaces = Get_The_Rest(); // this comes first
        }
        public int Id {  get; set; }
        public string Name { get; set; }
        public int Capacity {  get; set; }
        public int AvailablePlaces { get; private set; }
        public List<Student> Students { get; set; }
        public List<Instructor> Instructors { get; set; }
        private  int Get_The_Rest()=>  Capacity-(Students.Count + Instructors.Count);
    
    }
   
}

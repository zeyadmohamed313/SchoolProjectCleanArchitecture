using MediatR;
using SchoolProject.Core.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Command.Models
{
    public class AddSubjectCommand:IRequest<Response<string>>
    {
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }

        public int? Period { get; set; }
    }
}

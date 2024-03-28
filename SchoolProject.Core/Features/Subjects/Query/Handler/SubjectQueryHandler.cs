using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Query.Models;
using SchoolProject.Core.Features.Subjects.Query.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Query.Handler
{
    public class SubjectQueryHandler:ResponseHandler,
        IRequestHandler<GetAllSubjectsQuery, Response<List<GetAllSubjectsResult>>>,
        IRequestHandler<GetSubjectsByInstructorQuery, Response<List<GetSubjectByInstructorResult>>>,
        IRequestHandler<GetSubjectsByStudentQuery, Response<List<GetSubjectByStudentResult>>>,
        IRequestHandler<GetSubjectByIdQuery, Response<GetSubjectByIdResult>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizar;
        private readonly ISubjectServices _subjectServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public SubjectQueryHandler(IStringLocalizer<SharedResourses> localizer,
            ISubjectServices subjectServices, IMapper mapper) : base(localizer)
        {
            _localizar = localizer;
            _subjectServices = subjectServices;
            _mapper = mapper;
        }

        #endregion
        #region HandleFunctions 

        public async Task<Response<List<GetAllSubjectsResult>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
        {
            // Get List Of Subjects 
            var Subjects = await _subjectServices.GetSubjectListAsync();
            // Null Checking 
            if (Subjects == null)
                return BadRequest<List<GetAllSubjectsResult>>(_localizar[SharedResoursesKeys.NotFound]);
            // mapping
            var mapper = _mapper.Map < List<GetAllSubjectsResult>>(Subjects);
            return Success(mapper);
           
        }

        public async Task<Response<List<GetSubjectByInstructorResult>>> Handle(GetSubjectsByInstructorQuery request, CancellationToken cancellationToken)
        {
            // Get List Of Subjects 
            var Subjects = await _subjectServices.GetSubjectsByInstructorAsync(request.Id);
            // Null Checking 
            if (Subjects == null)
                return BadRequest< List < GetSubjectByInstructorResult >> (_localizar[SharedResoursesKeys.NotFound]);
            // mapping
            var mapper = _mapper.Map< List < GetSubjectByInstructorResult >> (Subjects);
            return Success(mapper);
        }

        public async Task<Response<List<GetSubjectByStudentResult>>> Handle(GetSubjectsByStudentQuery request, CancellationToken cancellationToken)
        {
            // Get List Of Subjects 
            var Subjects = await _subjectServices.GetSubjectsByStudentAsync(request.Id);
            // Null Checking 
            if (Subjects == null)
                return BadRequest<List<GetSubjectByStudentResult>>(_localizar[SharedResoursesKeys.NotFound]);
            // mapping
            var mapper = _mapper.Map< List < GetSubjectByStudentResult >> (Subjects);
            return Success(mapper);
        }

        public async Task<Response<GetSubjectByIdResult>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            // Get Subject By ID
            var Subject = await _subjectServices.GetSubjectByIdAsync(request.Id);
            if (Subject == null)
                return BadRequest<GetSubjectByIdResult>(_localizar[SharedResoursesKeys.NotFound]);
            // Mapping
            var mapper = _mapper.Map<GetSubjectByIdResult>(Subject);

            return Success(mapper);
        }


        #endregion
    }
}

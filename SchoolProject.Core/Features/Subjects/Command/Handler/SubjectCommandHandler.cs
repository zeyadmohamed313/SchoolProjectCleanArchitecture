using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Command.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Subjects.Command.Handler
{
    public class SubjectCommandHandler:ResponseHandler,
        IRequestHandler<AddSubjectCommand, Response<string>>,
        IRequestHandler<RemoveSubjectCommand, Response<string>>,
        IRequestHandler<AddSubjectToStudentCommand, Response<string>>,
        IRequestHandler<AddSubjectToInstructorCommand, Response<string>>,
        IRequestHandler<RemoveSubjectFromInstructorCommand, Response<string>>,
        IRequestHandler<RemoveSubjectFromStudentCommand, Response<string>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizar;
        private readonly ISubjectServices _subjectServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public SubjectCommandHandler(IStringLocalizer<SharedResourses> localizer,
            ISubjectServices subjectServices, IMapper mapper) : base(localizer)
        {
            _localizar = localizer;
            _subjectServices = subjectServices;
            _mapper = mapper;
        }
        #endregion

        #region HandleFunctions
        public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
        {
            // Add Subject 
            var mapper = _mapper.Map<Data.Entites.Subjects>(request);
            await _subjectServices.AddSubject(mapper);
            return Success<string>(_localizar[SharedResoursesKeys.Success]);
        }

        public async Task<Response<string>> Handle(RemoveSubjectCommand request, CancellationToken cancellationToken)
        {
            var Deleted = await _subjectServices.RemoveSubject(request.Id);
            if (Deleted == "Success")
                return Success<string>(_localizar[SharedResoursesKeys.Deleted]);
            return BadRequest<string>(_localizar[SharedResoursesKeys.NotFound]);
        }

        public async Task<Response<string>> Handle(AddSubjectToStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _subjectServices.AddStudentToSubjectAsync(request.StudentId, request.SubjectId);
            switch (result)
            {
                case "StudentNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.ClassNotFound]);
                case "Already Exsists":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.AlreadyExsists]);
                default:
                    return Success<string>(_localizar[SharedResoursesKeys.Success]);

            }
        }

        public async Task<Response<string>> Handle(AddSubjectToInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _subjectServices.AddInstructorToSubjectAsync(request.InstructorId, request.SubjectId);
            switch (result)
            {
                case "StudentNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.ClassNotFound]);
                case "Already Exsists":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.AlreadyExsists]);
                default:
                    return Success<string>(_localizar[SharedResoursesKeys.Success]);

            }
        }

        public async Task<Response<string>> Handle(RemoveSubjectFromInstructorCommand request, CancellationToken cancellationToken)
        {
            var result = await _subjectServices.RemoveInstructorFromSubjectAsync(request.InstructorId, request.SubjectId);
            switch (result)
            {
                case "StudentNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.ClassNotFound]);
                case "AlreadyNotExsists":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.AlreadyNotExsists]);
                default:
                    return Success<string>(_localizar[SharedResoursesKeys.Success]);

            }
        }

        public async Task<Response<string>> Handle(RemoveSubjectFromStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _subjectServices.RemoveStudentFromSubjectAsync(request.StudentId, request.SubjectId);
            switch (result)
            {
                case "StudentNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.ClassNotFound]);
                case "AlreadyNotExsists":
                    return BadRequest<string>(_localizar[SharedResoursesKeys.AlreadyNotExsists]);
                default:
                    return Success<string>(_localizar[SharedResoursesKeys.Success]);

            }
        }
        #endregion
    }
}

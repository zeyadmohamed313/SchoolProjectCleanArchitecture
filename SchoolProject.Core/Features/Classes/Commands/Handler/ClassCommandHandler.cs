using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Classes.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Commands.Handler
{
    public class ClassCommandHandler:ResponseHandler,
        IRequestHandler<AddClassCommand, Response<string>>,
        IRequestHandler<RemoveClassCommand, Response<string>>,
        IRequestHandler<AddStudentToClassCommand, Response<string>>,
        IRequestHandler<RemoveStudentFromClassCommand, Response<string>>,
        IRequestHandler<AddInstructorToClassCommand, Response<string>>,
        IRequestHandler<RemoveInstructorFromClassCommand, Response<string>>

    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IClassServices _classServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public ClassCommandHandler(IStringLocalizer<SharedResourses> localizer,
            IClassServices classServices, IMapper mapper) : base(localizer)
        {
            _localizer = localizer;
            _classServices = classServices;
            _mapper = mapper;
        }
        #endregion
        #region HandleFunctions


        public async Task<Response<string>> Handle(AddClassCommand request, CancellationToken cancellationToken)
        {

            var mapper = _mapper.Map<Class>(request);
            await _classServices.AddClass(mapper);
            return Success<string>(_localizer[SharedResoursesKeys.Success]);
        }

        public async Task<Response<string>> Handle(RemoveClassCommand request, CancellationToken cancellationToken)
        {
            var Deleted = await _classServices.RemoveClass(request.Id);
            if(Deleted=="Success")
            return Success<string>(_localizer[SharedResoursesKeys.Deleted]);
            return BadRequest<string>(_localizer[SharedResoursesKeys.NotFound]);
        }

        public async Task<Response<string>> Handle(AddStudentToClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _classServices.AddStudentToClassAsync(request.StudId, request.ClassId);
            switch (result)
            {
                case "StudentNotFound":
                        return BadRequest<string>(_localizer[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.ClassNotFound]);
                case "Already Exsists":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.AlreadyExsists]);
                default:
                    return Success<string>(_localizer[SharedResoursesKeys.Success]);

            }
        }

        public async Task<Response<string>> Handle(RemoveStudentFromClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _classServices.RemoveStudentFromClassAsync(request.StudId, request.ClassId);
            switch (result)
            {
                case "StudentNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.ClassNotFound]);
                case "AlreadyNotExsists":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.AlreadyNotExsists]);
                default:
                    return Success<string>(_localizer[SharedResoursesKeys.Success]);

            }
        }

        public async Task<Response<string>> Handle(AddInstructorToClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _classServices.AddInstructorToClassAsync(request.InsId, request.ClassId);
            switch (result)
            {
                case "InstructorNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.InstructorNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.ClassNotFound]);
                case "Already Exsists":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.AlreadyExsists]);
                default:
                    return Success<string>(_localizer[SharedResoursesKeys.Success]);

            }
        }

        public async Task<Response<string>> Handle(RemoveInstructorFromClassCommand request, CancellationToken cancellationToken)
        {
            var result = await _classServices.RemoveInstructorFromClassAsync(request.InsId, request.ClassId);
            switch (result)
            {
                case "InstructorNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.StudentNotFound]);
                case "ClassNotFound":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.ClassNotFound]);
                case "AlreadyNotExsists":
                    return BadRequest<string>(_localizer[SharedResoursesKeys.AlreadyNotExsists]);
                default:
                    return Success<string>(_localizer[SharedResoursesKeys.Success]);

            }
        }
        #endregion
    }
}

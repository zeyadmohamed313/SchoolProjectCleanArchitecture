using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Instructor.Query.Models;
using SchoolProject.Core.Features.Instructor.Query.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructor.Query.Handler
{
    public class InstructorQueryHandler : ResponseHandler
        , IRequestHandler<GetInstructorByIdModel, Response<GetInstructorByIdResult>>,
        IRequestHandler<GetAllInstructorsModel,Response<List<GetAllInstructorsResult>>>
    {
        #region Properties
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IInstructorServices  _instructorServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public InstructorQueryHandler(IStringLocalizer<SharedResourses> localizer
            , IInstructorServices instructorServices, IMapper mapper):base(localizer)
        {
            _localizer = localizer;
            _instructorServices = instructorServices;
            _mapper = mapper;
        }
        #endregion
        #region Functions
        public async Task<Response<GetInstructorByIdResult>> Handle(GetInstructorByIdModel request, CancellationToken cancellationToken)
        {
            var Instructor = await _instructorServices.GetInstructorById(request.Id);
            if (Instructor == null)
                return BadRequest<GetInstructorByIdResult>(_localizer[SharedResoursesKeys.NotFound]);
            var mapper = _mapper.Map<GetInstructorByIdResult>(Instructor);
            return Success<GetInstructorByIdResult>(mapper);
        }

        public async Task<Response<List<GetAllInstructorsResult>>> Handle(GetAllInstructorsModel request, CancellationToken cancellationToken)
        {
            var instructors = await  _instructorServices.GetAllInstructors();
            if (instructors == null)
                return BadRequest<List<GetAllInstructorsResult>>(_localizer[SharedResoursesKeys.NotFound]);
            var InsMapper = _mapper.Map<List<GetAllInstructorsResult>>(instructors);
            return Success(InsMapper);
           
        }


        #endregion

    }
}

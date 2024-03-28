using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Classes.Query.Models;
using SchoolProject.Core.Features.Classes.Query.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Classes.Query.Handler
{
    public class ClassQueryHandler:ResponseHandler,
        IRequestHandler<GetAllClassesQuery, Response<List<GetAllClassesResult>>>,
        IRequestHandler<GetClassAvailableSpaceQuery, Response<GetClassAvailableSpaceResult>>,
        IRequestHandler<GetClassByIdQuery, Response<GetClassByIdResult>>,
        IRequestHandler<GetClassesByInsturctorQuery, Response<List<GetClassByResult>>>,
        IRequestHandler<GetClassesByStudentQuery, Response<List<GetClassByResult>>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizar;
        private readonly IClassServices _classServices;
        private readonly IMapper _mapper;
        #endregion
        #region Constructor
        public ClassQueryHandler(IStringLocalizer<SharedResourses> localizer,
            IClassServices classServices, IMapper mapper):base(localizer)
        {
            _localizar = localizer;
            _classServices = classServices;
            _mapper = mapper;
        }


        #endregion
        #region HandleFunctions
        public async Task<Response<List<GetAllClassesResult>>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
        {
            // Get All Class From Services
            var Classes = await _classServices.GetClassListAsync();
            // Null Checking
            if (Classes == null)
                return BadRequest<List<GetAllClassesResult>>(_localizar[SharedResoursesKeys.NotFound]);
            // Mapping
            var mapper = _mapper.Map<List<GetAllClassesResult>>(Classes);
            return Success(mapper);

        }

        public async Task<Response<GetClassAvailableSpaceResult>> Handle(GetClassAvailableSpaceQuery request, CancellationToken cancellationToken)
        {
            // Get Class By ID
            var Class = await _classServices.GetClassByIdAsync(request.Id);
            // Null Checking 
            if(Class == null)
                return BadRequest<GetClassAvailableSpaceResult>(_localizar[SharedResoursesKeys.NotFound]);
            // Mapping
            var mapper = _mapper.Map<GetClassAvailableSpaceResult>(Class);
            return Success(mapper);
        }

        public async Task<Response<List<GetClassByResult>>> Handle(GetClassesByInsturctorQuery request, CancellationToken cancellationToken)
        {
            // Get Class By Instructor ID
            var InsClasses = await _classServices.GetClassesByInstructorAsync(request.Id);
            // Null Checking 
            if(InsClasses == null)
                return BadRequest<List<GetClassByResult>>(_localizar[SharedResoursesKeys.NotFound]);
            // Mapping 
            var mapper = _mapper.Map<List<GetClassByResult>>(InsClasses);
            return Success(mapper);


        }

        public async Task<Response<List<GetClassByResult>>> Handle(GetClassesByStudentQuery request, CancellationToken cancellationToken)
        {
            // Get Class By Student ID
            var StdClasses = await _classServices.GetClassesByStudentAsync(request.Id);
            // Null Checking 
            if (StdClasses == null)
                return BadRequest<List<GetClassByResult>>(_localizar[SharedResoursesKeys.NotFound]);
            // Mapping 
            var mapper = _mapper.Map<List<GetClassByResult>>(StdClasses);
            return Success(mapper);
        }

        public async Task<Response<GetClassByIdResult>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            // Get Class By Its Id
            var Class = await _classServices.GetClassByIdAsync(request.Id);
            // Null Checking
            if(Class == null)
                return BadRequest<GetClassByIdResult>(_localizar[SharedResoursesKeys.NotFound]);
            // Mapping
            var mapper = _mapper.Map<GetClassByIdResult>(Class);
            return Success(mapper);

        }



        #endregion
    }
}

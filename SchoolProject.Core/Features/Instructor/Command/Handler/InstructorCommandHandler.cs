using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructor.Command.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Instructor.Command.Handler
{
    public class InstructorCommandHandler:ResponseHandler
        ,IRequestHandler<AddInstructorModel,Response<string>>
        ,IRequestHandler<UpdateInstructorCommandModel,Response<string>>
        ,IRequestHandler<DeleteInstructorCommandModel,Response<string>>
        
    {
        #region Feilds 
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IMapper _mapper;
        private readonly IInstructorServices _instructorServices;
        #endregion

        #region Constructor
        public InstructorCommandHandler(IStringLocalizer<SharedResourses>localizer,
            IMapper mapper, IInstructorServices instructorServices):base(localizer)
        {
            _localizer = localizer;
            _mapper = mapper;
            _instructorServices = instructorServices;
        }


        #endregion

        #region HandleFunctions
        public async Task<Response<string>> Handle(AddInstructorModel request, CancellationToken cancellationToken)
        {
            // Mapping Between Request and Student
            var InstructorMapper = _mapper.Map<Data.Entites.Instructor>(request);
            // add 
            var result = await _instructorServices.AddInstructor(InstructorMapper);
            if (result == "Added SuccessFully") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateInstructorCommandModel request, CancellationToken cancellationToken)
        {
            // Notice i have used the same mapping for add and update because they are the same
            var instructor = await _instructorServices.GetInstructorById(request.InsId);
            if(instructor == null)
                return BadRequest<string>(_localizer[SharedResoursesKeys.UpdateFailed]);

            //var InstructorMapper = _mapper.Map<Data.Entites.Instructor>(request);
            var InstructorMapper = _mapper.Map(request, instructor);

            var result = await _instructorServices.UpdateInstuctor(InstructorMapper);
            if (result == "Success") return Success<string>(_localizer[SharedResoursesKeys.Update]);
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteInstructorCommandModel request, CancellationToken cancellationToken)
        {
            var instructor = await _instructorServices.GetInstructorById(request.Id);
            if (instructor == null) return BadRequest<string>(_localizer[SharedResoursesKeys.NotFound]);
            await _instructorServices.DeleteInstructor(instructor);
            return Success<string>(_localizer[SharedResoursesKeys.Success]);
        }

        #endregion
    }
}

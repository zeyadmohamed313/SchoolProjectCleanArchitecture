using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Commands.Handler
{
    public class DepartmentCommandHandler : ResponseHandler,
        IRequestHandler<CreateDepartmentCommand, Response<string>>,
        IRequestHandler<UpdateDepartmentCommand,Response<string>>,
        IRequestHandler<DeleteDepartmentCommand,Response<string>>
    {

        #region Fields
        private readonly IStringLocalizer<SharedResourses> _localizer;
        private readonly IMapper _mapper;
        private readonly IDepartmentServices _departmentServices;
        #endregion
        public DepartmentCommandHandler(IStringLocalizer<SharedResourses> localizer
            , IMapper mapper , IDepartmentServices departmentServices):base(localizer) 
        {
            _localizer = localizer;
            _mapper = mapper;
            _departmentServices = departmentServices;
        }
        #region Constructor
        #endregion
        #region Functions
        public async Task<Response<string>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Mapping Between Request and Student
            var DepartmentMapper = _mapper.Map<Data.Entites.Department>(request);
            // add 
            var result = await _departmentServices.CreateDepartment(DepartmentMapper);
            if (result == "Added SuccessFully") return Created("");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentServices.GetDepartmentById(request.DID);
            var depmapper = _mapper.Map<Data.Entites.Department>(request);
            var result = await _departmentServices.UpdateDepartment(depmapper);

            if (result == "Null")
                return BadRequest<string>(_localizer[SharedResoursesKeys.DepartmentNotExsists]);

            return Success<string>(_localizer[SharedResoursesKeys.Success]);
            
        }

        public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await _departmentServices.GetDepartmentById(request.DID);
            if(department == null)
                return BadRequest<string>(_localizer[SharedResoursesKeys.DepartmentNotExsists]);
            await _departmentServices.DeleteDepartment(department);
            return Success<string>(_localizer[SharedResoursesKeys.Success]);

        }


        #endregion
    }
}

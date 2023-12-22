using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Department.Queries.Handler
{
	public class DepartmentQueryHandler : ResponseHandler,
	IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResourses> _localizer;
		private readonly IDepartmentServices _departmentService;
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		#endregion
		#region Notes
		// request => Mediator => Response
		// notice that response class have localizer in its constructor , then you should pass it in the base here
		#endregion
		#region Constructor 
		public DepartmentQueryHandler(IStringLocalizer<SharedResourses> localizer,
			IDepartmentServices departmentServices , IMapper mapper , IStudentService studentService):base(localizer)
		{
			_localizer = localizer;
			_departmentService = departmentServices;
			_mapper = mapper;
			_studentService = studentService;
		}
		#endregion
		#region Handle Function
		public async Task<Response<GetDepartmentByIdResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
		{
			var response = await _departmentService.GetDepartmentById(request.Id);
			// Check if Null or not 
			if (response == null) return NotFound<GetDepartmentByIdResponse>(_localizer[SharedResoursesKeys.NotFound]);
			// Mapping
			var Mapper = _mapper.Map<GetDepartmentByIdResponse>(response);
			// pagination of the student list in side the department
			Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr,e.NameEn));
			var StudentQuery =  _studentService.GetAllStudentsByDepartmentIdQueryable(request.Id);
			var StudentsAfterPagination = await StudentQuery.Select(expression).ToPaginatedListAsync(request.StudentPageNumber,request.StudentPageSize);
			Mapper.StudentListPaginated =  StudentsAfterPagination;
			return Success(Mapper);
		}
		#endregion
	}
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Features.Students.Queries.Results;
using SchoolProject.Core.Resourses;
using SchoolProject.Core.Wrapper;
using SchoolProject.Data.Entites;
using SchoolProject.Data.Results.StoredProceduresResult;
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
	IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentByIdResponse>>,
	IRequestHandler<GetAllDepartmentsQuery,PaginatedResult<GetAllDepartmentResult>>,
    IRequestHandler<GetDepartmentsWithStudentCountModel,Response<List<GetDepartmentWithStudentCountResult>>>,
	IRequestHandler<GetDepartmentsWithStudentsStoredProcedure,Response<List<GetDepartmentWithStudentsResult>>>
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

        public async Task<PaginatedResult<GetAllDepartmentResult>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            // Mapping 
            Expression<Func<Data.Entites.Department, GetAllDepartmentResult>> Expression = e =>
            new GetAllDepartmentResult(e.DID, e.Localize(e.DNameAr, e.DNameEn));
            //var Queryable = _studentService.GetAllStudentsQueryable();
            var FilterQuery = _departmentService.FilterDepartmentPaginatedQuerable(request.OrderBy, request.Search);
            var PaginatedList = await FilterQuery.Select(Expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }

        public async Task<Response<List<GetDepartmentWithStudentCountResult>>> Handle(GetDepartmentsWithStudentCountModel request, CancellationToken cancellationToken)
        {
            var viewDepartmentResult = await _departmentService.GetViewDepartment();
            var result = _mapper.Map<List<GetDepartmentWithStudentCountResult>>(viewDepartmentResult);
            return Success(result);
        }

        public async Task<Response<List<GetDepartmentWithStudentsResult>>> Handle(GetDepartmentsWithStudentsStoredProcedure request, CancellationToken cancellationToken)
        {
			return Success(await _departmentService.GetDepartmentWithStudentsStoredProcedure());
        }
        #endregion
    }
}

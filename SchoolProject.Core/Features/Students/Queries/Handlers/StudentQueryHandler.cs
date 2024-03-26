using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Features.Students.Queries.Results;
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

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
	public class StudentQueryHandler :ResponseHandler,
		IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
		IRequestHandler<GetStudentByIDQuery,Response<GetSingleStudentResponse>>,
		IRequestHandler<GetStudentPaginatedListQuery,PaginatedResult<GetStudentPaginatedListResponse>>
	{
		#region Feilds
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResourses> _stringLocalizer;
		#endregion
		#region Constructor
		public StudentQueryHandler(IStudentService studentService,IMapper mapper, IStringLocalizer<SharedResourses> stringLocalizer )
			:base(stringLocalizer)
		{
			_studentService = studentService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region HandleFunctions
		public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
		{
			var studentList=  await _studentService.GetStudentListAsync();
			var StudentListAfterMapping = _mapper.Map<List<GetStudentListResponse>>(studentList);
			var Result =  Success(StudentListAfterMapping);
			// boxing of Anoumonus Type
			Result.Meta = new { Count = StudentListAfterMapping.Count() };
			return Result;
		}

		public async Task<Response<GetSingleStudentResponse>> Handle(GetStudentByIDQuery request, CancellationToken cancellationToken)
		{
			var student = await _studentService.GetByIdWithIncludeAsync(request.Id);
			if(student==null)
				return NotFound<GetSingleStudentResponse>(_stringLocalizer[SharedResoursesKeys.NotFound]);
			var result  =_mapper.Map<GetSingleStudentResponse>(student);
			// another shape of it
			return Success<GetSingleStudentResponse>(result);
		}

		public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
		{
			// Mapping 
			Expression<Func<Student, GetStudentPaginatedListResponse>> Expression = e =>
			new GetStudentPaginatedListResponse(e.StudID, e.Localize(e.NameAr,e.NameEn), e.Address, e.Department.Localize(e.Department.DNameAr,e.Department.DNameEn));
			//var Queryable = _studentService.GetAllStudentsQueryable();
			var FilterQuery = _studentService.FilterStudentPaginatedQuerable(request.OrderBy,request.Search);
			var PaginatedList = await FilterQuery.Select(Expression).ToPaginatedListAsync(request.PageNumber,request.PageSize);
			PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
			return PaginatedList;
		}

		#endregion
	}
}

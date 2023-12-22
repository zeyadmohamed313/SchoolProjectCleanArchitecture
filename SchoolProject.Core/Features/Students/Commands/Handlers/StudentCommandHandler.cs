using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resourses;
using SchoolProject.Data.Entites;
using SchoolProject.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
	public class StudentCommandHandler : ResponseHandler,
	IRequestHandler<AddStudentCommand, Response<string>>,
	IRequestHandler<EditStudentCommand, Response<string>>,
	IRequestHandler<DeleteStudentCommand, Response<string>>
	{
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResourses> _localizer;
		#endregion
		#region Constructor
		public StudentCommandHandler(IStudentService studentService , IMapper mapper , IStringLocalizer<SharedResourses> localizer )
			:base(localizer)
		{
			_studentService = studentService;
			_mapper = mapper;
			_localizer = localizer;
		}
		#endregion
		#region
		
		public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
		{
			// Mapping Between Request and Student
			var StudentMapper = _mapper.Map<Student>(request);
			// add 
			var result = await _studentService.AddStudentAsync(StudentMapper);
			if (result == "Added SuccessFully") return Created("");
			else return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
		{
			// Check if the student with this id exsists 
			var student =await _studentService.GetByIdAsync(request.Id);
			// Return not found if not
			if(student==null)return BadRequest<string>("Student is Not Found");
			// mapping 
			// This will Create New Instance With request attributes
			//var StudentMapper = _mapper.Map<Student>(request);
			// This will just Modify The Exists Object Student
			var StudentMapper = _mapper.Map(request,student);
			// call the service that makes Edit
			var result = await _studentService.EditAsync(StudentMapper);
			// return Response
			if (result == "Success") return Success($"Added Succesfully {StudentMapper.StudID}");
			else return BadRequest<string>();

		}

		public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
		{
			// Check if the student with this id exsists 
			var student = await _studentService.GetByIdAsync(request.Id);
			// Return not found if not
			if (student == null) return BadRequest<string>("Student is Not Found");
			// Call the Services
			var result = await _studentService.DeleteAsync(student);
			if (result == "Success") return Deleted<string>();
			else return BadRequest<string>();
		}
		#endregion
	}
}

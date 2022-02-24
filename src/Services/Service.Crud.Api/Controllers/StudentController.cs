using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Crud.Api.CQRS.StudentApp.Commands;
using Service.Crud.Api.CQRS.StudentApp.Queries;
using Service.Crud.Api.Domain.Entities;
using Service.Crud.Api.Infrastructure.Response;
using Service.Crud.Api.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Crud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add-student")]
        public async Task<ApiResponse<bool>> AddStudent([FromBody] AddStudentCommand addStudentCommand)
        {
            var result = await _mediator.Send(addStudentCommand);
            return result;
        }

        [HttpGet]
        [Route("get-all-students")]
        public async Task<ApiResponse<IEnumerable<Student>>> GetAllStudents()
        
        {
            var result = await _mediator.Send(new GetAllStudentsQuery());
            return result;
        }

        [HttpPost]
        [Route("update-student")]
        public async Task<ApiResponse<bool>> UpdateStudent([FromBody] UpdateStudentCommand updateStudentCommand)
        {
            var result = await _mediator.Send(updateStudentCommand);
            return result;
        }

        [HttpDelete]
        [Route("delete-student/{id}")]
        public async Task<ApiResponse<bool>> DeleteStudent(int id)
        {
            var result = await _mediator.Send(new DeleteStudentCommand { Id = id });
            return result;
        }
    }
}

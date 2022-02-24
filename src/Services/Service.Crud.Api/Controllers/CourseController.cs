using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Service.Crud.Api.CQRS.CourseApp.Commands;
using Service.Crud.Api.CQRS.CourseApp.Queries;
using Service.Crud.Api.CQRS.StudentApp.Commands;
using Service.Crud.Api.Infrastructure.Response;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Crud.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add-course")]
        public async Task<ApiResponse<bool>> AddCourse([FromBody] AddCourseCommand addCourseCommand)
        {
            var result = await _mediator.Send(addCourseCommand);
            return result;
        }

        [HttpGet]
        [Route("get-all-courses")]
        public async Task<ApiResponse<IEnumerable<Course>>> GetAllCourses()
        {
            var result = await _mediator.Send(new GetAllCoursesQuery());
            return result;
        }

        [HttpPost]
        [Route("udpate-course")]
        public async Task<ApiResponse<bool>> UpdateCourse([FromBody] UpdateCourseCommand updateCourseCommand)
        {
            var result = await _mediator.Send(updateCourseCommand);
            return result;
        }

        [HttpDelete]
        [Route("delete-course/{id}")]
        public async Task<ApiResponse<bool>> DeleteCourse(int id)
        {
            var result = await _mediator.Send(new DeleteCourseCommand { Id = id });
            return result;
        }
    }
}

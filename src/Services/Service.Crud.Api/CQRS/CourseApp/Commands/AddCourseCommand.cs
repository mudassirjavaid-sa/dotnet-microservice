using MediatR;
using Service.Crud.Api.Infrastructure.Response;
using Service.Crud.Api.Persistence;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Crud.Api.CQRS.CourseApp.Commands
{
    public class AddCourseCommand : IRequest<ApiResponse<bool>>
    {
        public string Name { get; set; }

        public class AddCourseCommandHandler : IRequestHandler<AddCourseCommand, ApiResponse<bool>>
        {
            private readonly DBContext _dBContext;

            public AddCourseCommandHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<ApiResponse<bool>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var course = new Course();
                    course.Name = request.Name;
                    _dBContext.Courses.Add(course);
                    _dBContext.SaveChanges();


                    return await Task.FromResult(new ApiResponse<bool>()
                    {
                        Data = false,
                        IsError = false,
                        StatusCode = 200,
                        Description = "Course Added Successfully"
                    });
                }
                catch (Exception e)
                {
                    return await Task.FromResult(new ApiResponse<bool>()
                    {
                        Data = false,
                        IsError = true,
                        StatusCode = 500,
                        Description = "Something went wrong"
                    });
                }
            }
        }
    }
}

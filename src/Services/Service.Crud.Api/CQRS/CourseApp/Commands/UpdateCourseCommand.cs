using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class UpdateCourseCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public class AddCourseCommandHandler : IRequestHandler<UpdateCourseCommand, ApiResponse<bool>>
        {
            private readonly DBContext _dBContext;

            public AddCourseCommandHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<ApiResponse<bool>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var course = _dBContext.Courses.Where(x => x.Id == request.Id).FirstOrDefault();

                    if (course != null)
                    {
                        course.Name = request.Name;
                        _dBContext.Entry(course).State = EntityState.Modified; ;
                        _dBContext.SaveChanges();


                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 200,
                            Description = "Course updated Successfully"
                        });
                    }
                    else {
                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 404,
                            Description = "Course not found"
                        });

                    }
                    
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
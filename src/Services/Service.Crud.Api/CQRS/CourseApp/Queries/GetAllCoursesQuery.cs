using MediatR;
using Service.Crud.Api.Infrastructure.Response;
using Service.Crud.Api.Persistence;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Crud.Api.CQRS.CourseApp.Queries
{
    public class GetAllCoursesQuery : IRequest<ApiResponse<IEnumerable<Course>>>
    {
        public class GetAllStudentsQueryHandler : IRequestHandler<GetAllCoursesQuery, ApiResponse<IEnumerable<Course>>>
        {
            private readonly DBContext _dBContext;
            public GetAllStudentsQueryHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }
            public async Task<ApiResponse<IEnumerable<Course>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
            {
                var courses = _dBContext.Courses.ToList();

                return await Task.FromResult(new ApiResponse<IEnumerable<Course>>()
                {
                    Data = courses,
                    IsError = false,
                    StatusCode = 200,
                    Description = "Course record fetched successfully"
                });
            }
        }
    }
}

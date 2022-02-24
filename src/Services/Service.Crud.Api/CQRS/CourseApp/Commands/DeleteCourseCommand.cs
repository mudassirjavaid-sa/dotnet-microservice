using MediatR;
using Service.Crud.Api.Infrastructure.Response;
using Service.Crud.Api.Persistence;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Crud.Api.CQRS.StudentApp.Commands
{
    public class DeleteCourseCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }

        public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, ApiResponse<bool>>
        {
            private readonly DBContext _dBContext;

            public DeleteCourseCommandHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<ApiResponse<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var course = _dBContext.Courses.Where(x => x.Id == request.Id).FirstOrDefault();

                    if (course != null)
                    {
                        _dBContext.Courses.Remove(_dBContext.Courses.Where(x => x.Id.Equals(request.Id)).FirstOrDefault());
                        _dBContext.SaveChanges();


                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 200,
                            Description = "Course deleted Successfully"
                        });
                    }
                    else {
                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 404,
                            Description = "Course does not found"
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

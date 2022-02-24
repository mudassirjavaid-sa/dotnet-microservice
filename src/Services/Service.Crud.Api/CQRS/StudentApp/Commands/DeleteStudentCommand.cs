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
    public class DeleteStudentCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }

        public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, ApiResponse<bool>>
        {
            private readonly DBContext _dBContext;

            public DeleteStudentCommandHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<ApiResponse<bool>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = _dBContext.Students.Where(x => x.Id == request.Id).FirstOrDefault();

                    if (student != null)
                    {
                        _dBContext.Students.Remove(_dBContext.Students.Where(x => x.Id.Equals(request.Id)).FirstOrDefault());
                        _dBContext.SaveChanges();


                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 200,
                            Description = "Student deleted Successfully"
                        });
                    }
                    else {
                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 404,
                            Description = "Student not found"
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

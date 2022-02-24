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

namespace Service.Crud.Api.CQRS.StudentApp.Commands
{
    public class UpdateStudentCommand : IRequest<ApiResponse<bool>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RollNo { get; set; }

        public class AddCourseCommandHandler : IRequestHandler<UpdateStudentCommand, ApiResponse<bool>>
        {
            private readonly DBContext _dBContext;

            public AddCourseCommandHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<ApiResponse<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = _dBContext.Students.Where(x => x.Id == request.Id).FirstOrDefault();

                    if (student != null)
                    {
                        
                        student.Name = request.Name;
                        student.RollNo = request.RollNo;
                        _dBContext.Entry(student).State = EntityState.Modified; ;
                        _dBContext.SaveChanges();


                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 200,
                            Description = "Student updated Successfully"
                        });
                    }
                    else {
                        return await Task.FromResult(new ApiResponse<bool>()
                        {
                            Data = false,
                            IsError = false,
                            StatusCode = 200,
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
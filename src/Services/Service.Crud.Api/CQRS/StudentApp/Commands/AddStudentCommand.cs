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
    public class AddStudentCommand : IRequest<ApiResponse<bool>>
    {
        public string Name { get; set; }
        public int RollNo { get; set; }

        public class CreateCommandHandler : IRequestHandler<AddStudentCommand, ApiResponse<bool>>
        {
            private readonly DBContext _dBContext;

            public CreateCommandHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }

            public async Task<ApiResponse<bool>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var student = new Student();
                    student.Name = request.Name;
                    student.RollNo = request.RollNo;
                    _dBContext.Students.Add(student);
                    _dBContext.SaveChanges();


                    return await Task.FromResult(new ApiResponse<bool>()
                    {
                        Data = false,
                        IsError = false,
                        StatusCode = 200,
                        Description = "Student Added Successfully"
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

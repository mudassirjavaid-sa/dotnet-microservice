using MediatR;
using Service.Crud.Api.Infrastructure.Response;
using Service.Crud.Api.Persistence;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Crud.Api.CQRS.StudentApp.Queries
{
    public class GetAllStudentsQuery : IRequest<ApiResponse<IEnumerable<Student>>>
    {
        public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, ApiResponse<IEnumerable<Student>>>
        {
            private readonly DBContext _dBContext;
            public GetAllStudentsQueryHandler(DBContext dBContext)
            {
                _dBContext = dBContext;
            }
            public async Task<ApiResponse<IEnumerable<Student>>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
            {
                var students = _dBContext.Students.ToList();
             

                return await Task.FromResult(new ApiResponse<IEnumerable<Student>>()
                {
                    Data = students,
                    IsError = false,
                    StatusCode = 200,
                    Description = "Students record fetched successfully"
                });
            }
        }
    }
}

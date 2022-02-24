using AutoMapper;
using Service.Crud.Api.Domain.Entities;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Crud.Api.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDto, Student>();
        }

    }
}

﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Crud.Api.Infrastructure.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public bool IsError { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Description { get; set; }
    }
}

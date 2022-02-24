using Microsoft.EntityFrameworkCore;
using Service.Crud.Api.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Crud.Api.Persistence
{
    public class DBContext : DbContext
    {
    
        public DBContext(DbContextOptions<DBContext> options)
               : base(options)
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

    }
}

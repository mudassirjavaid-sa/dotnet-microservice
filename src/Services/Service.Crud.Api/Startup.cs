using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service.Crud.Api.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Service.Crud.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddCors(o =>
            //o.AddDefaultPolicy(b =>
            //    b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddDbContext<DBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                    ServiceLifetime.Scoped);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // AutoMapper
            services.AddAutoMapper(typeof(Startup));
            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Crud Api",
                    Description = "Microservice for Crud"
                });
            });
            // MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crud.API v1"));
            

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseRouting();
            //app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

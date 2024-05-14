﻿using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Mappers;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace EmployeeManagement
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
                services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

                services.AddControllers();
                services.AddEndpointsApiExplorer();
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Management API", Version = "v1" });
                    c.OperationFilter<SwaggerFileOperationFilter>();
                });

                // Dependencies Injection
                services.AddScoped<IRoleRepository, RoleRepository>();
                services.AddScoped<IRoleService, RoleService>();
                services.AddScoped<IFormTypeRepository, FormTypeRepository>();
                services.AddScoped<IFormTypeService, FormTypeService>();
                services.AddScoped<IClaimRepository, ClaimRepository>();
                services.AddScoped<IClaimService, ClaimService>();
                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<ISalaryRepository, SalaryRepository>();
                services.AddScoped<ISalaryService, SalaryService>();
                services.AddScoped<IFormRepository, FormRepository>();
                services.AddScoped<IFormService, FormService>();

                // AutoMapper
                services.AddAutoMapper(typeof(MapperProfile));

                // Optional: Uncomment the following lines if you need CORS
                // services.AddCors(options =>
                // {
                //     options.AddDefaultPolicy(builder =>
                //     {
                //         builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                //     });
                // });
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Management API v1"));
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthorization();

                // Optional: Uncomment if you need CORS
                // app.UseCors();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        }
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileUploadParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) || p.ParameterType == typeof(IEnumerable<IFormFile>))
                .ToList();

            if (!fileUploadParams.Any())
                return;

            foreach (var param in fileUploadParams)
            {
                operation.Parameters.Remove(operation.Parameters.First(p => p.Name == param.Name));
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties =
                                {
                                    [param.Name] = new OpenApiSchema
                                    {
                                        Type = "string",
                                        Format = "binary"
                                    }
                                }
                            }
                        }
                    }
                };
            }
        }
    }
}

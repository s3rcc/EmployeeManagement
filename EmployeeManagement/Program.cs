
using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Repositories;
using EmployeeManagement.Mappers;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Services;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    }
}

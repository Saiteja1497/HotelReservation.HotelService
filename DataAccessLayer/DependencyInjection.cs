﻿using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using DataAccessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelReservation.HotelService.DataAccessLayer
{
    public static class DependencyInjection
    {
        public  static IServiceCollection  AddDataAccessLayer(this IServiceCollection services,IConfiguration configuration)
        {
            string connectionStringTemplate = configuration.GetConnectionString("MySQLDatabase")!;
            string connectionString = connectionStringTemplate
                .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST")!)
                .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD")!)
                .Replace("$MYSQL_DATABASE", Environment.GetEnvironmentVariable("MYSQL_DATABASE")!)
                .Replace("$MYSQL_USER", Environment.GetEnvironmentVariable("MYSQL_USER")!)
                .Replace("$MYSQL_PORT", Environment.GetEnvironmentVariable("MYSQL_PORT")!);
            services.AddDbContext<ApplicationDbContext>(
                options =>
                {
                    options.UseMySQL(connectionString);
                }
                );
            services.AddScoped<IHotelRepository,HotelRepository>();
            return services;
        }
    }
}

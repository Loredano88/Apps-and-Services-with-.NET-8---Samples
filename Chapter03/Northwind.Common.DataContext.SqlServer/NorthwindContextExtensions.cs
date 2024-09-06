﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Northwind.EntityModels
{
    public static class NorthwindContextExtensions
    {
        /// <summary>
        /// Adds NorthwindContext to the specified IServiceCollection. Uses the SqlServer database provider.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="connectionString">Set to override the default.</param>
        /// <return>An IServiceCollection that can be used to add more services.</return>
        public static IServiceCollection AddNorthwindContext(
            this IServiceCollection services,
            string? connectionString = null)
        {
            if (connectionString == null) 
            {
                SqlConnectionStringBuilder builder = new();
                builder.DataSource = ".\\SQLEXPRESS";
                builder.InitialCatalog = "Northwind";
                builder.TrustServerCertificate = true;
                builder.MultipleActiveResultSets = true;
                
                // If using Azure SQL Edge.
                // builder.DataSource = "tcp:127.0.0.1,1433";
                
                // Because we want to fail fast. Default is 15 seconds.
                builder.ConnectTimeout = 3;
                
                // If using Windows Integrated authentication.
                //builder.IntegratedSecurity = true;
                
                // If using SQL Server authentication.
                builder.UserID = Environment.GetEnvironmentVariable("SQL_SERVER_USR", EnvironmentVariableTarget.Machine);
                builder.Password = Environment.GetEnvironmentVariable("SQL_SERVER_PWD", EnvironmentVariableTarget.Machine);
                
                connectionString = builder.ConnectionString;
            }

            // Log to console when executing EF Core commands
            services.AddDbContext<NorthwindContext>(options => 
            { 
                options.UseSqlServer(connectionString);
                options.LogTo(Console.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            },
            // Register with a transient lifetime to avoid concurrency
            // issues with Blazor Server projects.
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    }
}

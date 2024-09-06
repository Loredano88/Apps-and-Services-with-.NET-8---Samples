using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels
{ 
    public partial class NorthwindContext : DbContext
    {
        private static readonly SetLastRefreshedInterceptor setLastRefreshedInterceptor = new();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
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
                
                optionsBuilder.UseSqlServer(builder.ConnectionString);
            }
            
            optionsBuilder.AddInterceptors(setLastRefreshedInterceptor);
        }
    }
}

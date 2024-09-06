using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;
using Microsoft.CodeAnalysis.Options;

DbContextOptionsBuilder<HierarchyDb> options = new();
SqlConnectionStringBuilder builder = new();
builder.DataSource = ".\\SQLEXPRESS";
builder.InitialCatalog = "HierarchyMapping";
builder.TrustServerCertificate = true;
builder.MultipleActiveResultSets = true;
builder.ConnectTimeout = 3;

// Per autenticazione con credenziali Microsoft
//builder.IntegratedSecurity = true;

// Per autenticazione con credenziali Sql Server
builder.UserID = Environment.GetEnvironmentVariable("SQL_SERVER_USR", EnvironmentVariableTarget.Machine);
builder.Password = Environment.GetEnvironmentVariable("SQL_SERVER_PWD", EnvironmentVariableTarget.Machine);

options.UseSqlServer(builder.ConnectionString);
using (HierarchyDb db = new(options.Options))
{
    bool deleted = await db.Database.EnsureDeletedAsync();
    WriteLine($"Database deleted: {deleted}");

    bool created = await db.Database.EnsureCreatedAsync();
    WriteLine($"Database created: {created}");
    WriteLine("SQL script used to create the database:");
    WriteLine(db.Database.GenerateCreateScript());
    if ((db.Employees is not null) && (db.Students is not null))
    {
        db.Students.Add(new Student { Name = "Connor Roy", Subject = "Politics" });
        db.Employees.Add(new Employee { Name = "Kerry Castellabate", HireDate = DateTime.UtcNow });
        int result = db.SaveChanges();
        WriteLine($"{result} people added.");
    }
    if (db.Students is null || !db.Students.Any())
    {
        WriteLine("There are no students");
    }
    else
    {
        foreach (var student in db.Students) 
        {
            WriteLine("{0} studies {1}", student.Name, student.Subject);
        }
    }
    if (db.Employees is null || !db.Employees.Any())
    {
        WriteLine("There are no employees");
    }
    else
    {
        foreach (var employee in db.Employees)
        {
            WriteLine("{0} was hired on {1}", employee.Name, employee.HireDate);
        }
    }
    if (db.People is null || !db.People.Any())
    {
        WriteLine("There are no people.");
    }
    else
    {
        foreach (var person in db.People)
        {
            WriteLine("{0} has ID of {1}", person.Name, person.Id);
        }
    }
}
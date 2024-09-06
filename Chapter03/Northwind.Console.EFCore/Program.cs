using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

ConfigureConsole();

using (NorthwindDb db = new())
{
    Write("Enter a unit proce: ");
    string? priceText = ReadLine();
    if (!decimal.TryParse(priceText, out decimal price))
    {
        WriteLine("You must enter a valid unit price.");
        return;
    }
    var products = db.Products
        .AsNoTracking()
        .Where(_ => _.UnitPrice > price)
        .Select(_ => new { _.ProductId, _.ProductName, _.UnitPrice });
    WriteLine(new string('-', 58));
    WriteLine("| {0,5} | {1,-35} | {2,8} |", "Id", "Name", "Price");
    WriteLine(new string('-', 58));
    foreach (var p in products)
    {
        WriteLine("| {0,5} | {1,-35} | {2,8:C} |", p.ProductId, p.ProductName, p.UnitPrice);
    }
    WriteLine(new string('-', 58));
    WriteLine(products.ToQueryString());
    WriteLine();
    WriteLine($"Provider:   {db.Database.ProviderName}");
    WriteLine($"Connection: {db.Database.GetConnectionString()}");
}
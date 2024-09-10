using FluentValidation.Models;
using FluentValidation.Results;
using FluentValidation.Validators;
using System.Globalization;
using System.Text;

OutputEncoding = Encoding.UTF8;

Thread t = Thread.CurrentThread;
t.CurrentCulture = CultureInfo.GetCultureInfo("it-IT");
t.CurrentUICulture = t.CurrentCulture;
WriteLine($"Current culture: {t.CurrentCulture.DisplayName}");
WriteLine();
Order order = new Order() 
{
    OrderId = 10001,
    CustomerName = "Abcdef",
    CustomerEmail = "abc@example.com",
    CustomerLevel = CustomerLevel.Gold,
    OrderDate = new(2022, month: 12, day: 1),
    ShipDate = new(2022, month: 12, day: 5),
    Total = 49.99M
};
OrderValidator validator = new();
ValidationResult result = validator.Validate(order);
WriteLine($"CustomerName:  {order.CustomerName}");
WriteLine($"CustomerEmail: {order.CustomerEmail}");
WriteLine($"CustomerLevel: {order.CustomerLevel}");
WriteLine($"OrderId:       {order.OrderId}");
WriteLine($"OrderDate:     {order.OrderDate}");
WriteLine($"ShipDate:      {order.ShipDate}");
WriteLine($"Total:         {order.Total:C}");

WriteLine($"IsValid: {result.IsValid}");
foreach(var item in result.Errors)
{
    WriteLine($"    {item.Severity}: {item.ErrorMessage}");
}
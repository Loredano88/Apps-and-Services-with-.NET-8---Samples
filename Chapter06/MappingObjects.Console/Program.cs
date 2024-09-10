using AutoMapper;
using MappingObjects.Mappers;
using Northwind.EntityModels;
using Northwind.ViewModels;
using System.Text;

OutputEncoding = Encoding.UTF8;

Cart cart = new Cart(
    Customer: new Customer(
        FirstName: "John",
        LastName: "Smith"
    ),
    Items: new() 
    {
        new LineItem(ProductName: "Apples", UnitPrice: 0.49M, Quantity: 10),
        new LineItem(ProductName: "Bananas", UnitPrice: 0.99M, Quantity: 4)
    }
);
WriteLine("*** Original data before mapping.");
WriteLine($"{cart.Customer}");
foreach (var item in cart.Items)
{
    WriteLine($"    {item}");
}
MapperConfiguration config = CartToSummaryMapper.GetMapperConfiguration();
IMapper mapper = config.CreateMapper();

Summary summary = mapper.Map<Cart, Summary>(cart);

WriteLine();
WriteLine("*** After mapping.");
WriteLine($"Summary: {summary.FullName} spent {summary.Total:C}.");
namespace Northwind.ViewModels
{
    public record class Summary
    {
        public string? FullName { get; init; }
        public decimal Total { get; init; }
    }
}

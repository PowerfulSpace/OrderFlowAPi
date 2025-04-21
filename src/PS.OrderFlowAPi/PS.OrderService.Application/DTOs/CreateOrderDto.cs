namespace PS.OrderService.Application.DTOs
{
    public class CreateOrderDto
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}

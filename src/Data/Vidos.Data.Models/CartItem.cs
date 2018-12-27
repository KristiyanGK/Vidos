namespace Vidos.Data.Models
{
    public class CartItem : BaseModel
    {
        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public AirConditioner Product { get; set; }

        public string OrderId { get; set; }

        public Order Order { get; set; }
    }
}

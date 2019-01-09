using Newtonsoft.Json;

namespace Vidos.Data.Models
{
    public class CartItem : BaseModel
    {
        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public AirConditioner Product { get; set; }

        [JsonIgnore]
        public string OrderId { get; set; }

        [JsonIgnore]
        public Order Order { get; set; }
    }
}

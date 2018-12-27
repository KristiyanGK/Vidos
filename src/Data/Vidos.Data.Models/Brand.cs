using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class Brand : BaseModel
    {
        public Brand()
        {
            this.Products = new HashSet<AirConditioner>();
        }

        public string Name { get; set; }

        public ICollection<AirConditioner> Products { get; set; }
    }
}

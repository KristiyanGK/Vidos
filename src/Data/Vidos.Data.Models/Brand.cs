﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vidos.Data.Models
{
    public class Brand : BaseModel
    {
        public Brand()
        {
            this.Products = new HashSet<AirConditioner>();
        }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<AirConditioner> Products { get; set; }
    }
}

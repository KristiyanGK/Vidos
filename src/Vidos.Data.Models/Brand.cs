﻿using System;
using System.Collections.Generic;

namespace Vidos.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Products = new HashSet<AirConditioner>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<AirConditioner> Products { get; set; }
    }
}
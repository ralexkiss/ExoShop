using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataModels
{
    public class Product
    {
        public int ID { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string category { get; set; }
    }
}

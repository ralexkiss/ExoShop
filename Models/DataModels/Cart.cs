using Models.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Cart
    {
        public List<Product> products { get; set; }
        public User user { get; set;}
    }
}

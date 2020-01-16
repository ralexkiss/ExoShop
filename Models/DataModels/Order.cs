using System;
using System.Collections.Generic;
using System.Text;

namespace Models.DataModels
{
    public class Order
    {
        public int ID { get; set; }
        public User user { get; set; }
        public Billing billing { get; set; }
        public DateTime date { get; set; }
        public string Status { get; set; }

    }
}

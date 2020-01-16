 using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class BillingMemoryContext : IBillingContext
    {
        public Billing GetBillingById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddBilling(Billing billing)
        {
            throw new NotImplementedException();
        }

        public void RemoveBilling(Billing billing)
        {
            throw new NotImplementedException();
        }
    }
}
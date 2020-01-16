 using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class OrderSqlContext : IOrderContext
    {
        private MySqlConnection connection;
        public List<Order> GetAllOrdersByUser(User user)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
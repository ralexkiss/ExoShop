using Models.DataModels;
using System.Collections.Generic;

namespace Interfaces.Contexts
{
    /// <summary>
    /// Defines functionality for a order context.
    /// </summary>
    public interface IOrderContext
    {
        List<Order> GetAllOrdersByUser(User user);
        Order GetOrderById(int id);
        void AddOrder(Order order);
        void DeleteOrder(Order order);
    }
}
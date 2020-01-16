using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Data.Contexts
{
    public class OrderSqlContext : IOrderContext
    {
        private MySqlConnection connection;
        public List<Order> GetAllOrdersByUser(User user)
        {
            List<Order> orders = new List<Order>();
            List<Product> products = new List<Product>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders WHERE UserID=@UserID ", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", user.ID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Order order = new Order();
                                order.ID = (int)reader["ID"];
                                order.user = user;
                                order.Status = (string)reader["Status"];
                                order.date = reader["Date"] == DBNull.Value ? DateTime.Now : (DateTime)reader["Date"];

                                using (MySqlCommand getProducts = new MySqlCommand("SELECT * FROM Products INNER JOIN OrderProducts ON OrderProducts.ProductID = Products.ID WHERE OrderProducts.OrderID=@OrderID ORDER BY Price DESC", connection))
                                {
                                    getProducts.Parameters.AddWithValue("@OrderID", order.ID);
                                    using (MySqlDataReader getProductsReader = getProducts.ExecuteReader())
                                    {
                                        while (getProductsReader.Read())
                                        {
                                            Product product = new Product
                                            {
                                                ID = (int)getProductsReader["ID"],
                                                Name = (string)getProductsReader["Name"],
                                                Description = (string)getProductsReader["Description"],
                                                Price = (double)getProductsReader["Price"],
                                                ImageURL = (string)getProductsReader["ImageUrl"]
                                            };
                                            products.Add(product);
                                        }
                                    }
                                }
                                Billing billing = new Billing();
                                using (MySqlCommand getBilling = new MySqlCommand("SELECT * FROM Billing WHERE ID=@BillingID", connection))
                                {
                                    getBilling.Parameters.AddWithValue("@BillingID", reader["BillingID"]);
                                    using (MySqlDataReader getBillingReader = getBilling.ExecuteReader())
                                    {
                                        while (getBillingReader.Read())
                                        {
                                            billing.ID = (int)getBillingReader["ID"];
                                            billing.FirstName = (string)getBillingReader["FirstName"];
                                            billing.LastName = (string)getBillingReader["LastName"];
                                            billing.PhoneNumber = (string)getBillingReader["Phone"];
                                            billing.Address = (string)getBillingReader["Address"];
                                            billing.City = (string)getBillingReader["City"];
                                        }
                                    }
                                }
                                order.billing = billing;
                                order.products = products;
                                orders.Add(order);
                                }
                                return orders;
                            }
                        }
                    }
                }
            catch (MySqlException)
            {
                throw;
            }
        }

        public void AddOrder(Order order)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Order(UserID, BillingID, Password) VALUES (@UserID, @BillingID, @Status, @Date)", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", order.User.ID);
                        command.Parameters.AddWithValue("@BillingID", order.Billing.ID);    
                        command.Parameters.AddWithValue("@Status", order.Status);
                        command.Parameters.AddWithValue("@Date", order.Date);
                        command.ExecuteNonQuery();
                    }
                    foreach (Product product in order.Products)
                    {
                        using (MySqlCommand command = new MySqlCommand("INSERT INTO OrderProducts(ProductID , OrderID, Price) VALUES (@ProductID, @OrderID, @Price)", connection))
                        {
                            command.Parameters.AddWithValue("@ProductID", product.ID);
                            command.Parameters.AddWithValue("@OrderID", order.Billing.ID);
                            command.Parameters.AddWithValue("@Price", order.Products.Sum(foundProduct => foundProduct.Price));
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new RegistrationFailedException();
            }
        }

        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
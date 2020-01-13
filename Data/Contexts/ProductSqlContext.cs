 using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class ProductSqlContext : IProductContext
    {
        private MySqlConnection connection;

        public void Delete(int id)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM Products WHERE ID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Products ORDER BY Price DESC", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    ID = (int)reader["ID"],
                                    Name = (string)reader["Name"],
                                    Description = (string)reader["Description"],
                                    Price = (double)reader["Price"],
                                    ImageURL = (string)reader["ImageUrl"]
                                };
                                products.Add(product);
                            }
                            return products;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Product product)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Products (`Name`, `Description`, `Price`, `ImageUrl`) VALUES (@Name, @Description, @Price, @ImageUrl)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@ImageUrl", product.ImageURL);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
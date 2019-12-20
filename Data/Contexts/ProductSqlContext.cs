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
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Product", connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product();
                                product.ID = (int)reader["ID"];
                                product.Name = (string)reader["Name"];
                                product.Description = (string)reader["Description"];
                                product.Price = (double)reader["Price"];
                                product.ImageURL = (string)reader["PictureURL"];
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
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Product (`Name`, `Description`, `Price`, `PictureURL`) VALUES (@Name, @Description, @Price, @PictureURL)", connection))
                    {
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@PictureURL", product.ImageURL);
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
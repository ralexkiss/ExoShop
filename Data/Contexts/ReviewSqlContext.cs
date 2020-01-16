 using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class ReviewSqlContext : IReviewContext
    {
        private MySqlConnection connection;


        public List<Review> GetAllByProduct(Product product)
        {
            List<Review> reviews = new List<Review>();
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Reviews WHERE ProductID=@ProductID", connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", product.ID);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Review review = new Review
                                {
                                    ID = (int)reader["ID"],
                                    UserID = (int)reader["UserID"],
                                    ProductID = (int)reader["ProductID"],
                                    Stars = (int)reader["Stars"],
                                    Message = (string)reader["Review"]
                                };
                                reviews.Add(review);
                            }
                            return reviews;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public Review GetReviewById(int id)
        {
            try
            {
                Review review = new Review();
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Reviews WHERE ID=@ReviewID", connection))
                    {
                        command.Parameters.AddWithValue("@ReviewID", id);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                review.ID = (int)reader["ID"];
                                review.UserID = (int)reader["UserID"];
                                review.ProductID = (int)reader["ProductID"];
                                review.Stars = (int)reader["Stars"];
                                review.Message = (string)reader["Review"];
                            }
                            return review;
                        }
                    }
                }
            }
            catch (MySqlException)
            {
                throw;
            }
        }
        public void AddReview(Review review)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO Reviews(UserID, ProductID, Stars, Review) VALUES (@UserID,@ProductID,@Stars,@Message)", connection))
                    {
                        command.Parameters.AddWithValue("@UserID", review.UserID);
                        command.Parameters.AddWithValue("@ProductID", review.ProductID);
                        command.Parameters.AddWithValue("@Stars", review.Stars);
                        command.Parameters.AddWithValue("@Message", review.Message);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveReview(Review review)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM Reviews WHERE ID=@ReviewID", connection))
                    {
                        command.Parameters.AddWithValue("@ReviewID", review.ID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
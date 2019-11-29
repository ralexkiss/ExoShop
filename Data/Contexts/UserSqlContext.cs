﻿using Exceptions.User;
using Interfaces.Contexts;
using Models.DataModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Data.Contexts
{
    public class UserSqlContext : IUserContext
    {
        private MySqlConnection connection;

        public User AuthenticatUser(string email, string password)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand findcommand = new MySqlCommand("SELECT COUNT(*) FROM User WHERE Email = @Email AND Password = @Password", connection))
                    {
                        findcommand.Parameters.AddWithValue("@Email", email);
                        findcommand.Parameters.AddWithValue("Password", password);
                        if ((long)findcommand.ExecuteScalar() == 1)
                        {
                            MySqlCommand command = new MySqlCommand("SELECT ID, Name, IsAdmin FROM User WHERE Email = @Email", connection);
                            command.Parameters.AddWithValue("@Email", email);
                            User user = new User();
                            MySqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                user.ID = (int)reader["ID"];
                                user.Name = (string)reader["Name"];
                                user.IsAdmin = (bool)reader["IsAdmin"];
                                return user;
                            }
                        }
                        throw new AuthenticationFailedException();
                    }
                }
            }
            catch (MySqlException)
            {
                throw new AuthenticationFailedException();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(User user)
        {
            try
            {
                using (connection = DataConnection.getConnection())
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO User(Email, Name, Password) VALUES (@Email, @Name, @Password)", connection))
                    {
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Password", user.Password);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {
                throw new RegistrationFailedException();
            }
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
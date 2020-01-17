using Microsoft.AspNetCore.Http;
using Models.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ExoShop
{
    /*
    // Credits: https://www.talkingdotnet.com/store-complex-objects-in-asp-net-core-session/
    */
    public static class SessionExtensions
    {
        public static void CreateUser(this ISession session)
        {
            if (!ContainsObject(session, "loggedInUser"))
            {
                User tempUser = new User
                {
                    Name = "",
                    IsAdmin = false,
                    WishList = new List<Product>(),
                    Cart = new List<Product>()
                };
                SetObject(session, "loggedInUser", tempUser);
            }
        }

        public static void ResetUser(this ISession session)
        {
            if (ContainsObject(session, "loggedInUser"))
            {
                session.SetInt32("id", 0);
                session.SetString("name", string.Empty);
                session.SetString("admin", "false");
                DeleteObject(session, "loggedInUser");
            }
            CreateUser(session);
        }

        public static void UpdateUser(this ISession session, User user)
        {
            session.SetInt32("id", user.ID);
            session.SetString("name", user.Name);
            session.SetString("admin", user.IsAdmin.ToString().ToLower());
            SetObject(session, "loggedInUser", user);
        }

        public static User GetUser(this ISession session)
        {
            return GetObject<User>(session, "loggedInUser");
        }
        public static void SetObject(this ISession session, string key, object value)
        {
            if (ContainsObject(session, key))
            {
                DeleteObject(session, key);
            }
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static bool ContainsObject(this ISession session, string key)
        {
            return session.Get(key) != null;
        }
        public static void DeleteObject(this ISession session, string key)
        {
            session.Remove(key);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static bool? GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToBoolean(data, 0);
        }

        public static void SetDouble(this ISession session, string key, double value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }

        public static double? GetDouble(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToDouble(data, 0);
        }
    }
}

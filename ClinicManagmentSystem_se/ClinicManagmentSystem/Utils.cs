using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace ClinicManagmentSystem
{
    public class Utils
    {
        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encrypted_bytes = sha1.ComputeHash(password_bytes);

            return Convert.ToBase64String(encrypted_bytes);
        }

        public static Dictionary<int, string> getSlots()
        {
            Dictionary<int, string> slots = new Dictionary<int, string>();
            slots.Add(1, "Slot 1:From 6:00PM to 6:30PM");
            slots.Add(2, "Slot 2:From 6:30AM to 7:00PM");
            slots.Add(3, "Slot 3:From 7:00AM to 7:30PM");
            slots.Add(4, "Slot 4:From 7:30AM to 8:00PM");
            slots.Add(5, "Slot 5:From 8:00AM to 8:30PM");
            slots.Add(6, "Slot 6:From 8:30AM to 9:00PM");
            slots.Add(7, "Slot 7:From 9:00AM to 9:30PM");
            slots.Add(8, "Slot 8:From 9:30AM to 10:00PM");
            slots.Add(10, "Slot 9:From 10:00AM to 10:30PM");
            slots.Add(11, "Slot 10:From 10:30AM to 11:00PM"); 

            return slots;
           
        }


        public static void createAdmin(string password)
        {
            SqlConnection con = new SqlConnection(Properties.Resources.connectionString);
            SqlCommand command = con.CreateCommand();

            command.CommandText = "INSERT INTO [user] (user_username, user_password) VALUES (@username, @password)";
            command.Parameters.AddWithValue("@username", "admin");
            command.Parameters.AddWithValue("@password","admin");

            con.Open();

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            con.Close();
        }
    }
}

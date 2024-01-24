using LTT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Linq;
using System.Data;

using System.Net;
using MimeKit;
using MailKit.Net.Smtp;



namespace LTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private IConfiguration _configuration;

        public SignupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("sp")]

        public string sp(Signup signup)
        {
            try
            {
                //// Encrypt the password before storing it
                //string encryptedPassword = AesEncryption.Encrypt(signup.Password);

                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("L").ToString());
                SqlCommand cmd = new SqlCommand("INSERT INTO Signnup(Username,Password) VALUES('" + signup.Username + "', '" + signup.Password + "' )", con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                if (i > 0)
                {
                    return "Stored!";
                }
                else
                {
                    return "Error";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the exception)
                return "Error: " + ex.Message;
            }
        }

        [HttpPost]
        [Route("login")]
        public string login(Signup signup)
        {
            try
            {
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("L").ToString());
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Signnup WHERE Username='" + signup.Username + "' AND Password='" + signup.Password + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                //{
                //    string storedEncryptedPassword = dt.Rows[0]["Password"].ToString();
                //    string storedDecryptedPassword = AesEncryption.Decrypt(storedEncryptedPassword);

                //    // Log the decrypted password for debugging purposes
                //    Console.WriteLine("Decrypted Password: " + storedDecryptedPassword);

                //    if (signup.Password == storedDecryptedPassword)
                //    {
                //        return "Login Successful";
                //    }
                //    else
                //    {
                //        return "Invalid Password";
                //    }
                {
                    return "Loged In";
                }
                
                else
                {
                    return "Invalid User";
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately (e.g., log the exception)
                return "Error: " + ex.Message;
            }
        }

       

    }





}









using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using LTT.Models;
using System.Data.SqlClient;
using MailKit.Net.Smtp;
using System.Net;
using MimeKit;
using Microsoft.Extensions.Configuration;
using NSspi;
using static System.Net.WebRequestMethods;


namespace LTT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {
        private IConfiguration _configuration;

        public OtpController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //private readonly OtpDbContext _context;

        //public OtpController(OtpDbContext context)
        //{
        //    _context = context;
        //}

        [HttpPost]
        [Route("generate")]

        public ActionResult<string> GenerateAndSendOtp([FromBody] otpData otp)
        {
            string email = otp.Email;
            string randno = GenerateOtp();

            // Save the OTP in the database
            SaveOtpInDatabase(email, randno);

            // Send the OTP to the provided email
            SendOtpEmail(email, randno);

            return Ok("OTP sent successfully");
        }

        private void SaveOtpInDatabase(string email, string otp)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("L")))
            {
                con.Open();

                string insertQuery = "INSERT INTO OTP(Email, Otp, Timestamps) VALUES (@Email, @Otp, GETDATE())";
                using (SqlCommand cmd1 = new SqlCommand(insertQuery, con))
                {
                    cmd1.Parameters.AddWithValue("@Email", email);
                    cmd1.Parameters.AddWithValue("@Otp", otp);

                    cmd1.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        private void SendOtpEmail(string email, string otp)
        {
            string smtpServer = "smtp.ethereal.email";
            int smtpPort = 587; // or your specific SMTP port
            string smtpUsername = "grayson.haag@ethereal.email";
            string smtpPassword = "aca7V3vFz4UKuj5VXb";

            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, smtpPort, false);
                client.Authenticate(smtpUsername, smtpPassword);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Sender", "grayson.haag@ethereal.email"));
                message.To.Add(new MailboxAddress("Recipient", email));
                message.Subject = "Your OTP";
                message.Body = new TextPart("plain")
                {
                    Text = $"Your OTP is: {otp}"
                };

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        [HttpPost]
        [Route("validateOtp")]
        public ActionResult<string> ValidateOtp([FromBody] otpData otp)
        {
            string enteredEmail = otp.Email;
            string enteredOtp = otp.Otp;

            // Fetch the saved OTP from the database based on user input
            string savedOtp = FetchOtpFromDatabase(enteredEmail);

            if (string.IsNullOrEmpty(savedOtp))
            {
                return "Email not found in the database";
            }

            // Log the fetched and entered OTP values
            Console.WriteLine($"Fetched OTP from database: {savedOtp}");
            Console.WriteLine($"Entered OTP: {enteredOtp}");

            // Validate the entered OTP with the saved OTP using a case-insensitive comparison
            if (string.Equals(enteredOtp, savedOtp, StringComparison.OrdinalIgnoreCase))
            {
                return "Authentication successful";
            }

            return BadRequest("Invalid OTP");
        }

        private string FetchOtpFromDatabase(string enteredEmail)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("L")))
            {
                con.Open();
                
                string selectQuery = "SELECT Otp FROM OTP WHERE Email = @EnteredEmail ORDER BY Timestamps DESC";
                using (SqlCommand cmd = new SqlCommand(selectQuery, con))
                {
                    cmd.Parameters.AddWithValue("@EnteredEmail", enteredEmail);
                    return cmd.ExecuteScalar() as string;
                }
            }
        }
    }

    






}


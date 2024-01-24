using System.Data.SqlClient;

namespace LTT.Common
{
    public class OtpService : IOtpService
    {
        private readonly IConfiguration _configuration;

        public OtpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAndSendOtp(string email)
        {
            string randno = GenerateOtp();

            // Save the OTP in the database
            SaveOtpInDatabase(email, randno);

            // Send the OTP to the provided email
            SendOtpEmail(email, randno);

            return "OTP sent successfully";
        }

        public string ValidateOtp(string email, string enteredOtp)
        {
            // Fetch the saved OTP from the database based on user input
            string savedOtp = FetchOtpFromDatabase(email);

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

            return "Invalid OTP";
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
            // Same email sending logic as in your original code
            // ...
        }

        private string GenerateOtp()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
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

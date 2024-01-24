
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MimeKit;
//using MailKit.Net.Smtp;
//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using LTT.Models;

//namespace LTT.Common
//{
//    public class otp
//    {

//        private string GenerateOtp()
//        {
//            Random random = new Random();
//            return random.Next(100000, 999999).ToString();
//        }

//        private void SendOtpByEmail(string email, string otp)
//        {
//            var message = new MimeMessage();
//            message.From.Add(new MailboxAddress("Your Name", "your_email@gmail.com")); // Replace with your name and email
//            message.To.Add(new MailboxAddress("", email));
//            message.Subject = "Your OTP";
//            message.Body = new TextPart("plain")
//            {
//                Text = $"Your OTP is: {otp}"
//            };

//            using (var client = new SmtpClient())
//            {
//                client.Connect("smtp.gmail.com", 587, false);
//                client.Authenticate("your_email@gmail.com", "your_email_password"); // Replace with your email and password
//                client.Send(message);
//                client.Disconnect(true);
//            }
//        }
//        private void StoreOtpInDatabase(string email, string otp)
//        {
//            var otpData = new OtpData
//            {
//                Email = email,
//                Otp = otp,
//                Timestamp = DateTime.Now
//            };

//            _context.OtpData.Add(otpData);
//            _context.SaveChanges();
//        }
//    }
//    }

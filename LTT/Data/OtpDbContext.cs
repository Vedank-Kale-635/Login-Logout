using LTT.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LTT.Data
{
    public class OtpDbContext : DbContext
    {
        public DbSet<otpData> OtpData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=otp_database.db");
    }
}

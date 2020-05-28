using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MessageApi.Models
{
    public partial class MessageDBContext : DbContext
    {
        public MessageDBContext()
        {
        }

        public MessageDBContext(DbContextOptions<MessageDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Message> Message { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbUser = Environment.GetEnvironmentVariable("DB_USER");
                var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
                var connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

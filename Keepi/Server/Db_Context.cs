using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Keepi.Server
{
    public class Db_Context : DbContext
    {
        public Db_Context(DbContextOptions<Db_Context> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePhoto { get; set; }
        public string Following { get; set; }
        public string Followers { get; set; }
        public int WalletCount { get; set; }

    }
}

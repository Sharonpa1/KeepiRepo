using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Keepi.Shared
{

    [Table("UsersTbl")]
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string ProfilePhoto { get; set; } = "aaa";
        public string Following { get; set; } = "Following list";
        public string Followers { get; set; } = "Followers list";
        public int WalletCount { get; set; } = 100;
    }
}

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
        public User()
        {
                
        }

        public User(User user)
        {
            Id = user.Id;
            Username = user.Username;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Password = user.Password;
            Email = user.Email;
            City = user.City;
            PhoneNumber = user.PhoneNumber;
            BirthDate = user.BirthDate;
            ProfilePhoto = user.ProfilePhoto;
            Following = user.Following;
            Followers = user.Followers;
            WalletCount = user.WalletCount;
            SavedPosts = user.SavedPosts;
        }

        //public User(Guid _Id, string _Username, string _FirstName, string _LastName, string _Password,
        //            string _Email, string _City, string _PhoneNumber, DateTime _BirthDate, string _ProfilePhoto,
        //            string _Following, string _Followers, int _WalletCount)
        //{
        //    Id = _Id;
        //    Username = _Username;
        //    FirstName = _FirstName;
        //    LastName = _LastName;
        //    Password = _Password;
        //    Email = _Email;
        //    City = _City;
        //    PhoneNumber = _PhoneNumber;
        //    BirthDate = _BirthDate;
        //    ProfilePhoto = _ProfilePhoto;
        //    Following = _Following;
        //    Followers = _Followers;
        //    WalletCount = _WalletCount;
                
        //}
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
        public string SavedPosts { get; set; } = "";
    }
}

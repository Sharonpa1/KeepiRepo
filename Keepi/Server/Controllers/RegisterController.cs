using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Keepi.Shared;
using static Duende.IdentityServer.Models.IdentityResources;

namespace Keepi.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly Db_Context _context;

        public RegisterController(Db_Context context)
        {
            _context = context;
        }


        //    [HttpPost("register")]
        //    public async Task<List<bool>> Register([FromBody] RegistrationModel model)
        //    {
        //        try
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                // Check if username or email already exists
        //                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username || u.Email == model.Email);
        //                if (existingUser != null)
        //                {
        //                    //return BadRequest("Username or Email already exists.");
        //                    return new List<bool> { false };
        //                }

        //                // Create new user
        //                var user = new User
        //                {
        //                    Username = model.Username,
        //                    FirstName = model.FirstName,
        //                    LastName = model.LastName,
        //                    Password = model.Password, // Ideally, hash the password before storing it
        //                    Email = model.Email,
        //                    City = model.City,
        //                    PhoneNumber = model.PhoneNumber
        //                };

        //                _context.Users.Add(user);
        //                await _context.SaveChangesAsync();

        //                //return Ok();
        //                return new List<bool> { true };
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return new List<bool> { false };
        //        }

        //        //return BadRequest(ModelState);
        //        return new List<bool> { false };
        //    }
        //}

        //[HttpGet("_register/{_user}")]
        //public async Task<List<User>> Register(RegistrationModel _user)
        [HttpGet("_register/{_username}/{_firstName}/{_lastName}/{_password}/{_email}/{_city}/{_phoneNumber}")]
        public async Task<List<User>> Register(string _username, string _firstName, string _lastName, string _password, string _email, string _city, string _phoneNumber)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == _username || u.Email == _email);
                    if (existingUser != null)
                    {
                        return new List<User> { };
                    }

                    var user = new User
                    {
                        Username = _username,
                        FirstName = _firstName,
                        LastName = _lastName,
                        Password = _password, // Ideally, hash the password before storing it
                        Email = _email,
                        City = _city,
                        PhoneNumber = _phoneNumber
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    return new List<User> { user };
                }
            }
            catch (Exception)
            {
                return new List<User> { };
            }

            return new List<User> { };
        }
    }

    public class RegistrationModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}

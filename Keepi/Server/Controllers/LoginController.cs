using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Keepi.Shared;

namespace Keepi.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly Db_Context _context;

        public LoginController(Db_Context context)
        {
            _context = context;
        }

        [HttpGet("test")]
        public async Task<List<bool>> Test()
        {
            return new List<bool> { false };
        }

        [HttpGet]
        //public async Task<List<User>> Login([FromBody] LoginModel model)
        public async Task<List<User>> LoginAsync([FromQuery] string _email, [FromQuery] string _password)
        {
            if (ModelState.IsValid)
            {
                //    // Check if user exists with the given username and password
                var users = await _context.Users.FirstOrDefaultAsync(u => u.Email == _email && u.Password == _password);

                //    if (user != null)
                //    {
                //        // Successful login
                //        return Ok(new { success = true, message = "Login successful." });
                //    }
                //    else
                //    {
                //        // Invalid username or password
                //        return Unauthorized(new { success = false, message = "Invalid username or password." });
                //    }
            }

            //return BadRequest(ModelState);

            return new List<User> { };
        }

        [HttpGet("_login/{_userName}/{_password}")]
        public async Task<List<User>> Login(string _userName, string _password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == _email && u.Password == _password);
                    User user = await _context.Users.FirstOrDefaultAsync(u => u.Username == _userName && u.Password == _password);

                    if (user != null)
                    {
                        // Successful login
                        return new List<User> { user };
                    }
                    else
                    {
                        // Invalid username or password
                        return new List<User> { };
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }
    }

    //public class LoginModel
    //{
    //    [Required]
    //    public string Email { get; set; }
    //    [Required]
    //    public string Password { get; set; }
    //}
}

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

        [HttpGet("_login/{_userName}/{_password}")]
        public async Task<List<User>> Login(string _userName, string _password)
        {
            try
            {
                if (ModelState.IsValid)
                {
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
}

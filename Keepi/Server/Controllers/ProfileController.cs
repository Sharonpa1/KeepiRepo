using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Keepi.Shared;

namespace Keepi.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly Db_Context _context;

        public ProfileController(Db_Context context)
        {
            _context = context;
        }

        //[HttpGet("test")]
        //public async Task<List<bool>> Test()
        //{
        //    return new List<bool> { false };
        //}

        [HttpPost("upload_image")]
        public async Task<List<bool>> UploadProfileImage()
        {
            var file = Request.Form.Files.FirstOrDefault();

            if (file == null || file.Length == 0)
                return new List<bool> { false };

            var uploads = Path.Combine("C:\\Users\\sharo\\source\\repos\\KeepiRepo\\Keepi\\Server\\API\\UserUploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var filePath = Path.Combine(uploads, file.FileName);
            //var filePath = Path.Combine(uploads, userId);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // כאן נוסיף את שמירת הנתיב למסד הנתונים
            var userId = 1; // קבל את ה-userId מתוך ה-context או פרמטרים אחרים.
            await UpdateUserProfileImageAsync(userId, filePath);

            return new List<bool> { true };
        }

        [HttpGet("editUserName/{userId}/{newUserName}")]
        public async Task<List<User>> EditUserName(Guid userId, string newUserName)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.Username = newUserName;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("editFirstName/{userId}/{newFirstName}")]
        public async Task<List<User>> EditFirstName(Guid userId, string newFirstName)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.FirstName = newFirstName;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("editLastName/{userId}/{newLastName}")]
        public async Task<List<User>> EditLastName(Guid userId, string newLastName)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.LastName = newLastName;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("editPassword/{userId}/{newPassword}")]
        public async Task<List<User>> EditPassword(Guid userId, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.Password = newPassword;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("editCity/{userId}/{newCity}")]
        public async Task<List<User>> EditCity(Guid userId, string newCity)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.City = newCity;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("editPhoneNumber/{userId}/{newPhoneNumber}")]
        public async Task<List<User>> EditPhoneNumber(Guid userId, string newPhoneNumber)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.PhoneNumber = newPhoneNumber;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("editBirthDate/{userId}/{newBirthDate}")]
        public async Task<List<User>> EditBirthDate(Guid userId, DateTime newBirthDate)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.BirthDate = newBirthDate;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }



        //[HttpPost("upload_image")]
        //public async Task<List<bool>> UploadProfileImage([FromForm] MultipartFormDataContent content)
        //{
        //    var fileSection = content?.FirstOrDefault(section => section.Headers.ContentDisposition.Name.Trim('"') == "file") as StreamContent;

        //    if (fileSection == null)
        //        return new List<bool> { false };

        //    var fileName = fileSection.Headers.ContentDisposition.FileName.Trim('"');
        //    var uploads = Path.Combine("C:\\Users\\sharo\\source\\repos\\KeepiRepo\\Keepi\\Server\\API\\UserUploads");

        //    if (!Directory.Exists(uploads))
        //    {
        //        Directory.CreateDirectory(uploads);
        //    }

        //    var filePath = Path.Combine(uploads, fileName);

        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await fileSection.CopyToAsync(fileStream);
        //    }

        //    // כאן נוסיף את שמירת הנתיב למסד הנתונים
        //    var userId = 1; // קבל את ה-userId מתוך ה-context או פרמטרים אחרים.
        //    await UpdateUserProfileImageAsync(userId, filePath);

        //    return new List<bool> { true };
        //}


        //[HttpPost("upload_image")]
        //public async Task<List<bool>> UploadProfileImage([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return new List<bool> { false };

        //    //var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
        //    var uploads = Path.Combine("C:\\Users\\sharo\\source\\repos\\KeepiRepo\\Keepi\\Server\\API\\UserUploads");
        //    if (!Directory.Exists(uploads))
        //    {
        //        Directory.CreateDirectory(uploads);
        //    }

        //    var filePath = Path.Combine(uploads, file.FileName);
        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }

        //    // כאן נוסיף את שמירת הנתיב למסד הנתונים
        //    var userId = 1; // קבל את ה-userId מתוך ה-context או פרמטרים אחרים.
        //    await UpdateUserProfileImageAsync(userId, filePath);

        //    return new List<bool> { true };
        //}


        public async Task UpdateUserProfileImageAsync(int userId, string filePath)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.ProfilePhoto = filePath;
                await _context.SaveChangesAsync();
            }
        }
    }

}

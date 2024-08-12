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
        private readonly IWebHostEnvironment _environment;

        public ProfileController(Db_Context context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

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
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                    user.Password = hashedPassword;
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

        [HttpGet("editAge/{userId}/{newAge}")]
        public async Task<List<User>> EditAge(Guid userId, int newAge)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FindAsync(userId);

                if (user != null)
                {
                    user.Age = newAge;
                    await _context.SaveChangesAsync();
                    return new List<User> { user };
                }
            }
            return new List<User> { };
        }

        [HttpGet("followUser/{_CurrentUserId}/{_UserIdToFollow}")]
        public async Task<List<User>> FollowUser(Guid _CurrentUserId, Guid _UserIdToFollow)
        {
            try
            {
                User CurrentUser = _context.Users.FirstOrDefault(u => u.Id == _CurrentUserId);
                User UserToFollow = _context.Users.FirstOrDefault(u => u.Id == _UserIdToFollow);

                if (CurrentUser != null && UserToFollow != null)
                {
                    CurrentUser.Following += _UserIdToFollow + ";";
                    UserToFollow.Followers += _CurrentUserId + ";";
                    await _context.SaveChangesAsync();

                    return new List<User> { CurrentUser };
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        [HttpGet("unFollowUser/{_CurrentUserId}/{_UserIdToUnFollow}")]
        public async Task<List<User>> UnFollowUser(Guid _CurrentUserId, Guid _UserIdToUnFollow)
        {
            try
            {
                User CurrentUser = _context.Users.FirstOrDefault(u => u.Id == _CurrentUserId);
                User UserToUnFollow = _context.Users.FirstOrDefault(u => u.Id == _UserIdToUnFollow);

                if (CurrentUser != null && UserToUnFollow != null)
                {
                    string[] currentUser_followingList = CurrentUser.Following.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var filteredFollowingList = currentUser_followingList.Where(item => item != _UserIdToUnFollow.ToString());
                    CurrentUser.Following = string.Join(";", filteredFollowingList) + ";";

                    string[] userToUnfollow_followersList = UserToUnFollow.Followers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var filteredFollowersList = userToUnfollow_followersList.Where(item => item != _CurrentUserId.ToString());
                    UserToUnFollow.Followers = string.Join(";", filteredFollowersList) + ";";


                    await _context.SaveChangesAsync();

                    return new List<User> { CurrentUser };
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        [HttpGet("getFollowers/{userId}")]
        public async Task<List<User>> GetUserFollowersList(Guid userId)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == userId);
                List<User> followers = new List<User>();

                if (user != null)
                {
                    string[] followers_id = user.Followers.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (followers_id.Length > 0)
                    {
                        foreach (var _id in followers_id)
                        {
                            User follower = await _context.Users.FindAsync(Guid.Parse(_id));
                            if (follower != null)
                            {
                                followers.Add(follower);
                            }
                        }
                    }

                    return followers;
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        [HttpGet("getFollowing/{userId}")]
        public async Task<List<User>> GetUserFollowingList(Guid userId)
        {
            try
            {
                User user = _context.Users.FirstOrDefault(u => u.Id == userId);
                List<User> following = new List<User>();

                if (user != null)
                {
                    string[] following_id = user.Following.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (following_id.Length > 0)
                    {
                        foreach (var _id in following_id)
                        {
                            User _following = await _context.Users.FindAsync(Guid.Parse(_id));
                            if (_following != null)
                            {
                                following.Add(_following);
                            }
                        }
                    }

                    return following;
                }

            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }


        public async Task UpdateUserProfileImageAsync(int userId, string filePath)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.ProfilePhoto = filePath;
                await _context.SaveChangesAsync();
            }
        }

        //[HttpPost("upload")]
        //public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        //    var uploadPath = Path.Combine(_environment.WebRootPath, "profile-pictures", fileName);

        //    using (var fileStream = new FileStream(uploadPath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //    }

        //    //await _profileRepository.SaveProfilePicturePathAsync(fileName);

        //    return Ok(new { Path = $"/profile-pictures/{fileName}" });
        //}

    }

}

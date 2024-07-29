using Keepi.Client.Communication;
using Keepi.Client.Repositories.Base;
using Keepi.Client.Repositories.Interfaces;
using Keepi.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Keepi.Client.Repositories.Implementation
{
    public class ProfileRepository : Repository, IProfileRepository
    {
        private const string URL = "api/Profile";

        public ProfileRepository(IHttpService commService) : base(commService)
        {
        }

        public async Task<object> UploadProfileImage(MultipartFormDataContent file) =>
             await Post<MultipartFormDataContent>(URL + System.IO.Path.AltDirectorySeparatorChar + "upload_image", file);

        public async Task<List<User>> EditUserName(Guid userId, string newUserName) =>
             await Get<User>($"api/Profile/editUserName/{userId}/{newUserName}");

        public async Task<List<User>> EditFirstName(Guid userId, string newFirstName) =>
             await Get<User>($"api/Profile/editFirstName/{userId}/{newFirstName}");

        public async Task<List<User>> EditLastName(Guid userId, string newLastName) =>
             await Get<User>($"api/Profile/editLastName/{userId}/{newLastName}");
        
        public async Task<List<User>> EditPassword(Guid userId, string newPassword) =>
             await Get<User>($"api/Profile/editPassword/{userId}/{newPassword}");
       
        public async Task<List<User>> EditCity(Guid userId, string newCity) =>
             await Get<User>($"api/Profile/editCity/{userId}/{newCity}");
      
        public async Task<List<User>> EditPhoneNumber(Guid userId, string newPhoneNumber) =>
             await Get<User>($"api/Profile/editPhoneNumber/{userId}/{newPhoneNumber}");

        public async Task<List<User>> EditBirthDate(Guid userId, DateTime newBirthDate) =>
             await Get<User>($"api/Profile/editBirthDate/{userId}/{newBirthDate}");

        //public async Task<List<bool>> Test() =>
        //    await Get<bool>(URL + System.IO.Path.AltDirectorySeparatorChar + "test");
    }

}

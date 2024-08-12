﻿using Keepi.Shared;
using Microsoft.AspNetCore.Http;

namespace Keepi.Client.Repositories.Interfaces
{
    public interface IProfileRepository
    {
        Task<object> UploadProfileImage(MultipartFormDataContent file);
        //Task<object> UploadProfileImage(IFormFile file);

        Task<List<User>> EditFirstName(Guid userId, string newFirstName);
        Task<List<User>> EditLastName(Guid userId, string newLastName);
        Task<List<User>> EditPassword(Guid userId, string newPassword);
        Task<List<User>> EditCity(Guid userId, string newCity);
        Task<List<User>> EditPhoneNumber(Guid userId, string newPhoneNumber);
        Task<List<User>> EditAge(Guid userId, int newAge);
        Task<List<User>> FollowUser(Guid _CurrentUserId, Guid _UserIdToFollow);
        Task<List<User>> UnFollowUser(Guid _CurrentUserId, Guid _UserIdToFollow);
        Task<List<User>> GetUserFollowersList(Guid userId);
        Task<List<User>> GetUserFollowingList(Guid userId);

    }
}

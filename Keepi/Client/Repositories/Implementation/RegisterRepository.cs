﻿using Keepi.Client.Communication;
using Keepi.Client.Repositories.Base;
using Keepi.Client.Repositories.Interfaces;
using Keepi.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using static Keepi.Client.Pages.RegisterPage;

namespace Keepi.Client.Repositories.Implementation
{
    public class RegisterRepository : Repository, IRegisterRepository
    {
        private const string URL = "api/Register";

        public RegisterRepository(IHttpService commService) : base(commService)
        {
            
        }

        //public async Task<object> Register(RegistrationModel user) =>
        //     await Post<RegistrationModel>(URL + System.IO.Path.AltDirectorySeparatorChar + "register", user);

        //public async Task<List<User>> Register(RegistrationModel user) =>
        //   await Get<User>($"api/Register/_register/{user}");

        public async Task<List<User>> Register(string _username, string _firstName, string _lastName, string _password, string _email, string _city, string _phoneNumber) =>
           await Get<User>($"api/Register/_register/{_username}/{_firstName}/{_lastName}/{_password}/{_email}/{_city}/{_phoneNumber}");
    }

}

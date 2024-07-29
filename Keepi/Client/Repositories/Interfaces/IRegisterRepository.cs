using Keepi.Shared;
using Microsoft.AspNetCore.Mvc;
using static Keepi.Client.Pages.RegisterPage;

namespace Keepi.Client.Repositories.Interfaces
{
    public interface IRegisterRepository
    {
        //Task<object> Register(RegistrationModel user);

        //Task<List<User>> Register(RegistrationModel user);

        Task<List<User>> Register(string _username, string _firstName, string _lastName, string _password, string _email, string _city, string _phoneNumber);
    }

}

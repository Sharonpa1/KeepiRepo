using Microsoft.AspNetCore.Mvc;
using static Keepi.Client.Pages.RegisterPage;

namespace Keepi.Client.Repositories.Interfaces
{
    public interface IRegisterRepository
    {
        Task<object> Register(RegistrationModel user);

        //Task<List<bool>> Test();
    }

}

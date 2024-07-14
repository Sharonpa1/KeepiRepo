using Keepi.Client.Communication;
using Keepi.Client.Repositories.Base;
using Keepi.Client.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Keepi.Client.Repositories.Implementation
{
    public class RegisterRepository : Repository, IRegisterRepository
    {
        private const string URL = "api/Register";

        public RegisterRepository(IHttpService commService) : base(commService)
        {
        }

        public async Task<List<bool>> Register() =>
             await Get<bool>(URL + System.IO.Path.AltDirectorySeparatorChar + "register");

        //public async Task<List<bool>> Test() =>
        //     await Get<bool>(URL + System.IO.Path.AltDirectorySeparatorChar + "test");
    }

}

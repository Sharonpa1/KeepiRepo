using Keepi.Client.Communication;
using Keepi.Client.Repositories.Base;
using Keepi.Client.Repositories.Interfaces;
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

        public async Task<object> Register(RegistrationModel user) =>
             await Post<RegistrationModel>(URL + System.IO.Path.AltDirectorySeparatorChar + "register", user);

        //public async Task<List<bool>> Register2(RegistrationModel product)
        //{
        //    var response = await _httpClient.PostAsJsonAsync("api/products", product);
        //    return response.IsSuccessStatusCode;
        //}

        //public async Task<List<bool>> Register() =>
        //    await Post<bool, HttpResponseContainer<bool>>(URL + System.IO.Path.AltDirectorySeparatorChar + "register", );


        //public async Task<List<bool>> Test() =>
        //     await Get<bool>(URL + System.IO.Path.AltDirectorySeparatorChar + "test");
    }

}

using Keepi.Client.Communication;
using Keepi.Client.Repositories.Base;
using Keepi.Client.Repositories.Interfaces;
using Keepi.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Keepi.Client.Repositories.Implementation
{
    public class LoginRepository : Repository, ILoginRepository
    {
        private const string URL = "api/Login";

        //public LoginRepository(IHttpService commService) : base(commService)
        //{
        //}

        public async Task<List<bool>> Test() =>
             await Get<bool>(URL + System.IO.Path.AltDirectorySeparatorChar + "test");

        //public async Task<List<User>> Login() =>
        //     await Get<User>(URL + System.IO.Path.AltDirectorySeparatorChar + "_login");

        public async Task<List<User>> Login(string email, string password) =>
            await Get<User>($"api/Login/_login/{email}/{password}");

        private readonly HttpClient _httpClient;

        public LoginRepository(HttpClient httpClient, IHttpService commService) : base(commService)
        {
            _httpClient = httpClient;
        }

        //public async Task<List<User>> LoginAsync(string email, string password)
        //{
        //    //var url = $"api/Login/login1?email={email}&password={password}";
        //    var url = $"api/Login/login1/{email}/{password}";
        //    var response = await _httpClient.GetAsync(url);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return await response.Content.ReadFromJsonAsync<List<User>>();
        //    }

        //    throw new ApplicationException($"Error fetching login data: {response.ReasonPhrase}");
        //}
    }

}

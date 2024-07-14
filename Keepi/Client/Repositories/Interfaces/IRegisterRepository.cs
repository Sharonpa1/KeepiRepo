using Microsoft.AspNetCore.Mvc;

namespace Keepi.Client.Repositories.Interfaces
{
    public interface IRegisterRepository
    {
        Task<List<bool>> Register();

        //Task<List<bool>> Test();
    }

}

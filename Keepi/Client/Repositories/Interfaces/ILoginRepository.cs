using Keepi.Shared;

namespace Keepi.Client.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        //Task<List<User>> Login();

        //Task<List<User>> Login(string email, string password);
        Task<List<User>> Login(string userName, string password);

        Task<List<bool>> Test();
    }
}

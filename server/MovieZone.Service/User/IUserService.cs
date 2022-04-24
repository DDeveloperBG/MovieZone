namespace MovieZone.Service.User
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        bool CheckIsUsernameUsed(string username);

        Task RegisterAsync(string idToken, string username);
    }
}

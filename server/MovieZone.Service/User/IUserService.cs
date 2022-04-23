namespace MovieZone.Service.User
{
    using System.Threading.Tasks;

    public interface IUserService
    {
        Task RegisterAsync(string idToken, string username);
    }
}

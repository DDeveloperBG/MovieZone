namespace MovieZone.Service.User
{
    using System.Threading.Tasks;

    using MovieZone.Persistence.Models;

    public interface IUserService
    {
        bool CheckIsUsernameUsed(string username);

        Task RegisterAsync(string idToken, string username);

        Task SeedAdminAsync(string displayName, string email, string password);

        Task<bool> ValidateIsUserAuthorizedAsync(string userIdToken);

        ApplicationUser GetUserById(string id);
    }
}

namespace MovieZone.Service.FirebaseIntegration
{
    using System.Threading.Tasks;

    using FirebaseAdmin.Auth;

    public interface IFirebaseService
    {
        Task<string> GetUserIdWithIdTokenAsync(string idToken);

        Task<UserRecord> GetUserAsync(string uid);
    }
}

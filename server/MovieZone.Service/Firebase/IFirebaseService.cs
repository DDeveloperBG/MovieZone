namespace MovieZone.Service.Firebase
{
    using System.Threading.Tasks;

    using FirebaseAdmin.Auth;

    public interface IFirebaseService
    {
        Task<string> GetUserIdWithIdTokenAsync(string idToken);

        Task<UserRecord> GetUserAsync(string uid);

        public Task<string> RegisterUserAsync(string displayName, string email, string password);

        public Task AddUserToRoleAsync(string uid, string role);

        public Task<string> GetUidByEmailAsync(string uid);
    }
}

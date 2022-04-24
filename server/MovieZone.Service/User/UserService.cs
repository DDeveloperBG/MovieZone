namespace MovieZone.Service.User
{
    using System.Linq;
    using System.Threading.Tasks;

    using MovieZone.Data.Common.Repositories;
    using MovieZone.Data.Models;
    using MovieZone.Service.FirebaseIntegration;

    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IFirebaseService firebaseService;

        public UserService(
            IRepository<ApplicationUser> userRepository,
            IFirebaseService firebaseService)
        {
            this.userRepository = userRepository;
            this.firebaseService = firebaseService;
        }

        public bool CheckIsUsernameUsed(string username)
        {
            return this.userRepository
                .AllAsNoTracking()
                .Where(x => x.UserName == username)
                .Any();
        }

        public async Task RegisterAsync(string idToken, string username)
        {
            var uid = await this.firebaseService.GetUserIdWithIdTokenAsync(idToken);

            var userData = await this.firebaseService.GetUserAsync(uid);
            if (!this.CheckIsUsernameUsed(username))
            {
                throw new System.Exception("Username is already used!");
            }

            var user = new ApplicationUser(uid)
            {
                Email = userData.Email,
                UserName = username,
            };

            await this.userRepository.AddAsync(user);
            await this.userRepository.SaveChangesAsync();
        }
    }
}

namespace MovieZone.Service.User
{
    using System.Threading.Tasks;

    using MovieZone.Data.Common.Repositories;
    using MovieZone.Data.Models;
    using MovieZone.Domain.Entities;
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

        public async Task RegisterAsync(string idToken, string username)
        {
            var uid = await this.firebaseService.GetUserIdWithIdTokenAsync(idToken);

            var userData = await this.firebaseService.GetUserAsync(uid);

            var user = new ApplicationUser
            {
                Email = userData.Email,
                UserName = username,
                FirebaseUser = new FirebaseUser(uid),
            };

            await this.userRepository.AddAsync(user);
            await this.userRepository.SaveChangesAsync();
        }
    }
}

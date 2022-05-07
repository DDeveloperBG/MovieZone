namespace MovieZone.Service.User
{
    using System.Linq;
    using System.Threading.Tasks;

    using MovieZone.Common;
    using MovieZone.Persistence.Common.Repositories;
    using MovieZone.Persistence.Models;
    using MovieZone.Service.Firebase;

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

        public async Task SeedAdminAsync(string displayName, string email, string password)
        {
            bool adminExistsInDB = this.userRepository.AllAsNoTracking().Where(x => x.Email == email).Any();
            if (adminExistsInDB)
            {
                return;
            }

            string uid = await this.firebaseService.GetUidByEmailAsync(email);
            bool userNotExistsInFirebase = uid == null;
            if (userNotExistsInFirebase)
            {
                uid = await this.firebaseService.RegisterUserAsync(displayName, email, password);

                var adminRoleName = Globals.AppRoles.AdminRoleName;
                await this.firebaseService.AddUserToRoleAsync(uid, adminRoleName);
            }

            var user = new ApplicationUser(uid)
            {
                Email = email,
                UserName = displayName,
            };

            await this.userRepository.AddAsync(user);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task<bool> ValidateIsUserAuthorizedAsync(string userIdToken)
        {
            string uid = await this.firebaseService.GetUserIdWithIdTokenAsync(userIdToken);

            return this.userRepository
                .AllAsNoTracking()
                .Where(x => x.Id == uid)
                .Any();
        }
    }
}

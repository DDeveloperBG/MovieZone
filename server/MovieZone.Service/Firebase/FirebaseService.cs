namespace MovieZone.Service.Firebase
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using FirebaseAdmin;
    using FirebaseAdmin.Auth;

    using Google.Apis.Auth.OAuth2;

    using MovieZone.Service.DTOs.Firebase;
    using MovieZone.Service.Firebase.NewtonsoftNamingStrategies;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class FirebaseService : IFirebaseService
    {
        private readonly JsonSerializerSettings configKeysJsonSerializationSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver { NamingStrategy = new ConfigKeysNamingStrategy() },
        };

        public FirebaseService(FirebaseConfigKeys configKeys)
        {
            if (FirebaseApp.DefaultInstance != null)
            {
                return;
            }

            var json = JsonConvert.SerializeObject(configKeys, this.configKeysJsonSerializationSettings);
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromJson(json),
            });
        }

        public async Task<string> GetUserIdWithIdTokenAsync(string idToken)
        {
            FirebaseToken decodedToken = await FirebaseAuth
                .DefaultInstance
                .VerifyIdTokenAsync(idToken);

            return decodedToken.Uid;
        }

        public Task<UserRecord> GetUserAsync(string uid)
        {
            return FirebaseAuth.DefaultInstance.GetUserAsync(uid);
        }

        public async Task<string> RegisterUserAsync(string displayName, string email, string password)
        {
            var userCredentials = new UserRecordArgs
            {
                DisplayName = displayName,
                Email = email,
                Password = password,
            };

            var firebaseUser = await FirebaseAuth
                .DefaultInstance
                .CreateUserAsync(userCredentials);

            return firebaseUser.Uid;
        }

        public Task AddUserToRoleAsync(string uid, string role)
        {
            var claims = new Dictionary<string, object>
            {
                { ClaimTypes.Role, role },
            };

            return FirebaseAuth
                  .DefaultInstance
                  .SetCustomUserClaimsAsync(uid, claims);
        }

        /// <summary>
        /// Gets by email user email from firebase.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>If he exists, return uid. Else null.</returns>
        public async Task<string> GetUidByEmailAsync(string email)
        {
            try
            {
                var user = await FirebaseAuth
                    .DefaultInstance
                    .GetUserByEmailAsync(email);

                return user.Uid;
            }
            catch
            {
                return null;
            }
        }
    }
}

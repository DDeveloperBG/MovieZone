namespace MovieZone.Service.Firebase
{
    using System.Threading.Tasks;

    using FirebaseAdmin;
    using FirebaseAdmin.Auth;

    using Google.Apis.Auth.OAuth2;

    using Microsoft.Extensions.Configuration;

    using MovieZone.Service.DTOs.Firebase;

    using Newtonsoft.Json;

    public class FirebaseService : IFirebaseService
    {
        public FirebaseService(IConfiguration configuration)
        {
            if (FirebaseApp.DefaultInstance != null)
            {
                return;
            }

            FirebaseConfigKeys fbconfig = new FirebaseConfigKeys();
            configuration.Bind("Firebase", fbconfig);

            var json = JsonConvert.SerializeObject(fbconfig);
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
    }
}

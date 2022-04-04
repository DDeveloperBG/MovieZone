namespace MovieZone.Service.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Options;
    using Microsoft.FeatureManagement;
    using Microsoft.IdentityModel.Tokens;
    using MovieZone.Domain.Auth;
    using MovieZone.Domain.Common;
    using MovieZone.Domain.Enum;
    using MovieZone.Domain.Settings;
    using MovieZone.Service.Contract;
    using MovieZone.Service.Exceptions;

    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailService;
        private readonly JWTSettings jwtSettings;
        private readonly IDateTimeService dateTimeService;
        private readonly IFeatureManager featureManager;

        public AccountService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService,
            IFeatureManager featureManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtSettings = jwtSettings.Value;
            this.dateTimeService = dateTimeService;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.featureManager = featureManager;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await this.userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.Email}.");
            }

            var result = await this.signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }

            if (!user.EmailConfirmed)
            {
                throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            }

            JwtSecurityToken jwtSecurityToken = await this.GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await this.userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            var refreshToken = this.GenerateRefreshToken(ipAddress);
            response.RefreshToken = refreshToken.Token;
            return new Response<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
        }

        public async Task<Response<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithSameUserName = await this.userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                throw new ApiException($"Username '{request.UserName}' is already taken.");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
            };
            var userWithSameEmail = await this.userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await this.userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    var verificationUri = await this.SendVerificationEmail(user, origin);

                    if (await this.featureManager.IsEnabledAsync(nameof(FeatureManagement.EnableEmailService)))
                    {
                        await this.emailService.SendEmailAsync(new MailRequest() { From = "amit.naik8103@gmail.com", ToEmail = user.Email, Body = $"Please confirm your account by visiting this URL {verificationUri}", Subject = "Confirm Registration" });
                    }

                    return new Response<string>(user.Id, message: $"User Registered. Please confirm your account by visiting this URL {verificationUri}");
                }
                else
                {
                    throw new ApiException($"{result.Errors.ToList()[0].Description}");
                }
            }
            else
            {
                throw new ApiException($"Email {request.Email} is already registered.");
            }
        }

        public async Task<Response<string>> ConfirmEmailAsync(string userId, string code)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await this.userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return new Response<string>(user.Id, message: $"Account Confirmed for {user.Email}. You can now use the /api/Account/authenticate endpoint.");
            }
            else
            {
                throw new ApiException($"An error occured while confirming {user.Email}.");
            }
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = await this.userManager.FindByEmailAsync(model.Email);

            // always return ok response to prevent email enumeration
            if (account == null)
            {
                return;
            }

            var code = await this.userManager.GeneratePasswordResetTokenAsync(account);
            var route = "api/account/reset-password/";
            var enpointUri = new Uri(string.Concat($"{origin}/", route));
            var emailRequest = new MailRequest()
            {
                Body = $"You reset token is - {code}",
                ToEmail = model.Email,
                Subject = "Reset Password",
            };
            await this.emailService.SendEmailAsync(emailRequest);
        }

        public async Task<Response<string>> ResetPassword(ResetPasswordRequest model)
        {
            var account = await this.userManager.FindByEmailAsync(model.Email);
            if (account == null)
            {
                throw new ApiException($"No Accounts Registered with {model.Email}.");
            }

            var result = await this.userManager.ResetPasswordAsync(account, model.Token, model.Password);
            if (result.Succeeded)
            {
                return new Response<string>(model.Email, message: $"Password Resetted.");
            }
            else
            {
                throw new ApiException($"Error occured while reseting the password.");
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = this.RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress,
            };
        }

        private async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "api/account/confirm-email/";
            var enpointUri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(enpointUri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "code", code);

            // Email Service Call Here
            return verificationUri;
        }

        private string RandomTokenString()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(40);

            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", string.Empty);
        }

        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            var userClaims = await this.userManager.GetClaimsAsync(user);
            var roles = await this.userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("ip", ipAddress),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this.jwtSettings.Issuer,
                audience: this.jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(this.jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}

namespace BreakPoint.Services.Services
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using BreakPoint.Data.EntityModels;
    using BreakPoint.Data.Interfaces;
    using BreakPoint.Models.BindingModels.Account;
    using BreakPoint.Services.Interfaces;
    using BreakPoint.Services.Utilities;

    public class AccountService : Service, IAccountService
    {
        public AccountService(IBreakPointDbContext context)
            : base(context)
        {
        }

        public string CreateNewUserAccount(RegisterUserBindingModel bm)
        {
            try
            {
                // returns byte[][] array of 2 elements(hashed password and salt)
                var passwordHashAndSalt = this.GenerateSaltedHash(bm.Password);

                User newUser = new User();
                newUser.Name = bm.Name;
                newUser.Email = bm.Email;
                newUser.PasswordHash = Convert.ToBase64String(passwordHashAndSalt[0]);
                newUser.Salt = Convert.ToBase64String(passwordHashAndSalt[1]);

                this.Context.Users.Add(newUser);
                this.Context.SaveChanges();
            }
            catch (Exception)
            {
                return "";
            }

            LoginUserBindingModel loginBm = new LoginUserBindingModel()
            {
                Email = bm.Email,
                Password = bm.Password
            };

            var tokenBearer = LoginUser(loginBm);


            return tokenBearer;
        }

        private byte[][] GenerateSaltedHash(string userPassword)
        {  
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] password = userPassword.Select(character => (byte)character).ToArray();
                var salt = this.GenerateSalt();
                var saltedPassword = new byte[password.Length + salt.Length];

                password.CopyTo(saltedPassword, 0);
                salt.CopyTo(saltedPassword, password.Length);

                var saltedPasswordHash = sha256.ComputeHash(saltedPassword);

                // return passwordHash and salt in double array to store it in User
                var result = new byte[][] { saltedPasswordHash, salt };

                return result;
            }
        }

        private byte[] GenerateSalt()
        {
            using (RandomNumberGenerator random = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[32];
                random.GetNonZeroBytes(salt);
                return salt;
            }
        }

        public string LoginUser(LoginUserBindingModel bm)
        {
            string tokenBearer = string.Empty;
            try
            {
                var user = this.Context
                .Users
                .Where(u => u.Email == bm.Email)
                .First();

                var passwordHash = GenerateHashOfPassword(bm.Password, user.Salt);
                if (user.PasswordHash == passwordHash)
                {
                    tokenBearer = GenerateToken();
                    TokenManager newToken = new TokenManager()
                    {
                        
                        Value = tokenBearer,
                        CreatedOn = DateTime.Now,
                    };
                    newToken.User = user;

                    this.Context.Tokens.Add(newToken);
                    this.Context.SaveChanges();
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }

            return tokenBearer;
        }

        private string GenerateHashOfPassword(string password, string salt)
        {
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] passwordInBytes = password.Select(character => (byte)character).ToArray();
                byte[] saltInBytes = Convert.FromBase64String(salt);
                var saltedPassword = new byte[passwordInBytes.Length + saltInBytes.Length];

                passwordInBytes.CopyTo(saltedPassword, 0);
                saltInBytes.CopyTo(saltedPassword, passwordInBytes.Length);
                var saltedPasswordHash = sha256.ComputeHash(saltedPassword);

                return Convert.ToBase64String(saltedPasswordHash);
            }
        }

        private string GenerateToken()
        {
            return Guid.NewGuid().ToString() + TokenGenerator.Generate(30);
        }
    }
}

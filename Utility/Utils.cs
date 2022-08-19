using Metrosoft.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Metrosoft.Utility
{
    public class Utils
    {
        public IConfiguration _Configuration;
        private readonly AppSettings _appSettings;


        public Utils(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }



        public void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var Hmac = new HMACSHA512())
            {
                PasswordSalt = Hmac.Key;
                PasswordHash = Hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] PassowrdHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PassowrdHash);
            }
        }

        public string CreateToken(Users model)
        {

            //simplify the user role
            var utcNow = DateTime.UtcNow;
            List<Claim> claims = new List<Claim>();
            claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,model.Email),
                new Claim(ClaimTypes.Name, model.Email)


            }.ToList();


            //create a token for the user
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("09iuhgfdswe456yhnkio");


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1000),
                Issuer = "http://localhost/12345",
                Audience = "ProductApi.com",
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokens = tokenHandler.WriteToken(token);

            return tokens;
        }

    }

    public class AppSettings
    {
        public string Secret { get; set; }
    }
}

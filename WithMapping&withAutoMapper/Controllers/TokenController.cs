using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIMMwithoutJunctionModel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DocPatientContext _context;
        private readonly SymmetricSecurityKey _key;

        public TokenController(DocPatientContext context, IConfiguration config)
        {
            _context = context;
            _key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(config["Key"]));

        }
        [HttpPost]
        public IActionResult GenerateToken(User luser)
        {
            string token = string.Empty;

            //  var user = _context.Users.FirstOrDefault(u => u.email == luser.email && u.password == luser.password && u.role == luser.role);
            var user = ValidateUser(luser.email, luser.password);
            var claims = new List<Claim>
               {
                   new Claim(JwtRegisteredClaimNames.NameId,user.userName!),
                   new Claim(JwtRegisteredClaimNames.Email,user.email),
               };
                // only add role if available
                if (!string.IsNullOrEmpty(user.role))
                    claims.Add(new Claim(ClaimTypes.Role, user.role));

                var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    SigningCredentials = cred,
                    Subject = new ClaimsIdentity(claims),//user data
                    Expires = DateTime.Now.AddMinutes(60) //expiration time
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var createToken = tokenHandler.CreateToken(tokenDescription);
                token = tokenHandler.WriteToken(createToken);
                return Ok(new { token, role = user.role });
            
          
        }
        private User ValidateUser(string email, string password)
        {
            var users = _context.Users.ToList();
            var user = users.FirstOrDefault(u => u.email == email && u.password == password);
            return user;
           
        }
    }
}

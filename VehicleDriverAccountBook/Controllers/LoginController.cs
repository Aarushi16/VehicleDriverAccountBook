using AuthorizationAPI.Context;
using AuthorizationAPI.Model;
using AuthorizationAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AuthorizationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;
        private readonly GenerateToken _generateToken;

        public LoginController(UserDbContext userDbContext, GenerateToken generateToken)
        {
            _userDbContext = userDbContext;
            _generateToken = generateToken;
        }

        [HttpPost]
        public IActionResult SignIn(User user)
        {
            SignInResponse response = new SignInResponse();
            response.Token = string.Empty;

            var result = _userDbContext.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
            if (result != null)
            {
                response.IsSuccess = true;
                response.Token = _generateToken.GenerateJsonWebToken();
            }

            return Ok(response);
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            SignUpResponse response = new SignUpResponse();
            _userDbContext.Users.Add(user);
            var result = _userDbContext.SaveChanges();
            if (result == 1)
                response.IsSuccess = true;

            return Ok(response);
        }
    }
}

using LibraryManagementSystem.Model;
using LibraryManagementSystem.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        //// GET: api/<loginsController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}


        //Get Configurations from appSettings - SecretKey

        private readonly IConfiguration _config;
        private readonly ILoginRepository _loginRepository;

        //DI
        public LoginsController(IConfiguration config, ILoginRepository loginRepository)
        {
            _config = config;
            _loginRepository = loginRepository;
        }
        #region --validate username and password
        //GET api/<Logins/username/password
        [HttpGet("{usernames}/{passwords}")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginCredentials(string usernames, string passwords)
        {
            //Variable for tracking unauthorised
            IActionResult response = Unauthorized(); //401
            LibLogin validUser = null;

            //1 - Authenticate the user by passing username and password
            validUser = await _loginRepository.ValidateUser(usernames, passwords);

            //2 - Generate JWT Token

            if (validUser != null)
            {
                //Custom Method for generating token
                var tokenString = GenerateJWTToken(validUser);

                response = Ok(new
                {
                    uName = validUser.Username,
                    memberId = validUser.MemberId,
                    token = tokenString
                });
            }
            return response;


        }
        #endregion

        #region Generate JWT Token
        private string GenerateJWTToken(LibLogin validUser)
        {
            //1 - Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            //2 - Algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //3 - JWT
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
                );

            //4 - Writing Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}

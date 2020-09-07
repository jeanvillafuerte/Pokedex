using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pokedex.Api.Security;
using Pokedex.Repository;
using Pokedex.Repository.Persistence;

namespace Pokedex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _settings;
        private readonly IRepository<Users> _userRepository;

        public LoginController(IConfiguration configuration, JwtSettings settings, IRepository<Users> userRepository)
        {
            _configuration = configuration;
            _settings = settings;
            _userRepository = userRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AppUserViewModel userVM)
        {
            var manager = new SecurityManager( _userRepository, _configuration, _settings);

            var auth = manager.ValidateUser(userVM);

            if (auth.IsAuthenticated)
            {
                return Ok(auth);
            }
            else
            {
                return BadRequest(new { auth = false });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using owasp_acccesscontrol.Application.Interfaces;
using owasp_accesscontrol.Domain.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace owasp_accesscontrol.WebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
       
        [HttpGet]        
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(userService.FindAll());
        }

        [HttpPost("login")]        
        public ActionResult<string> Login(Login login)
        {
            if (login == null)
            {
                return BadRequest();
            }

            if(string.IsNullOrWhiteSpace(login.UserName) || string.IsNullOrWhiteSpace(login.Password))
            {
                return BadRequest();
            }

            try
            {
                return Ok(userService.Login(login.UserName, login.Password));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(401);
            }
            
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            int id = userService.Create(user);

            return Ok(new User() { UserId = id});
        }
    }
}


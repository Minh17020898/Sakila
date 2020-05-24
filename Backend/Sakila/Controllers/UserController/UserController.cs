using Microsoft.AspNetCore.Mvc;
using Sakila.Services;
using System;

namespace Sakila.Controllers.UserController
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{userId}")]
        public string Get(Guid userId)
        {
            return userService.GetUsername(userId);
        }

        [HttpPost]
        public string Login([FromBody] UserDTO userDTO)
        {
            return userService.Login(userDTO.Username, userDTO.Password);
        }

        [HttpPost]
        public string Register([FromBody] UserDTO userDTO)
        {
            return userService.Register(userDTO.Username, userDTO.Password);
        }
    }
}
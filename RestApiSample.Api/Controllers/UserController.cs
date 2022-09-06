using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApiSample.Api.Data.DTOs;
using RestApiSample.Api.Data.Models;
using RestApiSample.Api.Repositories.Interfaces;

namespace RestApiSample.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UserController : ControllerBase
    {
        #region Ctor

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #endregion

        /// <summary>
        ///     Authentication user by username and password
        /// </summary>
        /// <param name="authenticateUserDto">Login user dto</param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto? authenticateUserDto)
        {
            if (authenticateUserDto == null) return BadRequest();

            var result = await _userRepository.AuthenticateUser(authenticateUserDto);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Register new user by default user role
        /// </summary>
        /// <param name="authenticateUserDto">Register user Dto</param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto? authenticateUserDto)
        {
            if (authenticateUserDto == null) return BadRequest();

            if (await _userRepository.IsUserExistsByUserName(authenticateUserDto.UserName!))
            {
                return Conflict("The username entered already exists.");
            }

            await _userRepository.RegisterUser(authenticateUserDto);

            return Ok();
        }
    }
}

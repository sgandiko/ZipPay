using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ZipPay.Domain.dto;
using ZipPay.Domain.Interface.Service;

namespace ZipPay.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const string DuplicateEmailRequestMessage = "{0} already exists in the system.";
        private const string UserNotFound = "user id with {0} was not found.";
        private const string UsersNotFound = "No users found.";
        private const string UserSaveFailRequestMessage = "{0} unable to save the record.";

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var getUserListDto = await _userService.GetUsersAsync();
                if (getUserListDto.UsersNotFound)
                    return BadRequest(string.Format(UsersNotFound));
                return Ok(getUserListDto.Users);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {
                var user = await _userService.GetUserAsync(id);
                if (user == null || user.UserNotFound)
                    return BadRequest(string.Format(UserNotFound, id));
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                await _userService.CreateUserAsync(userDto);

                if (userDto.EmailAlreadyExists)
                    return BadRequest(string.Format(DuplicateEmailRequestMessage,userDto.Email));
                if (!userDto.Saved)
                    return BadRequest(string.Format(UserSaveFailRequestMessage,userDto.Email));

                return Created(new Uri(Request.GetDisplayUrl() + "/" + userDto.Id), userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
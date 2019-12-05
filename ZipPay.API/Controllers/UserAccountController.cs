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
    public class UserAccountController : ControllerBase
    {
        private const string UserNotFound = "user account id with {0} was not found.";
        private const string UserAccountsNotFound = "User Accounts was not found.";
        private const string UserAccountFound = "User account for the user id {0} is already exists.";
        private const string UserAccountSaveFailRequestMessage = "User Id with {0} unable to save the record.";

        private const string MinSavingsAmountNotMetRequestMessage = "User savings amount didn't met the requirement to create the User Account. Montly Salary minus expenses should be grater than 1000.";
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var getUserAccountListDto = await _userAccountService.GetUserAccountsAsync();
                if (getUserAccountListDto.UserAccountsNotFound)
                    return NotFound(string.Format(UserAccountsNotFound));
                return Ok(getUserAccountListDto.UserAccounts);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserAccountDto>> Get(int id)
        {
            try
            {
                var getUserAccount = await _userAccountService.GetUserAccountAsync(id);
                if (getUserAccount.UserAccountNotFound)
                    return NotFound(string.Format(UserNotFound, id));
                return Ok(getUserAccount);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]CreateUserAccountDto createUserAccountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var userAccount = await _userAccountService.CreateUserAccountAsync(createUserAccountDto);

                if (createUserAccountDto.UserAccountExists)
                    return BadRequest(string.Format(UserAccountFound, createUserAccountDto.UserId));
                if (createUserAccountDto.UserNotFound)
                    return NotFound(string.Format(UserNotFound, createUserAccountDto.UserId));
                if (createUserAccountDto.MinSavingsAmountNotMet)
                    return BadRequest(string.Format(MinSavingsAmountNotMetRequestMessage));
                if (!createUserAccountDto.Created)
                    return BadRequest(string.Format(UserAccountSaveFailRequestMessage, createUserAccountDto.UserId));

                return Created(new Uri(Request.GetDisplayUrl() + "/" + userAccount.Id), userAccount);
            }
            catch (Exception ex)
            {
                return BadRequest(string.Format(UserAccountSaveFailRequestMessage, createUserAccountDto.UserId));
            }

        }
    }
}
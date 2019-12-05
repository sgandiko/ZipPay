using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZipPay.Domain.dto;
using ZipPay.Domain.Entity;
using ZipPay.Domain.Interface.Repository.UserAccountRepo;
using ZipPay.Domain.Interface.Repository.UserRepo;
using ZipPay.Domain.Interface.Service;

namespace ZipPay.Infrastructure.service
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private const decimal MinSavingsAmount = 1000;
        private const decimal MaxLoanEligibleAmount = 1000;
        public UserAccountService(IUserAccountRepository userAccountRepository, IMapper mapper, IUserRepository userRepository)
        {
            _userAccountRepository = userAccountRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<UserAccountDto> CreateUserAccountAsync(CreateUserAccountDto createUserAccountDto)
        {
            try
            {
                // check user account is already created
                if (IsUserAccountAlreadyExists(createUserAccountDto.UserId))
                {
                    createUserAccountDto.UserAccountExists = true;
                    return null;
                }
                // Get user by id from User Table
                User user = GetUserById(createUserAccountDto.UserId);
                if(user == null)
                {
                    createUserAccountDto.UserNotFound = true;
                    return null;
                }

                // validate monthly savings should be grater the Min amount
                if ((user.Salary - user.Expenses) < MinSavingsAmount)
                {
                    createUserAccountDto.MinSavingsAmountNotMet = true;
                }
                // create the user account object
                var userAccountDto = new UserAccountDto()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Savings = (user.Salary - user.Expenses),
                    MaxAmountEligibleForLoan = MaxLoanEligibleAmount,
                    UserId = user.Id
                };
                var userAccount = _mapper.Map<UserAccount>(userAccountDto);
                // create user account
                await _userAccountRepository.InsertAsync(userAccount);
                await _userAccountRepository.SaveAsync();
                createUserAccountDto.Created = true;
                userAccountDto.Id = userAccount.Id;
                return userAccountDto;
            }
            catch (Exception)
            {
                // Error handle
                createUserAccountDto.Created = false;
                return null;
            }
        }

       


        // Get User Account by Id
        public async Task<GetUserAccountDto> GetUserAccountAsync(int id)
        {
            var getUserAccountDto = new GetUserAccountDto();
            try
            {
                var user = await _userAccountRepository.GetByIdAsync(id);
                if (user == null)
                {
                    getUserAccountDto.UserAccountNotFound = true;
                    return getUserAccountDto;
                }
                return _mapper.Map<GetUserAccountDto>(user);
            }
            catch (Exception)
            {
                getUserAccountDto.UserAccountNotFound = true;
                return getUserAccountDto;
            }
        }


        // Get all User Accounts
        public async Task<GetUserAccountListDto> GetUserAccountsAsync()
        {
            var getUserAccountListDto = new GetUserAccountListDto();
            try
            {
                var userAccounts = await _userAccountRepository.GetAllAsync();
                if(userAccounts == null || userAccounts.Count() == 0)
                {
                    getUserAccountListDto.UserAccountsNotFound = true;
                    return getUserAccountListDto;
                }
                getUserAccountListDto.UserAccounts = userAccounts.Select(x => _mapper.Map<UserAccountDto>(x)).ToList();
                return getUserAccountListDto;
            }
            catch (Exception)
            {

                getUserAccountListDto.UserAccountsNotFound = true;
                return getUserAccountListDto;
            }
            
        }

        #region private methods
        private User GetUserById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        private bool IsUserAccountAlreadyExists(int userId)
        {
            return _userAccountRepository.GetAll(filter: x => x.UserId == userId).Any();
        }
        #endregion

    }
}

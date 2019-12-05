using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipPay.Domain.dto;
using ZipPay.Domain.Entity;
using ZipPay.Domain.Interface.Repository.UserRepo;
using ZipPay.Domain.Interface.Service;

namespace ZipPay.Infrastructure.service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        #region Constructors
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        #endregion

        #region public methods
        public async Task<CreateUserDto> CreateUserAsync(CreateUserDto userDto)
        {
            try
            {
                if (IsUserEmailAlreadyExists(userDto.Email))
                {
                    userDto.EmailAlreadyExists = true;
                    return userDto;
                }
                var user = _mapper.Map<User>(userDto);
                await _userRepository.InsertAsync(user);
                await _userRepository.SaveAsync();
                userDto.Saved = true;
                userDto.Id = user.Id;
                return userDto;
            }
            catch (Exception)
            {
                userDto.Saved = false;
                return userDto;
            }

        }
        public async Task<GetUserDto> GetUserAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    return new GetUserDto() { UserNotFound = true };
                return _mapper.Map<GetUserDto>(user);
            }
            catch (Exception)
            {

                return new GetUserDto() { UserNotFound = true };
            }
            
        }

        public async Task<GetUserListDto> GetUsersAsync()
        {
            var getUserListDto = new GetUserListDto();
            try
            {
                
                var users = await _userRepository.GetAllAsync();
                if(users == null && users.Count() == 0)
                {
                    getUserListDto.UsersNotFound = true;
                    return getUserListDto;
                }
                getUserListDto.Users = users.Select(x => _mapper.Map<UserDto>(x)).ToList();

                return getUserListDto;
            }
            catch (Exception)
            {
                getUserListDto.UsersNotFound = true;
                return getUserListDto;
            }
            
        }
        #endregion



        #region Private Methods
        private bool IsUserEmailAlreadyExists(string email)
        {
            return _userRepository.GetAll(filter: x => x.Email.ToLower() == email.ToLower()).Any();
        }
        #endregion
    }
}

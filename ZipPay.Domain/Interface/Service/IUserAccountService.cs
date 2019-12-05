using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZipPay.Domain.dto;
using ZipPay.Domain.Entity;

namespace ZipPay.Domain.Interface.Service
{
    public interface IUserAccountService
    {
        Task<UserAccountDto> CreateUserAccountAsync(CreateUserAccountDto createUserAccountDto);
        Task<GetUserAccountListDto> GetUserAccountsAsync();
        Task<GetUserAccountDto> GetUserAccountAsync(int id);
    }
}

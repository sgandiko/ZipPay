using System;
using System.Collections.Generic;
using System.Text;
using ZipPay.Domain.Entity;
using ZipPay.Domain.Interface.Repository.UserAccountRepo;
using ZipPay.Domain.Interface.Repository.UserRepo;
using ZipPay.Infrastructure.Data;
using ZipPay.Infrastructure.Persistence.Repository.Common;

namespace ZipPay.Infrastructure.Persistence.Repository.UserAccountRepo
{
    public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
    {
        //private readonly ZipPayDataContext _dbContext;
        public UserAccountRepository(ZipPayDataContext dbContext) : base(dbContext)
        {
            
        }
    }
}

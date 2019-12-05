using System;
using System.Collections.Generic;
using System.Text;
using ZipPay.Domain.Entity;
using ZipPay.Domain.Interface.Repository.UserRepo;
using ZipPay.Infrastructure.Data;
using ZipPay.Infrastructure.Persistence.Repository.Common;

namespace ZipPay.Infrastructure.Persistence.Repository.UserRepo
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        //private readonly ZipPayDataContext _dbContext;
        public UserRepository(ZipPayDataContext dbContext) : base(dbContext)
        {

        }
    }
}

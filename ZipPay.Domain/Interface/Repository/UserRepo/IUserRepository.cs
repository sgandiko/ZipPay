﻿using System;
using System.Collections.Generic;
using System.Text;
using ZipPay.Domain.Entity;
using ZipPay.Domain.Interface.Repository.Common;

namespace ZipPay.Domain.Interface.Repository.UserRepo
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}

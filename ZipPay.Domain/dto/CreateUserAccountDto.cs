using Newtonsoft.Json;
using System.Collections.Generic;

namespace ZipPay.Domain.dto
{
    public partial class CreateUserAccountDto
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public bool Created { get; set; } = false;
        [JsonIgnore]
        public bool UserNotFound { get; set; } = false;
        [JsonIgnore]
        public bool MinSavingsAmountNotMet { get; set; } = false;
        [JsonIgnore]
        public bool UserAccountExists { get; set; } = false;
    }

    public partial class GetUserAccountDto : UserAccountDto
    {
        [JsonIgnore]
        public bool UserAccountNotFound { get; set; } = false;
    }

    public partial class GetUserAccountListDto : UserAccountDto
    {
        public GetUserAccountListDto()
        {
            UserAccounts = new HashSet<UserAccountDto>();
        }
        public virtual ICollection<UserAccountDto> UserAccounts { get; set; }
        public bool UserAccountsNotFound { get; set; } = false;
    }
}

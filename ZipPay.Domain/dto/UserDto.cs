using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ZipPay.Domain.dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "EmailAddress length can't be more than 256.")]
        public string Email { get; set; }
        [Required]
        [Range(0.0,Double.MaxValue)]
        public decimal Salary { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Expenses { get; set; }

        
        
    }

    public partial class CreateUserDto : UserDto
    {
        [JsonIgnore]
        public bool Saved { get; set; } = false;
        [JsonIgnore]
        public bool EmailAlreadyExists { get; set; } = false;
    }

    public partial class GetUserDto : UserDto
    {
        [JsonIgnore]
        public bool UserNotFound { get; set; } = false;
    }

    public partial class GetUserListDto 
    {
        public GetUserListDto()
        {
            Users = new HashSet<UserDto>();
        }
        public virtual ICollection<UserDto> Users { get; set; }
        public bool UsersNotFound { get; set; } = false;
    }
}

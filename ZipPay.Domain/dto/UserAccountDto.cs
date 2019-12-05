using System;
using System.ComponentModel.DataAnnotations;

namespace ZipPay.Domain.dto
{
    public class UserAccountDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name length can't be more than 50.")]
        public string Name { get; set; }
        [Required]
        [StringLength(256, ErrorMessage = "EmailAddress length can't be more than 256.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        public decimal Savings { get; set; }
        [Range(0.0, Double.MaxValue)]
        public decimal MaxAmountEligibleForLoan { get; set; }
        public int UserId { get; set; }

        public virtual UserDto User { get; set; }
    }
}

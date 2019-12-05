using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipPay.Domain.Entity
{
    [Table("UserAccounts")]
    public partial class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Savings { get; set; }
        [Range(0.0, Double.MaxValue)]
        public decimal MaxAmountEligibleForLoan { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }

}

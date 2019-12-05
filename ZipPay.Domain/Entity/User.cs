using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ZipPay.Domain.Entity
{
    [Table("Users")]
    public partial class User
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
        public decimal Salary { get; set; }
        [Required]
        [Range(0.0, Double.MaxValue)]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Expenses { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }

}

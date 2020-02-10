using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("userId", TypeName = "int(10)")]
        [Required]
        public int userId { get; set; }

        [Column("password", TypeName = "varchar(100)")]
        [MaxLength(50)]
        [Required]
        public string password { get; set; }
    }
}
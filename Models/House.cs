using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApi.Models
{
    [Table("houses")]
    public class House
    {
        [Key]
        [Column("houseId", TypeName = "int(10)")]
        [Required]
        public int houseId { get; set; }

        [Column("houseRow", TypeName = "int(10)")]
        [Required]
        public int houseRow { get; set; }

        [Column("houseColumn", TypeName = "int(10)")]
        [Required]
        public int houseColumn { get; set; }
    }
}
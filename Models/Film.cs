using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApi.Models
{
    [Table("films")]
    public class Film
    {
        [Key]
        [Column("filmId", TypeName = "int(10)")]
        [Required]
        public int filmId { get; set; }

        [Column("filmName", TypeName = "varchar(100)")]
        [MaxLength(50)]
        [Required]
        public string filmName { get; set; }

        [Column("duration", TypeName = "int(5)")]
        [Required]
        public int duration { get; set; }

        [Column("category", TypeName = "varchar(50)")]
        [MaxLength(50)]
        [Required]
        public string category { get; set; }

        [Column("language", TypeName = "varchar(10)")]
        [MaxLength(10)]
        [Required]
        public string language { get; set; }

        [Column("director", TypeName = "varchar(50)")]
        [MaxLength(50)]
        [Required]
        public string director { get; set; }

        [Column("description", TypeName = "varchar(200)")]
        [MaxLength(200)]
        [Required]
        public string description { get; set; }

        public List<Comment> comments { get; set; }
    }
}
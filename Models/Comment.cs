using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApi.Models
{
    [Table("comments")]
    public class Comment
    {
        [Key]
        [Column("commentId", TypeName = "int(10)")]
        [Required]
        public int commentId { get; set; }

        [Column("userId", TypeName = "int(10)")]
        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User author { get; set; }

        [Column("filmId", TypeName = "int(10)")]
        [Required]
        public int filmId;
        [ForeignKey("filmId")]
        public virtual Film film { get; set; }

        [Column("comment", TypeName = "varchar(200)")]
        [MaxLength(200)]
        [Required]
        public string comment { get; set; }
    }
}
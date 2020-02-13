using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApi.Models
{
    [Table("broadcasts")]
    public class Broadcast
    {
        [Key]
        [Column("broadcastId", TypeName = "int(10)")]
        [Required]
        public int broadcastId { get; set; }

        [Column("dates", TypeName = "datetime")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime dates { get; set; }

        [Column("filmId", TypeName = "int(10)")]
        [Required]
        public int filmId { get; set; }

        [ForeignKey("filmId")]
        public virtual FilmAbstract filmAbstract { get; set; }

        [Column("houseId", TypeName = "int(10)")]
        [Required]
        public int houseId { get; set; }

        [ForeignKey("houseId")]
        public virtual House house { get; set; }

        [Column("day", TypeName = "varchar(10)")]
        [Required]
        public string day;

        public virtual ICollection<Ticket> tickets { get; set; }
    }
}
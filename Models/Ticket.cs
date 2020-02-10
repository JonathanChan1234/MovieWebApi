using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetApi.Models
{
    public enum TicketType
    {
        student,
        adult
    }

    [Table("tickets")]
    public class Ticket
    {
        [Key]
        [Column("ticketId", TypeName = "int(10)")]
        [Required]
        public int ticketId { get; set; }

        [Column("seatNo", TypeName = "int(5)")]
        [Required]
        public int seatNo { get; set; }

        [Column("seatName", TypeName = "varchar(10)")]
        [Required]
        public string seatName { get; set; }

        [Column("broadcastId", TypeName = "int(10)")]
        [Required]
        public int broadcastId { get; set; }

        [ForeignKey("broadcastId")]
        public Broadcast broadcast;

        [Column("valid", TypeName = "boolean")]
        [Required]
        public bool valid { get; set; }

        [Column("userId", TypeName = "int(10)")]
        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public User user { get; set; }

        [Column("ticketType", TypeName = "enum('student', 'adult')")]
        [Required]
        public TicketType ticketType { get; set; }

        [Column("ticketFee", TypeName = "int(5)")]
        [Required]
        public int ticketFee { get; set; }

        [Column("lastUpdatedAt", TypeName = "timestamp")]
        [Timestamp]
        public DateTime lastUpdatedAt { get; set; }
    }
}
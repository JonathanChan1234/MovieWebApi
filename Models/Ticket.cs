using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using NetApi.Validators;

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
        public virtual Broadcast broadcast { get; set; }

        [Column("valid", TypeName = "boolean")]
        [Required]
        public bool valid { get; set; }

        [Column("userId", TypeName = "int(10)")]
        [Required]
        public int userId { get; set; }
        [ForeignKey("userId")]
        public virtual User user { get; set; }

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

    public class TicketValidator : AbstractValidator<Ticket>
    {
        public TicketValidator()
        {
            RuleFor(ticket => ticket.seatNo)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .WithMessage("Missing/Invalid seat no");
            RuleFor(ticket => ticket.seatName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Missing/Invalid seat name");
            RuleFor(ticket => ticket.broadcastId)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .WithMessage("Missing/Invalid broadcast id");
            RuleFor(ticket => ticket.valid)
                .NotNull()
                .NotEmpty()
                .IsBoolean()
                .WithMessage("Missing/Invalid ticket valid");
            RuleFor(ticket => ticket.userId)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .WithMessage("Missing/Invalid user id");
            RuleFor(ticket => ticket.ticketType.ToString())
                .NotNull()
                .NotEmpty()
                .IsEnumName(typeof(TicketType), caseSensitive: false)
                .WithMessage("Missing/Invalid ticket type");
            RuleFor(ticket => ticket.ticketFee)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .WithMessage("Missing/Invalid ticket type");
        }
    }
}
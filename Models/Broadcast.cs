using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using NetApi.Validators;

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
        public virtual Film film { get; set; }

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

    public class BroadcastValidator : AbstractValidator<Broadcast>
    {
        public BroadcastValidator()
        {
            RuleFor(broadcast => broadcast.dates)
                .NotNull()
                .NotEmpty()
                .IsDateTime()
                .WithMessage("Missing/Invalid broadcast date");
            RuleFor(broadcast => broadcast.filmId)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .WithMessage("Missing/Invalid Film id");
            RuleFor(broadcast => broadcast.houseId)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .WithMessage("Missing/Invalid broadcast house id");
        }
    }
}
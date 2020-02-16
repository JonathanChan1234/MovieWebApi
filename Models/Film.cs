using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using NetApi.Validators;
using Newtonsoft.Json;

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
        [Required]
        public string filmName { get; set; }

        [Column("duration", TypeName = "int(5)")]
        [Required]
        public int duration { get; set; }

        [Column("category", TypeName = "varchar(50)")]
        [Required]
        public string category { get; set; }

        [Column("language", TypeName = "varchar(10)")]
        [Required]
        public string language { get; set; }

        [Column("director", TypeName = "varchar(50)")]
        [Required]
        public string director { get; set; }

        [Column("description", TypeName = "varchar(200)")]
        [Required]
        public string description { get; set; }
        public virtual ICollection<Comment> comments { get; set; }
        public virtual ICollection<Broadcast> broadcasts { get; set; }
    }

    public class FilmAbstract
    {
        [Key]
        [Column("filmId", TypeName = "int(10)")]
        [Required]
        public int filmId { get; set; }

        [Column("filmName", TypeName = "varchar(100)")]
        [Required]
        public string filmName { get; set; }

        [Column("duration", TypeName = "int(5)")]
        [Required]
        public int duration { get; set; }

        [Column("language", TypeName = "varchar(10)")]
        [Required]
        public string language { get; set; }

        [Column("director", TypeName = "varchar(50)")]
        [Required]
        public string director { get; set; }

        [Column("category", TypeName = "varchar(50)")]
        [Required]
        public string category { get; set; }

        [Column("description", TypeName = "varchar(200)")]
        [Required]
        public string description { get; set; }

        [JsonIgnore]
        [ForeignKey("filmId")]
        public virtual Film film { get; set; }
    }

    public class FilmValidator : AbstractValidator<Film>
    {
        public FilmValidator()
        {
            RuleFor(film => film.director)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("The length of film name have to be within 100 character and cannot be empty");
            RuleFor(film => film.duration)
                .NotNull()
                .NotEmpty()
                .IsInteger()
                .LessThan(1000)
                .WithMessage("The duration of the film must be less than 1000 mins and cannot be empty");
            RuleFor(film => film.category)
                .NotEmpty()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(film => film.language)
                .NotEmpty()
                .NotEmpty()
                .MaximumLength(10);
            RuleFor(film => film.director)
                .NotEmpty()
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(film => film.description)
                .NotEmpty()
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}
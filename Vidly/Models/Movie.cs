using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, MMMM d, yyyy}") ]
        [Required]
        [Display(Name ="Release Date")]
        public DateTime ReleaseDate { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, MMMM d, yyyy}") ]
        //[Required]
        public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name="Number in Stock")]
        [Range(1,20)]
        public int NumberInStock { get; set; }
        //navigation and FK
        //removed [Required] annotation couse EntityValidationErrors while Saving New Movie
        public Genre Genre { get; set; }

        [Required]
        [Display(Name="Genre")]
        public int GenreId { get; set; }
    }
}
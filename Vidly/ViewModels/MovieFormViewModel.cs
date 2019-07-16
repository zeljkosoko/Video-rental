using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;
//add for new added propertie for pure vmodel
using System.ComponentModel.DataAnnotations;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        //this view model is for MovieForm that insert new or edit existing selected movie
        //public Movie Movie { get; set; } PURE ViewModel has all required properties 

        public int? Id { get; set; } // if id==null then default ctor assigns one to id

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, MMMM d, yyyy}") ]
        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0: dddd, MMMM d, yyyy}") ]
        //[Required]
        //public DateTime DateAdded { get; set; }

        [Required]
        [Display(Name = "Number in Stock")]
        [Range(1, 20)]
        public int? NumberInStock { get; set; }

        //navigation and FK
        //removed [Required] annotation couse EntityValidationErrors while Saving New Movie
        //public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int? GenreId { get; set; }
       
        public IEnumerable<Genre> Genres { get; set; }

        public string Title
        {
            get
            {
                //if (Movie != null && Movie.Id != 0)
                //    return "Edit Movie";
                //return "New Movie";
                return Id != 0 ? "Edit Movie" : "New Movie";
            }
        }

        //add default and custom ctor for pure vmodel
        public MovieFormViewModel()
        {
            Id = 0;
            ReleaseDate = DateTime.MinValue; //1 Jan 0001
            NumberInStock = 0;
        }
        public MovieFormViewModel(Movie m)
        { // VMODEL takes object (movie m) parameter and initializes vmodel properties
            Id = m.Id;
            Name = m.Name;
            ReleaseDate = m.ReleaseDate;
            NumberInStock = m.NumberInStock;
            GenreId = m.GenreId;
        }

    }
}
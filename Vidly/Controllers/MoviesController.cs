using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
//for include 
using System.Data.Entity;
//EntityValidationErrors
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        public ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
                    //var movies = GetMovies();
                    //var movies = _context.Movies.Include(m => m.Genre).ToList();//include navigation property related to referenced object
                    //return View(movies);
            if(User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ViewResult New()
        {
            //New actions will open view that use model view(movie and navigation genre)
            //first load genres from dbContext and pass it to the MovieModelView
            //var movie = new Movie();
            //movie.ReleaseDate = (DateTime.Parse("01/01/0001"));
            //movie.NumberInStock = 0;

            var genres = _context.Genres.ToList();
            //then set genres to VM property
            var movieVM = new MovieFormViewModel()
            {
                //Movie = movie,
                Genres = genres
            };
            return View("MovieForm", movieVM);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {   
            //Validation begin
            if (!ModelState.IsValid)
            {
                //set modelview with correct filled data and show validation messages
                var movieMV = new MovieFormViewModel(movie) // sets movie properties using the custom constructor
                {
                    //Movie = movie,
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", movieMV);
            }
           //Validation end

            if(movie.Id == 0)
            {   //Validation failed for one or more entities. See 'EntityValidationErrors' property for more details.=> SOME Property is required
                movie.DateAdded = DateTime.Now;//if we dont have a input field for this property
                _context.Movies.Add(movie);
            }
            else
            {
                //get the corresponding movie from dbContext
                var movieFromdbContext = _context.Movies.Single(m => m.Id == movie.Id);
                movieFromdbContext.Name = movie.Name;
                movieFromdbContext.ReleaseDate = movie.ReleaseDate;
                movieFromdbContext.GenreId = movie.GenreId;
                movieFromdbContext.NumberInStock = movie.NumberInStock;
            }
            //If app failed at SaveChanges put try/catch and put brakepoint at console.write(ex).Then run in debug mode(F5) and app 
            // goes to brake point and read "ex" data..

            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    Console.WriteLine(ex);
            //}
            _context.SaveChanges();
                                   //movies controller uses index action which returns list view
            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id) 
        {
            //edit action pass the movie by id
            var movieFromDbContext = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieFromDbContext == null)
                return HttpNotFound();
            
            var movieVM = new MovieFormViewModel(movieFromDbContext)
            {
                //Movie = movieFromDbContext,
                Genres = _context.Genres.ToList()
            };
            //MovieForm use MovieVModel ie. 
            //Movie model and Genres.Movie Form uses GenresId and based on this id show appropriate Genre
            return View("MovieForm", movieVM);

        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int? id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);
            if(movie == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(movie);
            }
        }
        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie() { Id = 1, Name = "Godfather" },
        //        new Movie() { Id = 2, Name = "Rocky" }
        //    };
        //}
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (String.IsNullOrEmpty(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0} sortBy={1}", pageIndex, sortBy));
        //}
        // GET: Movies/Details
        //public ActionResult Details()
        //{
        //    //var movie = new Movie() { Name = "Shrek" };

        //    var customers = new List<Customer>
        //    {
        //        new Customer() { Name = "Petar" },
        //        new Customer() { Name = "Kosta" },
        //        new Customer() { Name = "Danijela" }
        //    };

        //    var movies = new List<Movie>
        //    {
        //        new Movie() { Name = "Shrek" },
        //        new Movie() { Name = "Wall-e" },
        //    };
        //    // aggregate model---------Movie + Customer--------------
        //    var vmodel = new RandomMovieViewModel()
        //    { //object initialization
        //        Movies = movies,
        //        Customers = customers
        //    };

        //    //return new ViewResult();
        //    //return View(movie);
        //    //return Content("helllo");
        //    //return HttpNotFound();
           
        //    return View(vmodel);
        //}

        //public ActionResult Edit(int id)
        //{
        //    return Content("id=" + id);
        //}

        ////first setup routes.mapAttributesRoute() in routeconfig
        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(String.Format(year + "/" + month));
        //}
        
    }
}
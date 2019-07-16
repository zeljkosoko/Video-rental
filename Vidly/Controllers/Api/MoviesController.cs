using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.DTOs;
using AutoMapper;
//include requires data.entity
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        
        //get(all movies) http request for api endpoint: /api/movies
        //public IEnumerable<Movie> GetMovies()
        //{
        //    return _context.Movies.ToList();
        //}
        public IHttpActionResult GetMovies()
        {
            var movieDTOs = _context.Movies
                .Include(m => m.Genre)
                .ToList()
                .Select(Mapper.Map<Movie, MovieDTO>);
            return Ok(movieDTOs);
        }

        //get(single movie) http request for api endpoint: /api/movies/1
        //public Movie GetMovie(int id)
        //{
        //    var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
        //    if (movieInDB == null)
        //        throw new HttpResponseException(HttpStatusCode.NotFound);
        //    return movieInDB;
        //}
       
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDB == null)
                return NotFound();

            //maps this movieInDb with its movieSubset
            var movieSubset = Mapper.Map<Movie, MovieDTO>(movieInDB);
            return Ok(movieSubset);
        }
        //post http request for api endpoint /api/movies

        //public Movie CreateMovie(Movie movie)
        //{
        //    if (!ModelState.IsValid)
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    _context.Movies.Add(movie);
        //    _context.SaveChanges();

        //    return movie;//db upgrade it with other properties
        //}

        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost] // Current RequestUri is /api/movies/10 => 10 is 'Id' of movieInDB
        public IHttpActionResult CreateMovie(MovieDTO movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //maps this movieDto with fullMovieInDb in _context
            var fullMovieInDb = Mapper.Map<MovieDTO, Movie>(movieDto);
            _context.Movies.Add(fullMovieInDb);
            _context.SaveChanges();

            movieDto.Id = fullMovieInDb.Id;
            
            //Status code: Created(201) is RESTful api convention(agreement)
            return Created(new Uri(Request.RequestUri + "/" + fullMovieInDb.Id), movieDto);//db upgrade it with other properties
            
        }
        //put http request for api endpoint /api/movies/11
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut] 
        public void UpdateMovie(int id, MovieDTO movieDto)// id must be in json !!!!!!!!!!!
        {
            //get movie by passed id, then mapper makes a map between moviedto and movieInDb

            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            Mapper.Map(movieDto, movieInDB);

            _context.SaveChanges();
        }
        //delete http request for api endpoint /api/customer/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDB = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            _context.Movies.Remove(movieInDB);
            _context.SaveChanges();

        }
    }
}

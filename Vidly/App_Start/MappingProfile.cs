using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//use Automapper
using AutoMapper;
//source and destination
using Vidly.Models;
using Vidly.DTOs;

namespace Vidly.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Domain to DTO
            Mapper.CreateMap<Customer, CustomerDTO>();
            Mapper.CreateMap<Movie, MovieDTO>();
            Mapper.CreateMap<MembershipType, MembershipTypeDTO>();
            Mapper.CreateMap<Genre, GenreDTO>();

            //DTO to Domain
            Mapper.CreateMap<CustomerDTO, Customer>().ForMember(c=>c.Id, opt => opt.Ignore()); // get request must have id so we ignore it like this
            Mapper.CreateMap<MovieDTO, Movie>().ForMember(m => m.Id, opt => opt.Ignore());

            
        }
        //and include Profile in global Application_start
    }
}
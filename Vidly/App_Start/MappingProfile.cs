using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Map Domain to Dto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<Movie, MovieDto>();
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();

            //Dto to Domain
            //CreateMap<MovieDto, Movie>().ForMember(m => m.Id, opt => opt.Ignore());
            //CreateMap<CustomerDto, Customer>().ForMember(m => m.Id, opt => opt.Ignore());



        }
    }
}
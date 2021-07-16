using AutoMapper;
using BeyondSportsApi.Entities;
using BeyondSportsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeyondSportsApi
{
    public class BeyondSportProfile : Profile
    {
        public BeyondSportProfile()
        {
            CreateMap<Team, TeamDto>()
                .ForMember(t => t.Name, map => map.MapFrom(team => team.Name));

            CreateMap<PlayerDto, Player>()
                .ReverseMap();
        }
    }
}

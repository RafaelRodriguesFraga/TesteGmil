using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteGmil.Model.Models;
using TesteGmil.View;

namespace TesteGmil.Model.Mappers
{
    public class ModelToDtoMapper : Profile
    {
        public ModelToDtoMapper()
        {
            CreateMap<Artist, ArtistDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Music, MusicDto>()
                .ForMember(dto =>  dto.Genre, opt => opt.MapFrom(model => model.Genre));
        }
    }
}

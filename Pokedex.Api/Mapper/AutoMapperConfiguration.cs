using AutoMapper;
using Pokedex.Api.Model;
using Pokedex.Repository.Persistence;
using System;

namespace Pokedex.Api.Mapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            CreateMap<Favorites, FavoritePokemonViewModel>()
               .ForMember(d => d.Id, s => s.MapFrom(a => a.PokemonId))
               .ForMember(d => d.Name, s => s.MapFrom(a => a.PokemonName))
               .ForMember(d => d.Types, s => s.MapFrom(a => a.Types.Split(',', StringSplitOptions.RemoveEmptyEntries) ))
               .ForMember(d => d.Sprite, s => s.MapFrom(a => Convert.ToBase64String(a.Thumbnail)));

        }
    }
}

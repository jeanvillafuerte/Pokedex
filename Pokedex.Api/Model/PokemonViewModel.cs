using Pokedex.Domain;
using System;

namespace Pokedex.Api.Model
{
    public class PokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Types { get; set; }
        public PokemonStats Stats { get; set; }
        public string Sprite { get; set; }
        public bool IsFavorite { get; set; }


        public static PokemonViewModel ConverToPokemonViewModel(Pokemon pokemon, Languages language)
        {
            var index = LanguageHelper.IndexLanguage(language);
            var img = Convert.ToBase64String(pokemon.Sprite);

            return new PokemonViewModel
            {
                Id = pokemon.Id,
                Name = pokemon.Names[index],
                Sprite = img,
                Stats = pokemon.Stats,
                Types = pokemon.Types
            };
        }
    }
}

using Newtonsoft.Json;
using Pokedex.Domain;

namespace Pokedex.Repository.Base
{
    [JsonConverter(typeof(PokemonConverter))]
    internal class PokemonDto
    {
        public int Id { get; set; }
        public string[] Names { get; set; }
        public string[] Types { get; set; }
        public PokemonStatsDto Stats { get; set; }
        public byte[] Sprite { get; set; }

        public Pokemon Convert()
        {
            return new Pokemon()
            {
                Id = this.Id,
                Names = this.Names,
                Types = this.Types,
                Stats = new PokemonStats
                { 
                    HP = this.Stats.HP,
                    Attack = this.Stats.Attack,
                    Defense = this.Stats.Defense,
                    SpAttack = this.Stats.SpAttack,
                    SpDefense = this.Stats.SpDefense,
                    Speed = this.Stats.Speed,
                }
            };
        }
    }

    class LanguageData
    {
        public string english { get; set; }
        public string japanese { get; set; }
        public string french { get; set; }
        public string chinese { get; set; }
    }
}

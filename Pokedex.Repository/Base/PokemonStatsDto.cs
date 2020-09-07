using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Repository.Base
{
    internal class PokemonStatsDto
    {
        [JsonProperty("HP")]
        public int HP { get; set; }
        [JsonProperty("Attack")]
        public int Attack { get; set; }
        [JsonProperty("Defense")]
        public int Defense { get; set; }
        [JsonProperty("Sp. Attack")]
        public int SpAttack { get; set; }
        [JsonProperty("Sp. Defense")]
        public int SpDefense { get; set; }
        [JsonProperty("Speed")]
        public int Speed { get; set; }
    }
}

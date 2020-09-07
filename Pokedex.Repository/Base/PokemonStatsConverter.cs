using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Repository.Base
{
    class PokemonStatsConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var pokemonStatsDto = new PokemonStatsDto();

            pokemonStatsDto.HP = (int)jObject["HP"];
            pokemonStatsDto.Attack = (int)jObject["Attack"];
            pokemonStatsDto.Defense = (int)jObject["Defense"];
            pokemonStatsDto.SpAttack = (int)jObject["Sp. Attack"];
            pokemonStatsDto.SpDefense = (int)jObject["Sp. Defense"];
            pokemonStatsDto.Speed = (int)jObject["Speed"];

            return pokemonStatsDto;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

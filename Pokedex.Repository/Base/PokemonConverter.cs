using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Repository.Base
{
    public class PokemonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var pokemonDto = new PokemonDto();
            pokemonDto.Id = (int)jObject["id"];
            var dataLanguages = jObject["name"].ToObject<LanguageData>();
            pokemonDto.Names = new[] { dataLanguages.english, dataLanguages.japanese, dataLanguages.chinese, dataLanguages.french };
            pokemonDto.Stats = jObject["base"].ToObject<PokemonStatsDto>();
            pokemonDto.Types = jObject["type"].ToObject<string[]>();
            return pokemonDto;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}

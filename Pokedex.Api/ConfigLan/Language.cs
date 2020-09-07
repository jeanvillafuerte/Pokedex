using Pokedex.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Api.ConfigLan
{
    public class Language
    {
        public Languages CurrentLanguage { get; set; }

        public static Language GetLanguage(string value)
        {
            Languages lan = Languages.English;

            switch (value)
            {
                case "English":
                    lan = Languages.English;
                    break;
                case "Chinese":
                    lan = Languages.Chinsese;
                    break;
                case "Japanese":
                    lan = Languages.Japanese;
                    break;
                case "French":
                    lan = Languages.French;
                    break;
                default:
                    throw new ArgumentException("Allowed languages are English, Chinese, Japanese, French, review appsettings.json");
            }

            return new Language() { CurrentLanguage = lan };
        }
    }
}

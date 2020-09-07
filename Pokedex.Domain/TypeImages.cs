using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Domain
{
    public enum TypeImages
    {
        Image,
        Thumbnail,
        Sprite
    }

    public enum Languages
    {
        English,
        Japanese,
        Chinsese,
        French
    }

    public static class LanguageHelper
    {
        public static int IndexLanguage(Languages language)
        {
            int indexLanguage = -1;

            switch (language)
            {
                case Languages.English:
                    indexLanguage = 0;
                    break;
                case Languages.Japanese:
                    indexLanguage = 1;
                    break;
                case Languages.Chinsese:
                    indexLanguage = 2;
                    break;
                case Languages.French:
                    indexLanguage = 3;
                    break;
                default:
                    break;
            }
            return indexLanguage;
        }
    }
}

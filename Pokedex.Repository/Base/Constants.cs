using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Repository.Base
{
    public static class Constants
    {
        public static string BaseUrlPokedex = "https://raw.githubusercontent.com/fanzeyi/pokemon.json/master/pokedex.json";
        public static string BaseUrlType = "https://github.com/fanzeyi/pokemon.json/blob/master/types.json";
        public static string BaseUrlImages = "https://github.com//fanzeyi/pokemon.json/blob/master/images/{0}.png?raw=true";
        public static string BaseUrlSprites = "https://github.com//fanzeyi/pokemon.json/blob/master/sprites/{0}MS.png?raw=true";
        public static string BaseUrlThumbnails = "https://github.com//fanzeyi/pokemon.json/blob/master/thumbnails/{0}.png?raw=true";
    }

}

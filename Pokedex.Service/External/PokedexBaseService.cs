using Newtonsoft.Json;
using Pokedex.Domain;
using Pokedex.Domain.Paginable;
using Pokedex.Repository.Base;
using Pokedex.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Service
{
    public interface IPokedexBaseService
    {
        Task<Pokemon> FindByIdAsync(int id);

        Task<IPageList<Pokemon>> FindByNameAsync(string name, int page, int pageSize, Languages language);

        Task<List<Pokemon>> GetAsync();

        Task<byte[]> GetImageAsync(int id, TypeImages type);

        Task<byte[]> GetSpriteAsync(int id);

        Task<byte[]> GetThumbnailAsync(int id);
    }

    public class PokedexBaseService : IPokedexBaseService
    {
        public async Task<Pokemon> FindByIdAsync(int id)
        {
            var all = await GetAsync();
            var filter = all.Where(s => s.Id == id).FirstOrDefault();
            filter.Sprite = await GetBaseImage(filter.Id, TypeImages.Image);
            return filter;
        }

        public async Task<IPageList<Pokemon>> FindByNameAsync(string name, int page, int pageSize, Languages language)
        {
            int indexLanguage = LanguageHelper.IndexLanguage(language);

            var all = await GetAsync();
            var filter = all.Where(s => s.Names[indexLanguage].ToLower().StartsWith(name.ToLower())).OrderBy(p => p.Names[indexLanguage]).ToList();

            var pagerResult = new PageList<Pokemon>(filter, page, pageSize);

            await Task.WhenAll(pagerResult.Select(s => PopulateImage(s)));

            return pagerResult;
        }

        public async Task<List<Pokemon>> GetAsync()
        {
            var Rawdata = await GetStringAsync(Constants.BaseUrlPokedex);
            var data = JsonConvert.DeserializeObject<List<PokemonDto>>(Rawdata);
            return data.Select(s => s.Convert()).ToList();
        }

        public async Task<byte[]> GetImageAsync(int id, TypeImages type)
        {
            return await GetBaseImage(id, TypeImages.Image);
        }

        public async Task<byte[]> GetSpriteAsync(int id)
        {
            return await GetBaseImage(id, TypeImages.Sprite);
        }

        public async Task<byte[]> GetThumbnailAsync(int id)
        {
            return await GetBaseImage(id, TypeImages.Thumbnail);
        }

        private static async Task<string> GetStringAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient.GetStringAsync(url);
            }
        }

        private async Task<byte[]> GetBaseImage(int id, TypeImages type)
        {
            string urlBase;
            switch (type)
            {
                case TypeImages.Image:
                    urlBase = Constants.BaseUrlImages;
                    break;
                case TypeImages.Thumbnail:
                    urlBase = Constants.BaseUrlThumbnails;
                    break;
                case TypeImages.Sprite:
                    urlBase = Constants.BaseUrlSprites;
                    break;
                default:
                    throw new Exception("Undefined type");
            }

            urlBase = string.Format(urlBase, id.ToString().PadLeft(3, '0'));

            using (var httpClient = new HttpClient())
            {
                var img = await httpClient.GetByteArrayAsync(urlBase);
                return img;
            }
        }

        private async Task<Pokemon> PopulateImage(Pokemon pokemon)
        {
            pokemon.Sprite = await GetThumbnailAsync(pokemon.Id);
            return pokemon;
        }

    }
}

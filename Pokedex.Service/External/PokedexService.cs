using Pokedex.Domain;
using Pokedex.Domain.Paginable;
using Pokedex.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pokedex.Service
{
    public sealed class PokedexService : IPokedexService
    {
        private readonly IPokedexBaseService _externalService;

        public PokedexService(IPokedexBaseService externalService)
        {
            _externalService = externalService;
        }

        public async Task<Pokemon> FindById(int id)
        {
            return await _externalService.FindByIdAsync(id);
        }

        public async Task<IPageList<Pokemon>> FindByName(string name, int page, int pageSize, Languages language)
        {
            var list = await _externalService.FindByNameAsync(name, page, pageSize, language);
            return list;
        }

        public async Task<IPageList<Pokemon>> FindByType(string type, int page, int pageSize)
        {
            var all = await GetAllAsync();
            var filter = all.Where(s => s.Types.Contains(type)).ToList();
            return new PageList<Pokemon>(filter, page, pageSize);
        }

        public async Task<List<Pokemon>> GetAllAsync()
        {
            return await _externalService.GetAsync();
        }

        public async Task<byte[]> GetThumbnail(int id)
        {
            return await _externalService.GetThumbnailAsync(id);
        }
    }
}

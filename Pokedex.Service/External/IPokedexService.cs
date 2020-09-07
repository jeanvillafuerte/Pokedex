using Pokedex.Domain;
using Pokedex.Domain.Paginable;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Service
{
    public interface IPokedexService
    {
        Task<byte[]> GetThumbnail(int id);
        Task<Pokemon> FindById(int id);
        Task<List<Pokemon>> GetAllAsync();
        Task<IPageList<Pokemon>> FindByName(string name, int page, int pageSize, Languages language);
        Task<IPageList<Pokemon>> FindByType(string type, int page, int pageSize);
    }
}

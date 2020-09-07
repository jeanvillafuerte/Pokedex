using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.ConfigLan;
using Pokedex.Api.Model;
using Pokedex.Repository;
using Pokedex.Repository.Persistence;
using Pokedex.Service;

namespace Pokedex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PokedexController : ControllerBase
    {
        private readonly IPokedexService _service;
        private readonly Language _language;
        private readonly IRepository<Favorites> _favRepo;

        public PokedexController(IPokedexService service, Language language, IRepository<Favorites> favRepo)
        {
            _service = service;
            _language = language;
            _favRepo = favRepo;
        }

        [HttpGet("search/{name}/{page}/{pageSize}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FindByName(string name, int page, int pageSize)
        {
            var result = await _service.FindByName(name, page, pageSize, _language.CurrentLanguage);
            var data = result.Select(p => PokemonViewModel.ConverToPokemonViewModel(p, _language.CurrentLanguage));

            data = await SetFavoritePokemon(data.ToList());

            return Ok(new { data , total = result.Total, totalPages = result.TotalPages });
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> FindById(int id)
        {
            var result = await _service.FindById(id);
            var data = PokemonViewModel.ConverToPokemonViewModel(result, _language.CurrentLanguage);

            return Ok(data);
        }

        private async Task<IEnumerable<PokemonViewModel>> SetFavoritePokemon(IEnumerable<PokemonViewModel> pokemons)
        {
            var ids = pokemons.Select(s => s.Id).ToArray();
            var userFav = await _favRepo.SelectAsync(p => ids.Contains(p.PokemonId), null, null, null, null);

            foreach (var fav in userFav)
            {
                var pok = pokemons.Where(s => s.Id == fav.PokemonId).FirstOrDefault();
                pok.IsFavorite = true;
            }

            return pokemons;
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.ConfigLan;
using Pokedex.Api.Model;
using Pokedex.Domain;
using Pokedex.Domain.Paginable;
using Pokedex.Repository;
using Pokedex.Repository.Persistence;
using Pokedex.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PokemonController : Controller
    {
        private readonly IRepository<Favorites> _favRepo;
        private readonly IDataContextAsync _context;
        private readonly IMapper _mapper;
        private readonly IPokedexService _service;
        private readonly Language _language;

        public PokemonController(IRepository<Favorites> favRepo, IDataContextAsync context, IMapper mapper, IPokedexService service, Language language)
        {
            _favRepo = favRepo;
            _context = context;
            _mapper = mapper;
            _service = service;
            _language = language;
        }

        [HttpGet("favorites/{idUser}/{page}/{pageSize}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ListFavorites(int idUser, int page = 1, int pageSize = 10)
        {
            var list = await _favRepo.SelectAsync(s => s.UserId == idUser, d => d.OrderByDescending( f => f.DateReg), null, null, null);
            var listViewModel = _mapper.Map<List<FavoritePokemonViewModel>>(list);

            var data = new PageList<FavoritePokemonViewModel>(listViewModel, page, pageSize);

            return Ok(new { data, total = data.Total, totalPages = data.TotalPages });
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Add(UserFavoriteViewModel model)
        {
            var pokemon = await _service.FindById(model.IdPokemon);

            var img = await _service.GetThumbnail(model.IdPokemon);

            var index = LanguageHelper.IndexLanguage(_language.CurrentLanguage);

            _favRepo.Add(new Favorites()
            {
                UserId = model.IdUser,
                PokemonName = pokemon.Names[index],
                Thumbnail = img,
                DateReg = DateTime.Now,
                PokemonId = model.IdPokemon,
                Types = string.Join(',', pokemon.Types)
            });

            var result = await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{idUser}/{idPokemon}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete(int idUser, int idPokemon)
        {
            var pokemon = await _favRepo.SelectAsync(s => s.UserId == idUser && s.PokemonId == idPokemon, null, null, null, null);
            _favRepo.Remove(pokemon.FirstOrDefault());
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}

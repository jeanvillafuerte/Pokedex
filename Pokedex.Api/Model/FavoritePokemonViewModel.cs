
namespace Pokedex.Api.Model
{
    public class FavoritePokemonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string[] Types { get; set; }
        public string Sprite { get; set; }
        public bool IsFavorite { get; set; } = true;
    }
}

namespace Pokedex.Domain
{
  
    public class Pokemon
    {
        public int Id { get; set; }
        public string[] Names { get; set; }
        public string[] Types { get; set; }
        public PokemonStats Stats { get; set; }
        public byte[] Sprite { get; set; }
    }

}

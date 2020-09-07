namespace Pokedex.Api.Security
{
    public class AppUserAuth
    {
        public AppUserAuth()
        {
            Id = 0;
            UserName = "Not Authorize";
            BearerToken = string.Empty;
            IsAuthenticated = false;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string BearerToken { get; set; }
    }
}

namespace BabatMebel.App.Services.Abstractions
{
    public interface IUserService
    {
        public Task<string> FindUser(string username);
    }
}

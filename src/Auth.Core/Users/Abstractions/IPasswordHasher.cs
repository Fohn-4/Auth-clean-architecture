namespace Auth.Core.Users.Abstractions
{
    public interface IPasswordHasher
    {
        string Hash(string password);
    }
}
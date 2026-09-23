namespace Auth.Core.Users.Dtos
{
    public class RegisterRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
    }
}
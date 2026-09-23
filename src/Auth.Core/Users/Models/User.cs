namespace Auth.Core.Users.Models
{
    public sealed class User
    {
        public required Guid Id { get; init; }
        public required string Email { get; init; }
        public string? HashedPassword { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public required Role Role { get; init; }
    }
}
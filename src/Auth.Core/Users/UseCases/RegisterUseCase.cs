using Auth.Core.Users.Abstractions;
using Auth.Core.Users.Dtos;
using Auth.Core.Users.Models;

namespace Auth.Core.Users.UseCases
{
    public sealed class RegisterUseCase
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher _hasher;

        public RegisterUseCase(IUserRepository repository, IPasswordHasher hasher)
        {
            _repository = repository;
            _hasher = hasher;
        }

        public async Task<Guid> ExecuteAsync(RegisterRequest request, CancellationToken ct)
        {
            var hashedPassword = _hasher.Hash(request.Password);

            var user = new User
            {
                Id = Guid.CreateVersion7(),
                Email = request.Email,
                HashedPassword = hashedPassword,
                FirstName = request.FirstName,
                LastName =  request.LastName,
                Role = Role.Visieur
            };

            await _repository.AddAsync(user, ct);

            return user.Id;
        }

        
    }
}
using Auth.Core.Users.Abstractions;
using Auth.Core.Users.Dtos;
using Auth.Core.Users.UseCases;
using NSubstitute;
using Auth.Core.Users.Models;
using Auth.Core.Users.Exceptions;
using NSubstitute.ExceptionExtensions;

namespace Auth.UnitTests.Users;

public class RegisterUseCaseTests
{
    [Fact]
    public async Task Register_persists_the_new_user()
    {
        //arrange
        var repository = Substitute.For<IUserRepository>();
        var hasher = Substitute.For<IPasswordHasher>();
        hasher.Hash("secret123").Returns("HASHED_PW");

        var sut = new RegisterUseCase(repository, hasher);
        var request = new RegisterRequest
        {
            Email = "jean.bon@mail.com",
            Password = "secret123",
            FirstName = "Jean",
            LastName = "Bon"
        };

        //act
        var newId = await sut.ExecuteAsync(request, CancellationToken.None);
        
        //assert
        await repository.Received(1).AddAsync(Arg.Any<User>(),Arg.Any<CancellationToken>());
        
        var persistedUser = (User)repository.ReceivedCalls().Single().GetArguments()[0]!;

        Assert.Equal("jean.bon@mail.com", persistedUser.Email);
        Assert.Equal("HASHED_PW", persistedUser.HashedPassword);
        Assert.Equal(Role.Visiteur, persistedUser.Role);
        Assert.Equal(newId, persistedUser.Id);    
    }

    [Fact]
    public async Task Register_throws_when_email_already_exists()
    {
        //arrange
        var repository = Substitute.For<IUserRepository>();
        var hasher = Substitute.For<IPasswordHasher>();

        var sut = new RegisterUseCase(repository, hasher);
        var request = new RegisterRequest
        {
            Email = "jean.bon@mail.com",
            Password = "secret123"
        };

        repository.AddAsync(Arg.Any<User>(), Arg.Any<CancellationToken>()).ThrowsAsync(new DuplicateEmailException("jean.bon@mail.com"));
        
        
        //act + assert
        await Assert.ThrowsAsync<DuplicateEmailException>( 
            () => sut.ExecuteAsync(request, CancellationToken.None));
    }
}
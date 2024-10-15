using LojaSeuManuel.Api.Models;

namespace LojaSeuManuel.Api.Services.Interfaces;

public interface IUserService
{
    Task<string> AuthenticateAsync(LoginModel login);
}
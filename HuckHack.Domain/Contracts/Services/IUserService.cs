using HuckHack.Domain.Entities;

namespace HuckHack.Domain.Contracts.Services
{
    public interface IUserService
    {
        string Create(User user);

        User Get(string id);
    }
}

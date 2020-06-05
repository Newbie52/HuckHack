using HuckHack.Domain.Contracts.Repositories;
using HuckHack.Domain.Contracts.Services;
using HuckHack.Domain.Entities;

namespace HuckHack.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string Create(User user)
        {
            var isRegistered = _userRepository.IsRegistered(user.Email);
            if (!isRegistered)
                return _userRepository.Create(user);
            else
                return _userRepository.GetByEmail(user.Email).Id;
        }

        public User Get(string id)
        {
            return _userRepository.Get(id);
        }
    }
}

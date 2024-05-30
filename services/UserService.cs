using academ_sync_back.Models;
using academ_sync_back.Repositories;

namespace academ_sync_back.services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !_jwtService.VerifyPassword(password, user.Password))
            {
                return null;
            }

            return _jwtService.GenerateToken(user.Id, user.Email, user.Role);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            if (await _userRepository.EmailExistsAsync(user.Email))
            {
                throw new ArgumentException("Email already exists");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)   
            {
                await _userRepository.DeleteAsync(user);
            }
        }
    }
}

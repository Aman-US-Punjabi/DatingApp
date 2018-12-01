// using System.Security.Claims;
// using System.Threading.Tasks;
// using DatingApp.API.Helpers;
// using DatingApp.API.Interfaces;
// using DatingApp.Core.Entities.Catalog;
// using DatingApp.Core.Interfaces.Repositories;
// using DatingApp.Core.Specifications;
// using Microsoft.Extensions.Logging;

// namespace DatingApp.API.Services
// {
//     public class UserService : IUserService
//     {
//         private readonly IUserRepository _userRepository;
//         private readonly ILogger<UserService> _logger;

//         public UserService(
//             IUserRepository userRepository,
//             ILoggerFactory loggerFactory
//         )
//         {
//             _logger = loggerFactory.CreateLogger<UserService>();
//             _userRepository = userRepository;
//         }

//         public async Task<User> GetUsers(int currentUserId, UserParams userParams)
//         {
//             _logger.LogInformation("--------------------------------------------");
//             _logger.LogInformation("UserService.Api is called");
//             _logger.LogInformation("--------------------------------------------");

//             // var userFromRepo = await _repo.GetUser(currentUserId);
//             var userFromRepo = await _userRepository.GetByIdAsync(currentUserId);
            

//             userParams.UserId = currentUserId;
//             _logger.LogInformation("");

//             if (string.IsNullOrEmpty(userParams.Gender))
//             {
//                 userParams.Gender = userFromRepo.Gender == "male" ? "female" : "male";
//             }

//             var userFilterSpecification = new UserFilterSpecification(userFromRepo.Gender);

//             // var users = await _repo.GetUsers(userParams);
//             var usersOnPage = await _userRepository.ListAsync(userFilterSpecification);
//             var totalUser = _userRepository.Count(userFilterSpecification);
//             var userFilterSpecification = new UserFilterSpecification("male");


            
//         }
//     }
// }
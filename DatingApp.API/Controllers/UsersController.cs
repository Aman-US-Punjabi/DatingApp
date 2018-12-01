using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.Core.Entities.Catalog;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Interfaces.Repositories;
using DatingApp.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(
            IUserRepository userRepository,
            IAppLogger<UsersController> logger,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _userRepository.GetByIdAsync(currentUserId);
            

            userParams.UserId = currentUserId;

            // Filter the user by gender
            var genderFilter = userParams.Gender ?? (userFromRepo.Gender == "male" ? "female" :  "male");
            var minDateOfBirthFilter =  DateTime.Today.AddYears(-99 - 1);;
            var maxDateOfBirthFilter = DateTime.Today.AddYears(-18);;

            if (userParams.minAge != 18 || userParams.maxAge != 99)
            {
                minDateOfBirthFilter = DateTime.Today.AddYears(-userParams.maxAge - 1);
                maxDateOfBirthFilter = DateTime.Today.AddYears(-userParams.minAge);
            }

            var userFilterSpecification = new UserFilterSpecification(
                includePhotos: true,
                gender: genderFilter,
                excludeUserId: userFromRepo.Id,
                minDateOfBirth: minDateOfBirthFilter, 
                maxDateOfBirth: maxDateOfBirthFilter
            );

            var userFilterPaginatedSpecification = new UserFilterPaginatedSpecification(
                userParams.TotalResultsToSkipForPagination,
                userParams.PageSize,
                includePhotos: true,
                gender: genderFilter,
                excludeUserId: userFromRepo.Id,
                minDateOfBirth: minDateOfBirthFilter, 
                maxDateOfBirth: maxDateOfBirthFilter
            );

            // ----------------  Filter applied  -----------------

            var users = await _userRepository.ListAsync(userFilterPaginatedSpecification);
            var totalUsers = _userRepository.Count(userFilterSpecification);

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(userParams.PageNumber, userParams.PageSize, totalUsers, userParams.TotalPages(totalUsers));
            
            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetByIdWithPhotosAsync(id);

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);
            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _userRepository.GetByIdAsync(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _userRepository.UpdateAsync(userFromRepo))
                return NoContent();
            
            throw new Exception($"Updating user {id} failed on save"); 
        }
    }
}
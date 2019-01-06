using System.Threading.Tasks;
using DatingApp.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using DatingApp.API.Dtos;
using Microsoft.AspNetCore.Identity;
using DatingApp.Core.Entities.Catalog;
using AutoMapper;
using System.Collections.Generic;
using DatingApp.Core.Specifications;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AdminController(
            IUserRepository userRepository,
            UserManager<User> userManager,
            IMapper mapper
        )
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await _userRepository.ListAllAsync();

            var users = new List<object>();

            foreach (var user in userList) {
                var userRoles = _userManager.GetRolesAsync(user).Result;
                var userToReturn = new {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = userRoles
                };

                users.Add(userToReturn);
            }

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await _userManager.FindByNameAsync(userName);

            var userRoles = await _userManager.GetRolesAsync(user);

            // get roles passed in body with post request
            var selectedRoles = roleEditDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] {};

            // add new roles, passed in body
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            // if new roles added, remove previous roles
            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to remove the roles");

            return Ok(await _userManager.GetRolesAsync(user));
        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public IActionResult GetPhotosForModeration()
        {
            return Ok("Only moderators can see this");
        }
    }
}
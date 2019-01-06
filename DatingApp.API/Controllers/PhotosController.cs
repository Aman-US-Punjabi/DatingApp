using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Helpers;
using DatingApp.Core.Entities.Catalog;
using DatingApp.Core.Interfaces;
using DatingApp.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.API.Controllers
{
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IAsyncRepository<Photo> _photoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAppLogger<PhotosController> _logger;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(
            IAsyncRepository<Photo> photoRepository,
            IUserRepository userRepository,
            IAppLogger<PhotosController> logger,
            IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            this._cloudinaryConfig = cloudinaryConfig;
            this._mapper = mapper;
            _photoRepository = photoRepository;
            _userRepository = userRepository;
            _logger = logger;
            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _photoRepository.GetByIdAsync(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            // var userFromRepo = await _repo.GetUser(userId);
            var userFromRepo = await _userRepository.GetByIdWithPhotosAsync(userId);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;
            
            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);


            if (await _userRepository.UpdateAsync(userFromRepo))
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _userRepository.GetByIdWithPhotosAsync(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();
            
            var photoFromRepo = await _photoRepository.GetByIdAsync(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This is already the main photo");

            var currentMainPhoto = await _userRepository.GetMainPhotoByUserIdAsync(userId);
            currentMainPhoto.IsMain = false;
            await _photoRepository.UpdateAsync(currentMainPhoto);

            photoFromRepo.IsMain = true;

            if (await _photoRepository.UpdateAsync(photoFromRepo))
                return NoContent();

            return BadRequest("Could not set photo to main.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _userRepository.GetByIdWithPhotosAsync(userId);

            if (!user.Photos.Any(p => p.Id == id))
                return Unauthorized();
                        
            var photoFromRepo = await _photoRepository.GetByIdAsync(id);

            if (photoFromRepo.IsMain)
                return BadRequest("You can't delete the main photo!");

            if (photoFromRepo.PublicId != null)
            {
                var deleteParam = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParam);

                if (result.Result == "ok") {
                    if (await _photoRepository.DeleteAsync(photoFromRepo))
                        return Ok();
                }
            }

            if (photoFromRepo.PublicId == null)
            {
                if (await _photoRepository.DeleteAsync(photoFromRepo))
                    return Ok();
            }

            return BadRequest("Failed to delete the photo");
        }
    }
}
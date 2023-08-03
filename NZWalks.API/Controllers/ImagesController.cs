using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/images

    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        // Uploading an image
        // POST: // https://localhost:portnumber/api/images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto requestDto)
        {
            ValidateFileUpload(requestDto);

            if(!ModelState.IsValid) return BadRequest(ModelState);

            var image = new Image
            {
                File = requestDto.File,
                FileExtension = Path.GetExtension(requestDto.File.FileName),
                FileSizeInBytes = requestDto.File.Length,
                FileName = requestDto.FileName,
                FileDescription = requestDto.FileDescription
            };

            await imageRepository.Upload(image);

            return Ok(image);
        }

        private void ValidateFileUpload(ImageUploadRequestDto requestDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(requestDto.File.FileName))) ModelState.AddModelError("file", "Unsupported file extension");

            if (requestDto.File.Length > 10485760) ModelState.AddModelError("file", "File cannot exceed 10MB");
        }
    }
}

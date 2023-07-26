using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            // Get data from database - Domain Models
            var regions = await dbContext.Regions.ToListAsync();

            // Map domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto() 
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            }

            // Return DTOs to client
            return Ok(regionsDto);
        }

        // Get region by Id
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if (region == null) return NotFound();

            // Map domain model to dto
            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            // Region DTO to client
            return Ok(regionDto);
        }

        // Create new region
        // POST: https://localhost:portnumber/api/regions/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to domain model
            var region = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use domain model to create region
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            // Map domain back to DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update a region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Check if region exists
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) return NotFound();

            // Map DTO to domain model
            region.Code = updateRegionRequestDto.Code;
            region.Name = updateRegionRequestDto.Name;
            region.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
            await dbContext.SaveChangesAsync();

            // Map domain model to DTO
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }

        // Delete a region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if the region exists
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) return NotFound();

            // Delete region
            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();

            // Return deleted region
            var regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}

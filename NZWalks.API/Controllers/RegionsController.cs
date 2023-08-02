using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions

    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Get all regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            // Get data from database - Domain Models
            var regions = await regionRepository.GetAllAsync();

            // Map domain models to DTOs using Automapper
            var regionsDto = mapper.Map<List<RegionDto>>(regions);

            // Return DTOs to client
            return Ok(mapper.Map<List<RegionDto>>(regions));  
        }

        // Get region by Id
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var region = await regionRepository.GetByIdAsync(id);

            if (region == null) return NotFound();

            // Map domain model to dto using Automapper, Region DTO to client
            return Ok(mapper.Map<RegionDto>(region));
        }

        // Create new region
        // POST: https://localhost:portnumber/api/regions/
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map DTO to domain model using Automapper
            var region = mapper.Map<Region>(addRegionRequestDto);

            // Use domain model to create region
            region = await regionRepository.CreateAsync(region);

            // Map domain back to DTO using Automapper
            var regionDto = mapper.Map<RegionDto>(region);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update a region
        // PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            // Map DTO to domain model using Automapper
            var region = mapper.Map<Region>(updateRegionRequestDto);

            // Check if region exists
            region = await regionRepository.UpdateAsync(id, region);
            if (region == null) return NotFound();

            return Ok(mapper.Map<RegionDto>(region));
        }

        // Delete a region
        // DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Check if the region exists
            var region = await regionRepository.DeleteAsync(id);
            if (region == null) return NotFound();

            // Return deleted region
            return Ok(mapper.Map<RegionDto>(region));
        }
    }
}

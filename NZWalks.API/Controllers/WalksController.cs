﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/walks

    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // Create walk
        // POST: // https://localhost:portnumber/api/walks
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //Check validation
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var walk = mapper.Map<Walk>(addWalkRequestDto);
            await walkRepository.CreateAsync(walk);

            return Ok(mapper.Map<WalkDto>(walk));
        }

        // Get all walks
        // GET: // https://localhost:portnumber/api/walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var walks = await walkRepository.GetAllAsync();
            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        // Get walk by id
        // GET: // https://localhost:portnumber/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);
            if (walk == null) return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }

        // Update walk by id
        // PUT: // https://localhost:portnumber/api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Check validation
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var walk = mapper.Map<Walk>(updateWalkRequestDto);
            await walkRepository.UpdateAsync(id, walk);

            if (walk == null) return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }

        // Delete walk by id
        // DELETE: // https://localhost:portnumber/api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteAsync(id);
            if (walk == null) return NotFound();

            return Ok(mapper.Map<WalkDto>(walk));
        }
    }
}

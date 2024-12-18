﻿using BACKEND.DTOs;
using BACKEND.Models;
using BACKEND.Services;
using BACKEND.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _storeContext;
        private  IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;
        private IBeerServicess _beerService;

        public BeerController(StoreContext storeContext, IValidator<BeerInsertDto> beerInsertValidator, IValidator<BeerUpdateDto> beerUpdateValidator, IBeerServicess beerService)
        {
            _storeContext = storeContext;
            _beerInsertValidator = beerInsertValidator;
            _beerUpdateValidator = beerUpdateValidator;
            _beerService = beerService;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _storeContext.Beers.Select(b => new BeerDto
            {
                Id = b.BeerId,
                Al = b.Al,
                BrandID = b.BrandId,
                Name = b.Name
            }).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Al = beer.Al,
                BrandID = beer.BrandId,
                Name = beer.Name
            };

            return Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationResult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                BrandId = beerInsertDto.BrandID,
                Al = beerInsertDto.Al
            };

            await _storeContext.Beers.AddAsync(beer);
            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandID = beer.BrandId,
                Al = beer.Al
            };

            return CreatedAtAction(nameof(GetById), new { id = beer.BeerId }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            // Validar el DTO de actualización
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            beer.Name = beerUpdateDto.Name;
            beer.Al = beerUpdateDto.Al;
            beer.BrandId = beerUpdateDto.BrandID;

            await _storeContext.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BeerId,
                Name = beer.Name,
                BrandID = beer.BrandId,
                Al = beer.Al
            };

            return Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            try
            {
                var beer = await _storeContext.Beers.FindAsync(id);
                if (beer == null)
                {
                    return NotFound();
                }

                _storeContext.Beers.Remove(beer);
                await _storeContext.SaveChangesAsync();

                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.Name,
                    BrandID = beer.BrandId,
                    Al = beer.Al
                };

                return Ok(beerDto);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting the beer.");
            }
        }
    }
}

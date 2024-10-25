using BACKEND.DTOs;
using BACKEND.Models;
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

        public BeerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _storeContext.Beers.Select(b => new BeerDto
            {
                Id = b.BrandId,
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
            try
            {
                var beer = new Beer()
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
                    Name = beerInsertDto.Name,
                    BrandID = beerInsertDto.BrandID,
                    Al = beerInsertDto.Al
                };
                return CreatedAtAction(nameof(GetById), new { id = beer.BeerId }, beerDto);
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException != null ? dbEx.InnerException.Message : dbEx.Message;
                return StatusCode(500, $"Internal server error: {innerException}");
            }
        }
    }
}
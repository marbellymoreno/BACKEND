using BACKEND.DTOs;
using BACKEND.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Controllers
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
            await _storeContext.Beers.Select( b => new BeerDto
            {
                Id = b.BrandId,
                Al = b.Al,
                BrandID = b.BrandId,
                Name = b.Name,
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
                Id = beer.BrandId,
                Al = beer.Al,
                BrandID = beer.BrandId,
                Name = beer.Name,
            };

            return Ok(beerDto);
            }
    }
}

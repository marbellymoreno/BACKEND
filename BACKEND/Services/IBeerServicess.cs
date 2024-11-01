using BACKEND.DTOs;

namespace BACKEND.Services
{
    public interface IBeerServicess
    {
        Task<IEnumerable<BeerDto>> Get();
        Task<BeerDto> GetById(int id);
        Task<BeerDto> Add(BeerInsertDto  beerInsertDto);
        Task<BeerDto> Update(int id, BeerUpdateDto beerUpdate);
        Task<BeerDto> Delete(int id);
    }
}
using BACKEND.DTOs;

namespace BACKEND.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDTO>> Get();
    }
}

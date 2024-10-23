using BACKEND.DTOs;
using System.Text.Json;

namespace BACKEND.Services
{
    public class PostService : IPostService
    {
        private HttpClient _httpClient;

        public PostService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<PostDTO>> Get()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var result = await _httpClient.GetAsync(url);

            var body = await result.Content.ReadAsStringAsync();

            var post =  JsonSerializer.Deserialize<IEnumerable <PostDTO>>(body);
            return post;
        }
    }
}

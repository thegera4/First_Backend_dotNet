using First_Backend_dotNet.DTOs;
using System.Text.Json;

namespace First_Backend_dotNet.Services
{
    public class PostsService : IPostsService
    {
        private readonly HttpClient _httpClient;

        public PostsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<PostDto>> Get()
        {
            var response = await _httpClient.GetAsync(_httpClient.BaseAddress);
            var body = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true //para ignorar mayusculas y minusculas cuando se deserializa el json
            };

            var post = JsonSerializer.Deserialize<IEnumerable<PostDto>>(body, options);
            return post;
        }
    }
}

using First_Backend_dotNet.DTOs;
using First_Backend_dotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Backend_dotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _titlesService;

        public PostsController(IPostsService titlesService) // constructor recibe las interfaces que se registraron en el Program.cs (inyeccion de dependencias)
        {
            _titlesService = titlesService;
        }

        [HttpGet]
        public async Task<IEnumerable<PostDto>> Get() => await _titlesService.Get();
        
    }
}

using First_Backend_dotNet.DTOs;

namespace First_Backend_dotNet.Services
{
    public interface IPostsService
    {
        public Task<IEnumerable<PostDto>> Get(); // se usa IEnumerable en lugar de List porque IEnumerable es mas eficiente y solo es para mostrar datos, en cambio List es para realizar mas operaciones.
    }
}

using First_Backend_dotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Backend_dotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomService _randomServiceSingleton;
        private IRandomService _randomServiceScoped;
        private IRandomService _randomServiceTransient;

        private IRandomService _randomService2Singleton;
        private IRandomService _randomService2Scoped;
        private IRandomService _randomService2Transient;

        public RandomController( // constructor recibe las interfaces que se registraron en el Program.cs (inyeccion de dependencias)
            [FromKeyedServices("randomSingleton")] IRandomService randomServiceSingleton,
            [FromKeyedServices("randomScoped")] IRandomService randomServiceScoped,
            [FromKeyedServices("randomTransient")] IRandomService randomServiceTransient,
            [FromKeyedServices("randomSingleton")] IRandomService randomService2Singleton,
            [FromKeyedServices("randomScoped")] IRandomService randomService2Scoped,
            [FromKeyedServices("randomTransient")] IRandomService randomService2Transient
        )
        {
            _randomServiceSingleton = randomServiceSingleton;
            _randomServiceScoped = randomServiceScoped;
            _randomServiceTransient = randomServiceTransient;
            _randomService2Singleton = randomService2Singleton;
            _randomService2Scoped = randomService2Scoped;
            _randomService2Transient = randomService2Transient;

        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get() 
        {
            var result = new Dictionary<string, int>
            {
                { "Singleton 1", _randomServiceSingleton.Value },
                { "Scoped 1", _randomServiceScoped.Value },
                { "Transient 1", _randomServiceTransient.Value },
                { "Singleton 2", _randomService2Singleton.Value },
                { "Scoped 2", _randomService2Scoped.Value },
                { "Transient 2", _randomService2Transient.Value }
            };
            return result;
        }






    }
}

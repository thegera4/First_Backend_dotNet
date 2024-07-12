using First_Backend_dotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Backend_dotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private IPeopleService _peopleService;

        public PeopleController()
        {
            _peopleService = new PeopleService();
        }

        [HttpGet("all")] // esto agrega al endpoint "/all" por lo que aqui queda "/api/Poeple/all"
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")] // esto agrega al endpoint "/{id}" por lo que aqui queda "/api/Poeple/{id}" es para filtrar por id
        public ActionResult<People> Get(int id) // ActionResult es para cuando tenemos diferentes tipos de respuestas, en este caso puede ser un People o un Error 404 (NotFound)
        {
            var people = Repository.People.FirstOrDefault(param => param.Id == id.ToString()); //FirstOrDefault es para obtener el primer elemento que cumpla con la condicion o null si no hay ninguno (para que no truene)
            if(people == null)
            {
                return NotFound(); // NotFound es un metodo de la clase ControllerBase que regresa un 404
            }
            return Ok(people); // Ok es un metodo de la clase ControllerBase que regresa un 200
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) => 
            Repository.People.Where(param => param.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people) // IActionResult es para cuando tenemos respuestas que no son un objeto, o pueden ser vacias o en esta caso es un metodo para agregar un objeto a la lista
        {
            if (!_peopleService.Validate(people)) // aqui solo mandamos a llamar el metodo de validacion que creamos en la capa de servicio
            {
                return BadRequest();
            }

            Repository.People.Add(people);
            return NoContent();
        }
    }

    public class People 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People() { Id = "1", Name = "John", Birthdate = new DateTime(1990, 1, 1) },
            new People() { Id = "2", Name = "Jane", Birthdate = new DateTime(1991, 2, 2) },
            new People() { Id = "3", Name = "Jack", Birthdate = new DateTime(1992, 3, 3) },
            new People() { Id = "4", Name = "Jill", Birthdate = new DateTime(1993, 4, 4) },
        };
    }

}

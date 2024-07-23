using First_Backend_dotNet.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Backend_dotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        // public PeopleController(IPeopleService peopleService) //Constructor de la clase para cuando se inyecta la interfaz metodo viejo
        public PeopleController([FromKeyedServices("people2Service")] IPeopleService peopleService) //Constructor de la clase para cuando se inyecta la interfaz metodo nuevo
        {
            _peopleService = peopleService; //Aqui estamos inyectando la interfaz que registramos en el Program.cs
        }

        [HttpGet("all")] // esto agrega al endpoint "/all" por lo que aqui queda "/api/Poeple/all"
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")] // esto agrega al endpoint "/{id}" por lo que aqui queda "/api/Poeple/{id}" es para filtrar por id
        public ActionResult<People> Get(int id) // ActionResult es para cuando tenemos diferentes tipos de respuestas, en este caso puede ser un People o un Error 404 (NotFound)
        { //FirstOrDefault es para obtener el primer elemento que cumpla con la condicion o null si no hay ninguno (para que no truene)
            var people = Repository.People.FirstOrDefault(param => param.Id == id.ToString());
            if(people == null)
            {
                return NotFound(); // NotFound es un metodo de la clase ControllerBase que regresa un 404
            }
            return Ok(people); // Ok es un metodo de la clase ControllerBase que regresa un 200
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) => Repository.People.Where(param => param.Name!
        .Contains(search, StringComparison.CurrentCultureIgnoreCase)).ToList(); //StringComparison.CurrentCultureIgnoreCase the ahorra el .ToUpper o .ToLower en varios lados

        [HttpPost]
        public IActionResult Add(People people) // IActionResult es para cuando tenemos respuestas que no son un objeto, o pueden ser vacias o en esta caso es un metodo para agregar un objeto a la lista
        {
            if (!_peopleService.Validate(people)) // aqui solo mandamos a llamar el metodo de validacion que creamos en la capa de servicio
            {
                return BadRequest(); // BadRequest es un metodo de la clase ControllerBase que regresa un 400
            }

            Repository.People.Add(people);
            return NoContent(); // NoContent es un metodo de la clase ControllerBase que regresa un 204
        }
    }

    public class People 
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public DateTime Birthdate { get; set; }
    }

    public class Repository
    {
        private static List<People> people =
        [
            new People() { Id = "1", Name = "John", Birthdate = new DateTime(1990, 1, 1) },
            new People() { Id = "2", Name = "Jane", Birthdate = new DateTime(1991, 2, 2) },
            new People() { Id = "3", Name = "Jack", Birthdate = new DateTime(1992, 3, 3) },
            new People() { Id = "4", Name = "Jill", Birthdate = new DateTime(1993, 4, 4) },
        ];

        public static List<People> People { get => people; set => people = value; }
    }

}

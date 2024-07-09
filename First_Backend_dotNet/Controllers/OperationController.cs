using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace First_Backend_dotNet.Controllers
{
    [Route("api/[controller]")] // [controller] es un helper que toma el nombre del controlador en este caso "Operation"
    [ApiController] // un controlador es quien recibe las peticiones y responde algo a ellas
    public class OperationController : ControllerBase
    {
        [HttpGet]
        public decimal Get(decimal a, decimal b)
        {
            return a + b;
        }

        [HttpPost]
        public decimal Add(Numbers numbers, [FromHeader] string Host, //[FromHeader] sirve para obtener el valor de algun header
            [FromHeader(Name = "Content-Length")] string ContentLength,
            [FromHeader(Name = "X-Some")] string Some) 
        {
            Console.WriteLine(Host);
            Console.WriteLine(ContentLength);
            Console.WriteLine(Some);
            return numbers.A - numbers.B;
        }

        [HttpPut]
        public decimal Edit(decimal a, decimal b)
        {
            return a * b;
        }

        [HttpDelete]
        public decimal Delete(decimal a, decimal b)
        {
            return a / b;
        }
    }

    public class Numbers // para el body de la peticion POST
    {
        public decimal A { get; set; }
        public decimal B { get; set; }
    }
}

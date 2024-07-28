using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace First_Backend_dotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public IActionResult GetSync() 
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            Thread.Sleep(1000);
            Console.WriteLine("Conexion a base de datos terminada");

            Thread.Sleep(1000);
            Console.WriteLine("Envio de email terminado");

            stopwatch.Stop();
            Console.WriteLine("Todo ha terminado...");

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async")]
        public async Task<IActionResult> GetAsync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();

            var task1 = new Task<int>(() => // los task sirven para hacer operaciones asincronas (tareas) en paralelo
            {
                Thread.Sleep(1000);
                Console.WriteLine("Conexion a base de datos terminada");
                return 1;
            });

            var task2 = new Task<int>(() => // los task sirven para hacer operaciones asincronas (tareas) en paralelo
            {
                Thread.Sleep(1000);
                Console.WriteLine("Envio de email terminado");
                return 2;
            });

            task1.Start(); // Inicia el task
            task2.Start();

            Console.WriteLine("Hago otra cosa");

            var result1 = await task1; // Espera a que termine el task (no hace nada hasta que termine)
            var result2 = await task2;

            Console.WriteLine("Todo ha terminado");

            stopwatch.Stop();

            return Ok(result1 + " " +  result2 + " " + stopwatch.Elapsed);
        }
   
    }
}

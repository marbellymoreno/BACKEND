using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SomeController : ControllerBase
    {
        [HttpGet("sync")]
        public async Task<IActionResult> getSync()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Conexion Finalizada");

            Thread.Sleep(1000);
            Console.WriteLine("Conexion Envio Finalizado");
            stopwatch.Stop();

            return Ok(stopwatch.Elapsed);
        }

        [HttpGet("async1")]
        public async Task<IActionResult> GetAsync1()
        {
            var task1 = new Task(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("Finalizacion de la tarea asincrona");
            });

            task1.Start();
            // Tarea que esta fuera del segmento asincrono

            Console.WriteLine("Finalizacion de la tarea NoAsincrona");
            await task1;
            Console.WriteLine("Finalizacion de la tarea NoAsincrona terminado");

            return Ok();
        }

        [HttpGet("async2")]
        public async Task<IActionResult> GetAsync2()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var task1 = new Task<int>(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Finalizacion de la tarea asincrona 1");
                return 8;
            });

            var task2 = new Task<int>(() =>
            {
                Thread.Sleep(5000);
                Console.WriteLine("Finalizacion de la tarea asincrona 2");
                return 1;
            });

            task1.Start();
            task2.Start();

            int result1 = await task1;
            int result2 = await task2;

            stopwatch.Stop();

            return Ok($"{result1} {result2} {stopwatch.Elapsed}");
        }
    }
}
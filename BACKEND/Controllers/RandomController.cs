using BACKEND.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomController : ControllerBase
    {
        private IRandomServices _singleton;
        private IRandomServices _rScoped;
        private IRandomServices _rTransient;

        private IRandomServices _singleton2;
        private IRandomServices _rScoped2;
        private IRandomServices _rTransient2;

        public RandomController
        (
            [FromKeyedServices("randomSingleton")] IRandomServices randomSingleton,
            [FromKeyedServices("randomScoped")] IRandomServices randomScoped,
            [FromKeyedServices("randomTransient")] IRandomServices randomTransient,
            [FromKeyedServices("randomSingleton")] IRandomServices randomSingleton2,
            [FromKeyedServices("randomScoped")] IRandomServices randomScoped2,
            [FromKeyedServices("randomTransient")] IRandomServices randomTransient2
        )
        {
            _singleton = randomSingleton;
            _rScoped = randomScoped;
            _rTransient = randomTransient;

            _singleton2 = randomSingleton2;
            _rScoped2 = randomScoped2;
            _rTransient2 = randomTransient2;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();

            result.Add("Singleton 1", _singleton.Value);
            result.Add("Scope 1", _rScoped.Value);
            result.Add("Transient 1", _rTransient.Value);

            result.Add("Singleton 2", _singleton2.Value);
            result.Add("Scope 2", _rScoped2.Value);
            result.Add("Transient 2", _rTransient2.Value);

            return result;
        }
    }
}

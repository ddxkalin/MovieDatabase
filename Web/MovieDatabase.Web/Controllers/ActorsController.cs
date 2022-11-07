namespace MovieDatabase.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;

    using MovieDatabase.Data.Models;
    using MovieDatabase.Services.Contracts;
    using MovieDatabase.Web.Controllers.Base;

    public class ActorsController : BaseController
    {
        private readonly ICrudService<Actor> actorService;

        public ActorsController(ICrudService<Actor> actorService)
        {
            this.actorService = actorService;
        }

        [HttpGet]
        [Route("actors/get")]
        public IActionResult GetActors(string name)
        {
            var actors = this.actorService.GetAll()
                .Where(a => a.Name.ToLower().Contains(name.ToLower()))
                .Select(a => new { a.Id, a.Name })
                .ToList();

            return this.Json(actors);
        }
    }
}

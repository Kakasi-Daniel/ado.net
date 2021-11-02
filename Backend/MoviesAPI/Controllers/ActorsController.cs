using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ActorsService actorsService;

        public ActorsController(ActorsService actorsService)
        {
            this.actorsService = actorsService;
        }

        

        [HttpGet]
        public async Task<ActionResult<List<ActorModel>>> GetActors()
        {
            return await actorsService.GetActorsAsync();
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetActorByID(int id)
        {
            var actor = await actorsService.GetActorByIDAsync(id);

            return actor.Name != null ? Ok(actor) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostActor([FromBody] ActorModel actor)
        {
            int id = await actorsService.PostActorAsync(actor);
            actor.ID = id;
            return Ok(actor);
        }
    }
}

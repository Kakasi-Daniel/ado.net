using MoviesLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesBLL;

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
        public async Task<ActionResult<List<ActorOut>>> GetActors()
        {
            return await actorsService.GetActorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActorOut>> GetActorByID(int id)
        {
            var actor = await actorsService.GetActorByIDAsync(id);

            return actor != null ? Ok(actor) : NotFound();
        }
        
        [HttpGet("movie/{movieId}")]
        public async Task<List<ActorsRolesOut>> GetActorsFromMovie([FromRoute]int movieId)
        {
            return await actorsService.GetActorsFromMovie(movieId);
        }

        [HttpPost]
        public async Task<ActionResult<ActorOut>> PostActor([FromBody] ActorIn actor)
        {
            int id = await actorsService.PostActorAsync(actor);
            return await GetActorByID(id);
        }

       

        [HttpPut("{id}")]
        public async Task<ActionResult<ActorOut>> UpdateActor([FromRoute] int id, [FromBody] ActorIn actor)
        {
            await actorsService.UpdateActorAsync(id, actor);
            return await actorsService.GetActorByIDAsync(id);

        }

        [HttpDelete]
        public async Task DeleteActor(int id)
        {
            await actorsService.DeleteActorAsync(id);
        }

    }
}

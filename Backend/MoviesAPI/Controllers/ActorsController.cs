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
        public async Task<ActionResult<ActorModel>> GetActorByID(int id)
        {
            var actor = await actorsService.GetActorByIDAsync(id);

            return actor != null ? Ok(actor) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ActorModel>> PostActor([FromBody] ActorModel actor)
        {
            int id = await actorsService.PostActorAsync(actor);
            return await GetActorByID(id);
        }

       

        [HttpPut("{id}")]
        public async Task<ActionResult<ActorModel>> UpdateActor([FromRoute] int id, [FromBody] ActorModel actor)
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

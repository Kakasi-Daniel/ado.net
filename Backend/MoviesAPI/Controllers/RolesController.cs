using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesLibrary.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesService roleService;

        public RolesController(RolesService roleService)
        {
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleOut>>> GetRoles()
        {
            return await roleService.GetRolesAsync();
        }

        [HttpGet]
        [Route("paged/{pageSize}/{pageNumber}")]
        public async Task<ActionResult<PaginationResult<RoleOutWithNames>>> GetAllMoviesPaginated(int pageSize, int pageNumber)
        {
            var roles = await roleService.GetPaginated(pageSize > 10 ? 10 : pageSize, pageNumber < 1 ? 1 : pageNumber);

            return roles;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleOutWithNames>> GetRole([FromRoute] int id)
        {
            var role = await roleService.GetRoleByIdAsync(id);

            return role != null ? Ok(role) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleOutWithNames>> AddRole([FromBody] RoleIn role)
        {
            int id = await roleService.AddRoleAsync(role);

            return await GetRole(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleOutWithNames>> UpdateRating([FromRoute] int id, [FromBody] RoleIn role)
        {
            await roleService.UpdateRoleAsync(id, role);

            return await GetRole(id);
        }

        [HttpDelete("{id}")]
        public async Task DeleteRating([FromRoute] int id)
        {
            await roleService.DeleteRoleAsync(id);
        }



    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesBLL.Services;
using MoviesLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleOut>> GetRole([FromRoute] int id)
        {
            var role = await roleService.GetRoleByIdAsync(id);

            return role.Name != null ? Ok(role) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<RoleOut>> AddRole([FromBody] RoleAddIn role)
        {
            int id = await roleService.AddRoleAsync(role);

            return await GetRole(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RoleOut>> UpdateRating([FromRoute] int id, [FromBody] RoleUpdateIn role)
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

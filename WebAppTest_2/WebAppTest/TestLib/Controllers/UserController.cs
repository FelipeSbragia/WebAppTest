using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TestLib.Data;
using TestLib.Models;

namespace TesteLib.Controllers
{
    [ApiController]
    [Route("v1/Users")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<User>>> Get([FromServices] DataContext context)
        {
            var User = await context.Users
                .ToListAsync();
            return User;
        }

        [HttpGet]
        [Route("{clientId:int}")]
        public async Task<ActionResult<User>> Get([FromServices] DataContext context, int clientId)
        {

            var User = await context.Users
                .FirstOrDefaultAsync(x => x.ClientId == clientId);

            return User;
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> Put([FromServices] DataContext context, [FromBody] User model, int id)
        {
            var User = await context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (User == null || User.Id != id)
            {
                return BadRequest();
            }

            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return User;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> Delete([FromServices] DataContext context, int id)
        {
            var User = await context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            context.Users.Remove(User);
            await context.SaveChangesAsync();
            return User;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] User model)
        {
            var User = await context.Users
              .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (User != null)
            {
                throw new System.ArgumentException($"User Id: {model.Id} already exists", "id");
            }

            User = await context.Users
              .FirstOrDefaultAsync(x => x.ClientId == model.ClientId);

            if (User != null)
            {
                throw new System.ArgumentException($"User for Client Id: {model.ClientId} already exists", "clientId");
            }

            var Client = await context.Clients
              .FirstOrDefaultAsync(x => x.Id == model.ClientId);

            if (Client == null)
            {
                throw new System.ArgumentException($"Client Id: {model.ClientId} not exists", "clientId");
            }

            if (ModelState.IsValid)
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, User);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

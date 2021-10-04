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
    [Route("v1/Clients")]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Client>>> Get([FromServices] DataContext context)
        {
            var Client = await context.Clients
                .ToListAsync();
            return Client;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Client>> Get([FromServices] DataContext context, int id)
        {

            var Client = await context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);

            return Client;
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Client>> Put([FromServices] DataContext context, [FromBody] Client model, int id)
        {
            var Client = await context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);

            if (Client == null || Client.Id != id)
            {
                return BadRequest();
            }

            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Client;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Client>> Delete([FromServices] DataContext context, int id)
        {
            var Client = await context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);

            context.Clients.Remove(Client);
            await context.SaveChangesAsync();
            return Client;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] Client model)
        {
            var Client = await context.Clients
              .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (Client != null)
            {
                throw new System.ArgumentException($"Client Id: {model.Id} already exists", "id");
            }

            if (ModelState.IsValid)
            {
                context.Clients.Add(model);
                await context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, Client);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

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
    [Route("v1/ClientsLimits")]
    public class ClientLimitsController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ClientLimits>>> Get([FromServices] DataContext context)
        {
            var ClientLimits = await context.ClientsLimits
                .ToListAsync();
            return ClientLimits;
        }

        [HttpGet]
        [Route("{clientId:int}")]
        public async Task<ActionResult<ClientLimits>> Get([FromServices] DataContext context, int clientId)
        {

            var ClientLimits = await context.ClientsLimits
                .FirstOrDefaultAsync(x => x.ClientId == clientId);

            return ClientLimits;
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ClientLimits>> Put([FromServices] DataContext context, [FromBody] ClientLimits model, int id)
        {
            var ClientLimits = await context.ClientsLimits
                .FirstOrDefaultAsync(x => x.Id == id);

            if (ClientLimits == null || ClientLimits.Id != id)
            {
                return BadRequest();
            }

            context.Entry(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return ClientLimits;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ClientLimits>> Delete([FromServices] DataContext context, int id)
        {
            var ClientLimits = await context.ClientsLimits
                .FirstOrDefaultAsync(x => x.Id == id);

            context.ClientsLimits.Remove(ClientLimits);
            await context.SaveChangesAsync();
            return ClientLimits;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(
            [FromServices] DataContext context,
            [FromBody] ClientLimits model)
        {
            var ClientLimits = await context.ClientsLimits
              .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (ClientLimits != null)
            {
                throw new System.ArgumentException($"ClientLimits Id: {model.Id} already exists", "id");
            }

            ClientLimits = await context.ClientsLimits
              .FirstOrDefaultAsync(x => x.ClientId == model.ClientId);

            if (ClientLimits != null)
            {
                throw new System.ArgumentException($"ClientLimits for Client Id: {model.ClientId} already exists", "clientId");
            }

            var Client = await context.Clients
              .FirstOrDefaultAsync(x => x.Id == model.ClientId);

            if (Client == null)
            {
                throw new System.ArgumentException($"Client Id: {model.ClientId} not exists", "clientId");
            }

            if (ModelState.IsValid)
            {
                context.ClientsLimits.Add(model);
                await context.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created, ClientLimits);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}

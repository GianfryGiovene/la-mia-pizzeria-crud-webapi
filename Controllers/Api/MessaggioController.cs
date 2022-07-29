using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LaMiaPizzeria.Controllers.Api
{
    [Route("api/messaggio")]
    [ApiController]
    public class MessaggioController : ControllerBase
    {
        public IActionResult Get(string? search)
        {
            List<Messaggio> messagesList;
            using(PizzaContext ctx = new PizzaContext())
            {
                if(search == null)
                {
                    messagesList = ctx.MessaggioList.ToList();
                }
                else
                {
                    messagesList = ctx.MessaggioList.Where(p => p.Title.Contains(search)).ToList();
                }
                return Ok(messagesList);
            }
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                Messaggio message = ctx.MessaggioList.Where(p => p.Id == id).FirstOrDefault();

                if(message == null)
                {
                    return NotFound();
                }
                return Ok(message);
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] Messaggio message)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            using (PizzaContext ctx = new PizzaContext())
            {
                ctx.MessaggioList.Add(message);
                ctx.SaveChanges();
                return Ok();
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Messaggio message)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            using (PizzaContext ctx = new PizzaContext())
            {
                Messaggio messageToEdit = ctx.MessaggioList.Where(p => p.Id == id).FirstOrDefault();
                if(messageToEdit != null)
                {
                    messageToEdit.Title = message.Title;
                    message.TextMessage = message.TextMessage;
                    ctx.SaveChanges();
                    return Ok(messageToEdit);
                }
                else
                {
                    return NotFound();
                }
                return Ok();
            }
            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                Messaggio mexToDelete = ctx.MessaggioList.Where(p => p.Id == id).FirstOrDefault();
                if(mexToDelete != null)
                {
                    ctx.MessaggioList.Remove(mexToDelete);

                    ctx.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }
       
    }
}

using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


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
    }
}

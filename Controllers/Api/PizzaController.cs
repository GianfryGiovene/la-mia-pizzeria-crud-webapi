using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.Api
{
    [Route("api/pizza")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        
        public IActionResult Get()
        
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                List<Pizza> pizzaList = ctx.PizzaList.ToList<Pizza>();

                return Ok(pizzaList);
            }            
        }

    }
}

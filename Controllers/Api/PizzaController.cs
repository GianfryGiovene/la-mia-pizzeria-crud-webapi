using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Controllers.Api
{
    [Route("api/pizza")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        
        public IActionResult Get(string? search)
        
        {
            using(PizzaContext ctx = new PizzaContext())
            {
                List<Pizza> pizzaList;
                if (search == null)
                {
                    pizzaList = ctx.PizzaList.Include(p => p.Category).ToList();
                }
                else
                {
                    pizzaList = ctx.PizzaList.Where(p => p.Name.Contains(search)).Include(p => p.Category).ToList<Pizza>();
                }



                return Ok(pizzaList);
            }            
        }

    }
}

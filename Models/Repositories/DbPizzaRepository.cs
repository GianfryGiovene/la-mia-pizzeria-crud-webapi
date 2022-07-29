using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LaMiaPizzeria.Models.Repositories
{
    public class DbPizzaRepository : IPizzaRepository
    {
        public void Create(Pizza pizza, List<string> selectedIngredients)
        {
            using(PizzaContext ctx = new PizzaContext())
                    {
                pizza.IngredienteList = new List<Ingrediente>();
                if(selectedIngredients != null)
                {
                    foreach(string selectedIng in selectedIngredients)
                    {
                        int selectedIntIngId = int.Parse(selectedIng);
                        Ingrediente ing = ctx.IngredienteList.Where(p=> p.Id == selectedIntIngId).FirstOrDefault();

                        pizza.IngredienteList.Add(ing);
                    }
                }

                ctx.Add(pizza);
                ctx.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Pizza GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pizza> GetList()
        {
            using (PizzaContext db = new PizzaContext())
            {
                IQueryable<Pizza> pizzaList = db.PizzaList.Include(p => p.Category);
                return pizzaList.ToList();
            }
        }

        public List<Pizza> GetListByFilter(string search)
        {
            using (PizzaContext ctx = new PizzaContext())
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
                return pizzaList;
            }
        }

        public void Update(Helper model)
        {
            throw new NotImplementedException();
        }

        private List<SelectListItem> GetIngredientsList()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<SelectListItem> ingredientsList = new List<SelectListItem>();
                List<Ingrediente> ingredients = db.IngredienteList.ToList();

                foreach (Ingrediente ingredient in ingredients)
                {
                    ingredientsList.Add(new SelectListItem() { Text = ingredient.Name, Value = ingredient.Id.ToString() });
                }

                return ingredientsList;
            }
        }
    }
}

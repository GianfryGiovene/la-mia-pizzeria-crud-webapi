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
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizza = db.PizzaList.Where(p => p.Id == id).FirstOrDefault();

                if (pizza != null)
                {
                    db.PizzaList.Remove(pizza);
                    db.SaveChanges();
                }
            }
        }

        public Pizza GetById(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza pizza = db.PizzaList.Where(p => p.Id == id).Include(c => c.Category).FirstOrDefault();

                if (pizza == null)
                {
                    return null;
                }
                else
                {
                    db.Entry(pizza).Collection("IngredienteList").Load();

                    return pizza;
                }

            }
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
                List<Pizza> pizzaList = ctx.PizzaList.Where(p => p.Name.Contains(search)).Include(p => p.Category).ToList<Pizza>();
                    
                return pizzaList;
            }
        }

        public void Update(Helper model)
        {
            throw new NotImplementedException();
        }



        //private List<SelectListItem> GetIngredientsList()
        //{
        //    using (PizzaContext db = new PizzaContext())
        //    {
        //        List<SelectListItem> ingredientsList = new List<SelectListItem>();
        //        List<Ingrediente> ingredients = db.IngredienteList.ToList();

        //        foreach (Ingrediente ingredient in ingredients)
        //        {
        //            ingredientsList.Add(new SelectListItem() { Text = ingredient.Name, Value = ingredient.Id.ToString() });
        //        }

        //        return ingredientsList;
        //    }
        //}
    }
}

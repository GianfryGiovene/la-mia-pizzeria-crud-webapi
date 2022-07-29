using LaMiaPizzeria.Data;
using LaMiaPizzeria.Models;
using LaMiaPizzeria.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace LaMiaPizzeria.Controllers
{
    [Authorize]
    public class PizzaController : Controller
    {
        private DbPizzaRepository PizzaRepository;
        //************* INDEX VIEW ***************
        public PizzaController()
        {
            this.PizzaRepository = new DbPizzaRepository();
        }

        [HttpGet]
        public IActionResult Index(string? search)
        {
            List<Pizza> pizzaList;
            if(search == null)
            {
                pizzaList = this.PizzaRepository.GetList();
            }
            else
            {
                pizzaList = this.PizzaRepository.GetListByFilter(search);
            }

            

            return View("Index", pizzaList);
            
        }


        //************* DETAILS VIEW ***************

        public IActionResult Details(int id)
        {
            Pizza pizza = PizzaRepository.GetById(id);
            if(pizza == null)
            {
                return NotFound();
            }
            else
            {
                return View(pizza);
            }

        }

        //************* GET CREATE VIEW ***************
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext db = new PizzaContext())
            {
                List<Categoria> categories = db.CategoriaList.ToList();
                Helper model = new Helper();

                model.Pizza = new Pizza();
                model.CategoryList = categories;
                model.IngredientiList = GetIngredientsList();

                return View("Create", model);
            }
        }

        //************* POST CREATE VIEW ***************
        [HttpPost]      
        [ValidateAntiForgeryToken]
        public IActionResult Create(Helper model)
        {
            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Categoria> categoryList = db.CategoriaList.ToList();

                    model.CategoryList = categoryList;
                    model.IngredientiList = GetIngredientsList();

                    return View("Create", model);
                }

            }
            this.PizzaRepository.Create(model.Pizza, model.SelectedIngredienti);
            return RedirectToAction("Index");

        } 

        //************* GET UPDATE VIEW ***************
        [HttpGet]
        public IActionResult Update(int id)
        {
            using(PizzaContext db = new PizzaContext())
            {
                Pizza editPizza = db.PizzaList.Where(p => p.Id == id).Include(p =>p.Category).Include(p=>p.IngredienteList).FirstOrDefault();
                if(editPizza == null)
                {
                    return NotFound();
                }  
                else
                {

                    Helper model = new Helper();

                    model.Pizza = editPizza;
                    model.CategoryList = db.CategoriaList.ToList();
                    model.SelectedIngredienti = new List<string>();

                    foreach(Ingrediente ing in editPizza.IngredienteList)
                    {
                        model.SelectedIngredienti.Add(ing.Id.ToString());
                    }
                    
                    model.IngredientiList = GetIngredientsList();

                    return View(model);
                }
            }
        }

        //************* POST UPDATE VIEW ***************
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Helper model)
        {

            if (!ModelState.IsValid)
            {
                using (PizzaContext db = new PizzaContext())
                {
                    List<Categoria> categoryList = db.CategoriaList.ToList();

                    model.CategoryList = categoryList;
                    model.IngredientiList = GetIngredientsList();

                    return View("Update", model);
                }

            }
            
            Pizza pizzaToEdit = PizzaRepository.GetById(id);

            if(pizzaToEdit != null)
            {
                pizzaToEdit.Name = model.Pizza.Name;
                pizzaToEdit.Description = model.Pizza.Description;
                pizzaToEdit.CategoryId = model.Pizza.CategoryId;
                pizzaToEdit.Price = model.Pizza.Price;

                PizzaRepository.Update(pizzaToEdit, model.SelectedIngredienti);
                return RedirectToAction("index");
            }
            else
            {
                return NotFound();
            }
        }

        //************* DELETE VIEW ***************
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            this.PizzaRepository.Delete(id);
            return RedirectToAction("Index");                
            
        }



        static List<SelectListItem> GetIngredientsList()
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

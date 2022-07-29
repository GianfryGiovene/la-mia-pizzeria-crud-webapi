using LaMiaPizzeria.Models.Repositories.Interfaces;

namespace LaMiaPizzeria.Models.Repositories
{
    public class InMemoryPizzaRepository : IPizzaRepository
    {
        private static List<Pizza> PizzaList = new List<Pizza>();

        public Pizza GetById(int id)
        {
            Pizza pizzaToFind = null;
            for (int i = 0; i < InMemoryPizzaRepository.PizzaList.Count; i++)
            {
                Pizza pizzaToCheck = InMemoryPizzaRepository.PizzaList[i];
                if (pizzaToCheck.Id == id)
                {
                    pizzaToFind = pizzaToCheck;
                    return pizzaToFind;
                }
            }

            return null;
        }

        public List<Pizza> GetList()
        {
            return InMemoryPizzaRepository.PizzaList;
        }

        public List<Pizza> GetListByFilter(string search)
        {
            throw new NotImplementedException();
        }

        public void Create(Pizza pizza, List<string> ingList)
        {
            pizza.Id = PizzaList.Count();
            pizza.Category = new Categoria();
            pizza.IngredienteList = new List<Ingrediente>();

            InMemoryPizzaRepository.PizzaList.Add(pizza);
        }

        public void Update(Pizza pizza, List<string> ingList)
        {
            int indexToFind = -1;
            
            for(int i= 0; i< InMemoryPizzaRepository.PizzaList.Count();i++)
            {
                Pizza pizzaToEdit = InMemoryPizzaRepository.PizzaList[i];
                if(pizzaToEdit.Id == pizza.Id)
                {
                    indexToFind = i;
                }
            }
            if(indexToFind != -1)
            {
                InMemoryPizzaRepository.PizzaList[indexToFind] = pizza;
            }
        }

        public void Delete(Pizza pizza)
        {
            int indexToDelete = -1;
            for (int i = 0; i < InMemoryPizzaRepository.PizzaList.Count; i++)
            {
                Pizza pizzaToCheck = InMemoryPizzaRepository.PizzaList[i];
                if (pizzaToCheck.Id == pizza.Id)
                {
                    indexToDelete = i;
                }
            }
            if (indexToDelete != -1)
            {
                InMemoryPizzaRepository.PizzaList.RemoveAt(indexToDelete);
            }
        }


    }
}

﻿namespace LaMiaPizzeria.Models.Repositories.Interfaces
{
    public interface IPizzaRepository
    {
        public List<Pizza> GetList();

        public List<Pizza> GetListByFilter(string search);

        public Pizza GetById(int id);

        public void Create(Pizza pizza, List<string> ingList);

        public void Update(Pizza pizza, List<string> ingList);

        public void Delete(Pizza pizza);
    }
}

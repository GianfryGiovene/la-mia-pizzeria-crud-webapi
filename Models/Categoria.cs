using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LaMiaPizzeria.Models
{
    
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        public List<Pizza>? PizzaList { get; set; }


        public Categoria()
        {

        }
    }
}

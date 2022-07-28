using System.ComponentModel.DataAnnotations;

namespace LaMiaPizzeria.Models
{
    public class Messaggio
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string TextMessage { get; set; }


        public Messaggio()
        {

        }

    }
}

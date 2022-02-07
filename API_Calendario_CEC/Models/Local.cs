using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Models
{
    public class Local
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo capacidade é obrigatório!")]
        public int Capacidade { get; set; }

        public bool Sistemas { get; set; }
    }
}

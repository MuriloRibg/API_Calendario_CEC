using System;
using System.ComponentModel.DataAnnotations;

namespace API_Calendario_CEC.Models
{
    public class PilaresInstrutor
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Id_Instrutor é obrigatório!")]
        public int Id_Instrutor { get; set; }

        [Required(ErrorMessage = "O campo Id_Pilar é obrigatório!")]
        public int Id_Pilar { get; set; }

        public virtual Pilar Pilar { get; set; }

        public virtual Instrutor Instrutor { get; set; }
    }
}

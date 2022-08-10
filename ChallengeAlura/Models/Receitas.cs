using System.ComponentModel.DataAnnotations;

namespace ChallengeAlura.Models
{
    public class Receitas
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
    }
}

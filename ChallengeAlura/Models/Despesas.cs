using System.ComponentModel.DataAnnotations;

namespace ChallengeAlura.Models
{
    public class Despesas
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
        public Categorias Categoria {get;set;}

        public enum Categorias
        {
            None,
            Alimentação,
            Saúde,
            Moradia,
            Transporte,
            Educação,
            Lazer,
            Imprevistos,
            Outras
        }
    }
}

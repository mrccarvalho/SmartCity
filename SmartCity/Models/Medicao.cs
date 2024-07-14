using System.ComponentModel.DataAnnotations;

namespace SmartCity.Models
{
    public class Medicao
    {
        [Required]
        public int MedicaoId { get; set; }

        [Required]
        public int EstacionamentoId { get; set; }
        [Required]
        public decimal ValorLido { get; set; }
        [Required]
        public System.DateTime DataMedicao { get; set; }
        public virtual Estacionamento Estacionamento { get; set; }

    }
}

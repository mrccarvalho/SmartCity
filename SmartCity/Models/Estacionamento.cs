using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace SmartCity.Models
{
    public class Estacionamento
    {
        [Required]
        public int EstacionamentoId { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public bool Estado { get; set; }
        public virtual ICollection<Medicao> Medicoes { get; set; }
    }

    public enum TipoMedicaoEnum
    {
        Estacionamento1 = 1,
        Estacionamento2 = 2,
        Estacionamento3 = 3,
        Estacionamento4 = 4
    }
}

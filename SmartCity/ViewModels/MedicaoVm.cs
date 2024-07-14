namespace SmartCity.ViewModels
{
    public class MedicaoVm
    {
        public int EstacionamentoId1 { get; set; }
        public int EstacionamentoId2 { get; set; }
        public int EstacionamentoId3 { get; set; }
        public int EstacionamentoId4 { get; set; }
        public DateTime? DataMedicao { get; set; }
        public decimal? Estacionamento1 { get; set; }
        public decimal? Estacionamento2 { get; set; }
        public decimal? Estacionamento3 { get; set; }
        public decimal? Estacionamento4 { get; set; }


        public string DateOnlyString => DataMedicao?.ToString("yyyy-MM-dd") ?? string.Empty;
        public string TimeOnlyString => DataMedicao?.ToString("hh:mm:ss tt") ?? string.Empty;


        public string Estacionamento1String => (Estacionamento1 != null) ? Estacionamento1.Value.ToString("###.0") : "0.0";
        public string Estacionamento2String => (Estacionamento2 != null) ? Estacionamento2.Value.ToString("###.0") : "0.0";
        public string Estacionamento3String => (Estacionamento3 != null) ? Estacionamento3.Value.ToString("###.0") : "0.0";
        public string Estacionamento4String => (Estacionamento4 != null) ? Estacionamento4.Value.ToString("###.0") : "0.0";

    }
}

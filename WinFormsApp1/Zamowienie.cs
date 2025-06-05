using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsApp1
{
    public class Zamowienie
    {
        public int id { get; set; }
        public string imieklienta { get; set; }
        public string numertelefonu { get; set; }
        public int pizzaid { get; set; }

        private DateTime _datazamowienia;
        public DateTime datazamowienia
        {
            get => _datazamowienia;
            set => _datazamowienia = DateTime.SpecifyKind(value, DateTimeKind.Unspecified); // Dla timestamp without time zone
        }

        public string status { get; set; }
        public Pizza Pizza { get; set; }

        [NotMapped]
        public string NazwaPizzy => Pizza?.nazwa ?? "Brak pizzy";
    }
}

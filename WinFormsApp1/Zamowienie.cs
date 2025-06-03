namespace WinFormsApp1
{
    public class Zamowienie
    {
        public int id { get; set; }
        public string imieklienta { get; set; }
        public string numertelefonu { get; set; }
        public int pizzaid { get; set; }
        public DateTime datazamowienia { get; set; }
        public string status { get; set; }

       
        public Pizza Pizza { get; set; }
    }
}

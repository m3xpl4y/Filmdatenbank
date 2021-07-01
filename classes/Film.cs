namespace csharp.FilmDatenbank.classes
{
    public class Film
    {
        public int ID { get; set; }
        
        public string Title { get; set; }
        
        public string Jahr { get; set; }
        
        public string ProduktionsFirma { get; set; }

        public Film()
        {
            
        }

        public Film(int iD, string title, string jahr, string produktionsFirma)
        {
            ID = iD;
            Title = title;
            Jahr = jahr;
            ProduktionsFirma = produktionsFirma;
        }
    }
}
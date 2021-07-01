namespace csharp.FilmDatenbank.classes
{
    public class Genre
    {
        public int ID { get; set; }
        public string GenreName { get; set; }


        public Genre(int iD, string genreName)
        {
            ID = iD;
            GenreName = genreName;
        }

        public Genre()
        {
        }
    }
}
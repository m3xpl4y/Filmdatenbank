using System;
using System.Collections.Generic;
using System.IO;
using csharp.FilmDatenbank.classes;
using MySql.Data.MySqlClient;

namespace FilmDatenbank
{
    class Program
    {
        static void Main(string[] args)
        {
            CountWords(SplitGenreMethod(ReadGenres()));
            
            #region INSERT MOVIE
            // List<string> stringListe = ResultsListen();
            // List<Film> filmListe = SplitMethod(stringListe);
            // WriteToDB(filmListe);
            #endregion
        }

        #region INSERT GENRE

        public static List<string> ReadGenres()
        {
            List<string> readListCsv = new List<string>();
            string fileName = @"C:\Users\DCV\OneDrive\vscode_max\Sonstiges\genre.csv";
            foreach (var line in File.ReadAllLines(fileName))
            {
                readListCsv.Add(line);
            }
            return readListCsv;
        }

        public static List<Genre> SplitGenreMethod(List<string> stringListe)
        {
            List<Genre> genreListe = new List<Genre>();
            foreach (var line in stringListe)
            {
                string[] splits = line.Split(',');
                
                foreach (var split in splits)
                {
                    Genre genre = new Genre();
                    genre.GenreName = split.Trim().ToLower();
                    
                    genreListe.Add(genre);
                    System.Console.WriteLine(split.Trim().ToLower());
                }
            }
            return genreListe;
        }

        private static void CountWords(List<Genre> gerneListe)
        {
            int check =0;
            List<string> genreList = new List<string>();
            foreach(Genre genre in gerneListe)
            {
                
                if(!genreList.Contains(genre.GenreName))
                {
                    check++;
                    genreList.Add(genre.GenreName);
                    WriteToFile(genre.GenreName);
                    System.Console.WriteLine(check);
                }
                else{
                    System.Console.WriteLine("Nicht Vorhanden!");
                }
                
            }
            System.Console.WriteLine("CHECK: " + check);
            System.Console.WriteLine("Count: " + genreList.Count);
        }

        #endregion

        #region INSERT MOVIES IN DATBASE
        public static List<string> ResultsListen()
        {
            int check = 0;
            List<string> resultsListen = new List<string>();
            string fileName = @"C:\Users\DCV\OneDrive\vscode_max\Sonstiges\filmdatenbank.csv";

            foreach(string line in File.ReadAllLines(fileName))
            {
                check++;
                resultsListen.Add(line);
                //System.Console.WriteLine(check);
            }
            
            return resultsListen;
        }

        public static List<Film> SplitMethod(List<string> stringListe)
        {
            
            int check = 0;
            // string fileName = @"C:\Users\DCV\OneDrive\vscode_max\Sonstiges\filmdatenbank.csv";
            // //string[] lines =  File.ReadAllLines(fileName);
            // StreamReader sr = new StreamReader(fileName);
            // string lines = sr.ReadLine();
            List<Film> filmListe = new List<Film>();
            foreach(string lines in stringListe)
            {
                check++;
                string[] splits = lines.Split(';');
                Film film = new Film();
                try
                {
                    film.ID = Convert.ToInt32(splits[0]);
                    film.Title = splits[1];
                    film.Jahr = splits[2];
                    film.ProduktionsFirma = splits[3];
                }
                catch (System.Exception ex)
                {
                     System.Console.WriteLine(ex);
                }
                System.Console.WriteLine(check + " " + film.Title + " _____ " + film.Jahr);
                filmListe.Add(film);
            }
            return filmListe;
        } 
        public static void WriteToDB(List<Film> filmListe)
        {
            int check = 0;
            foreach(Film film in filmListe)
            {
                check++;
                System.Console.WriteLine(check);
                
                    MySqlConnection conn = new MySqlConnection("SERVER=localhost;DATABASE=filmDB;UID=adm;PASSWORD=123456;port=3306");
                    
                    //string query = "insert into filmDB.film (id, title, jahr, produktionsfirma) values ('" + film.ID + "', '" + film.Title+ "', '" + film.Jahr + "','" + film.ProduktionsFirma + "')";
                    conn.Open();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "insert into film(id, title, jahr, produktionsfirma) values(?1,?2,?3,?4)";
                    cmd.Parameters.Add("?1",MySqlDbType.Int32).Value = film.ID;
                    cmd.Parameters.Add("?2",MySqlDbType.VarChar).Value = film.Title;
                    cmd.Parameters.Add("?3",MySqlDbType.VarChar).Value = film.Jahr;
                    cmd.Parameters.Add("?4",MySqlDbType.VarChar).Value = film.ProduktionsFirma;
                    cmd.Prepare();
                    MySqlDataReader mySqlDataReader;

                    mySqlDataReader = cmd.ExecuteReader();
                    // while(mySqlDataReader.Read())
                    // {
                        
                    // }
                    conn.Close();
            }
        }
        #endregion


        //
        private static void WriteToFile(string genre)
        {
            string path = @"C:\Users\DCV\OneDrive\vscode_max\Sonstiges\genreAusgabe2.csv";
            File.AppendAllTextAsync(path, genre + Environment.NewLine);
        }

    }
}

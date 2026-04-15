using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BibliothequeApp.Models;

namespace BibliothequeApp.Data
{
    /// <summary>
    /// Gère toutes les interactions avec la table Books de LibraryDB.
    /// Principe SRP : cette classe ne fait qu'accéder aux données.
    /// </summary>
    public class BookRepository
    {
        // ----------------------------------------------------------------
        // CHAÎNE DE CONNEXION — à modifier selon votre environnement MySQL
        // ----------------------------------------------------------------
        private const string ConnectionString =
            "server=localhost;port=3306;user=root;password=;database=LibraryDB;CharSet=utf8mb4;";

        // ----------------------------------------------------------------
        // Helpers privés
        // ----------------------------------------------------------------

        private MySqlConnection OpenConnection()
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }

        /// <summary>Hydrate un objet Book à partir d'un reader positionné sur une ligne.</summary>
        private static Book MapBook(MySqlDataReader r) => new Book
        {
            Id         = r.GetInt32("Id"),
            Titre      = r.GetString("Titre"),
            Auteur     = r.GetString("Auteur"),
            ISBN       = r.GetString("ISBN"),
            AnneePubli = r.GetInt32("AnneePubli"),
            Genre      = r.GetString("Genre"),
            Rayon      = r.GetString("Rayon"),
            Etagere    = r.GetString("Etagere"),
            Disponible = r.GetBoolean("Disponible"),
            Couverture = r.IsDBNull(r.GetOrdinal("Couverture")) ? "" : r.GetString("Couverture"),
            Emprunteur = r.IsDBNull(r.GetOrdinal("Emprunteur")) ? "" : r.GetString("Emprunteur"),
            DateEmprunt = r.IsDBNull(r.GetOrdinal("DateEmprunt")) ? (DateTime?)null : r.GetDateTime("DateEmprunt"),
        };

        // ----------------------------------------------------------------
        // CRUD
        // ----------------------------------------------------------------

        /// <summary>Retourne tous les livres de la bibliothèque.</summary>
        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            try
            {
                using var conn = OpenConnection();
                using var cmd  = new MySqlCommand(
                    "SELECT Id, Titre, Auteur, ISBN, AnneePubli, Genre, Rayon, Etagere, Disponible, Couverture, Emprunteur, DateEmprunt FROM Books ORDER BY Titre;",
                    conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    books.Add(MapBook(reader));
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur MySQL lors de la lecture des livres : {ex.Message}", ex);
            }
            return books;
        }

        /// <summary>Insère un nouveau livre et retourne son Id généré.</summary>
        public int AddBook(Book book)
        {
            try
            {
                using var conn = OpenConnection();
                const string sql =
                    "INSERT INTO Books (Titre, Auteur, ISBN, AnneePubli, Genre, Rayon, Etagere, Disponible, Couverture, Emprunteur, DateEmprunt) " +
                    "VALUES (@titre, @auteur, @isbn, @annee, @genre, @rayon, @etagere, @dispo, @couverture, @emprunteur, @dateEmprunt);";
                using var cmd = new MySqlCommand(sql, conn);
                BindParams(cmd, book);
                cmd.ExecuteNonQuery();
                return (int)cmd.LastInsertedId;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur MySQL lors de l'ajout du livre : {ex.Message}", ex);
            }
        }

        /// <summary>Met à jour un livre existant identifié par son Id.</summary>
        public void UpdateBook(Book book)
        {
            try
            {
                using var conn = OpenConnection();
                const string sql =
                    "UPDATE Books SET Titre=@titre, Auteur=@auteur, ISBN=@isbn, AnneePubli=@annee, " +
                    "Genre=@genre, Rayon=@rayon, Etagere=@etagere, Disponible=@dispo, " +
                    "Couverture=@couverture, Emprunteur=@emprunteur, DateEmprunt=@dateEmprunt " +
                    "WHERE Id=@id;";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", book.Id);
                BindParams(cmd, book);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur MySQL lors de la modification du livre : {ex.Message}", ex);
            }
        }

        /// <summary>Supprime un livre par son Id.</summary>
        public void DeleteBook(int id)
        {
            try
            {
                using var conn = OpenConnection();
                using var cmd  = new MySqlCommand("DELETE FROM Books WHERE Id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur MySQL lors de la suppression du livre : {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Recherche des livres dont le titre, l'auteur, le genre ou l'ISBN
        /// contient le terme passé en paramètre (insensible à la casse).
        /// </summary>
        public List<Book> SearchBooks(string terme)
        {
            var books = new List<Book>();
            try
            {
                using var conn = OpenConnection();
                const string sql =
                    "SELECT Id, Titre, Auteur, ISBN, AnneePubli, Genre, Rayon, Etagere, Disponible, Couverture, Emprunteur, DateEmprunt FROM Books " +
                    "WHERE Titre    LIKE @terme " +
                    "   OR Auteur   LIKE @terme " +
                    "   OR Genre    LIKE @terme " +
                    "   OR ISBN     LIKE @terme " +
                    "ORDER BY Titre;";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@terme", $"%{terme}%");
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    books.Add(MapBook(reader));
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Erreur MySQL lors de la recherche : {ex.Message}", ex);
            }
            return books;
        }

        // ----------------------------------------------------------------
        // Helper : lie les paramètres communs INSERT / UPDATE
        // ----------------------------------------------------------------
        private static void BindParams(MySqlCommand cmd, Book book)
        {
            cmd.Parameters.AddWithValue("@titre",   book.Titre);
            cmd.Parameters.AddWithValue("@auteur",  book.Auteur);
            cmd.Parameters.AddWithValue("@isbn",    book.ISBN);
            cmd.Parameters.AddWithValue("@annee",   book.AnneePubli);
            cmd.Parameters.AddWithValue("@genre",   book.Genre);
            cmd.Parameters.AddWithValue("@rayon",   book.Rayon);
            cmd.Parameters.AddWithValue("@etagere", book.Etagere);
            cmd.Parameters.AddWithValue("@dispo",   book.Disponible);
            cmd.Parameters.AddWithValue("@couverture", book.Couverture);
            cmd.Parameters.AddWithValue("@emprunteur", string.IsNullOrEmpty(book.Emprunteur) ? (object)DBNull.Value : book.Emprunteur);
            cmd.Parameters.AddWithValue("@dateEmprunt", book.DateEmprunt.HasValue ? (object)book.DateEmprunt.Value : DBNull.Value);
        }
    }
}

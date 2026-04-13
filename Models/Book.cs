namespace BibliothequeApp.Models
{
    /// <summary>
    /// Représente un livre dans l'inventaire de la bibliothèque.
    /// Correspond directement à la table Books de LibraryDB.
    /// </summary>
    public class Book
    {
        public int    Id         { get; set; }
        public string Titre      { get; set; } = string.Empty;
        public string Auteur     { get; set; } = string.Empty;
        public string ISBN       { get; set; } = string.Empty;
        public int    AnneePubli { get; set; }
        public string Genre      { get; set; } = string.Empty;
        public string Rayon      { get; set; } = string.Empty;
        public string Etagere    { get; set; } = string.Empty;
        public bool   Disponible { get; set; } = true;
    }
}

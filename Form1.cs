using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BibliothequeApp.Data;
using BibliothequeApp.Models;

namespace BibliothequeApp
{
    public partial class Form1 : Form
    {
        // ── Dépendances ────────────────────────────────────────────────
        private readonly BookRepository _repo = new BookRepository();

        // Id du livre sélectionné dans la grille (-1 = aucune sélection)
        private int _selectedBookId = -1;

        // ── Constructeur ───────────────────────────────────────────────
        public Form1()
        {
            InitializeComponent();
            ConfigurerGrille();
            AbonnerEvenements();
            ChargerLivres();
        }

        // ── Configuration initiale de la DataGridView ──────────────────
        private void ConfigurerGrille()
        {
            // Colonnes avec libellés français
            dgvLivres.Columns.Clear();
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId",     HeaderText = "ID",           DataPropertyName = "Id",         Visible = false });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTitre",  HeaderText = "Titre",        DataPropertyName = "Titre" });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAuteur", HeaderText = "Auteur",       DataPropertyName = "Auteur" });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colISBN",   HeaderText = "ISBN",         DataPropertyName = "ISBN" });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAnnee",  HeaderText = "Année",        DataPropertyName = "AnneePubli" });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colGenre",  HeaderText = "Genre",        DataPropertyName = "Genre" });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRayon",  HeaderText = "Rayon",        DataPropertyName = "Rayon" });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEtag",   HeaderText = "Étagère",      DataPropertyName = "Etagere" });
            dgvLivres.Columns.Add(new DataGridViewCheckBoxColumn { Name = "colDispo", HeaderText = "Disponible",   DataPropertyName = "Disponible" });
        }

        // ── Abonnement aux événements ──────────────────────────────────
        private void AbonnerEvenements()
        {
            btnAjouter.Click      += BtnAjouter_Click;
            btnModifier.Click     += BtnModifier_Click;
            btnSupprimer.Click    += BtnSupprimer_Click;
            btnVider.Click        += BtnVider_Click;
            btnRechercher.Click   += BtnRechercher_Click;
            btnAfficherTout.Click += BtnAfficherTout_Click;
            dgvLivres.SelectionChanged += DgvLivres_SelectionChanged;

            // Recherche en appuyant sur Entrée dans la zone de texte
            txtRecherche.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) BtnRechercher_Click(s, e);
            };
        }

        // ── Chargement / affichage ─────────────────────────────────────
        private void ChargerLivres(List<Book>? livres = null)
        {
            try
            {
                var liste = livres ?? _repo.GetAllBooks();
                dgvLivres.DataSource = null;
                dgvLivres.DataSource = liste;
                SetStatus($"{liste.Count} livre(s) affiché(s).");
            }
            catch (Exception ex)
            {
                AfficherErreur(ex.Message);
            }
        }

        // ── Sélection d'une ligne → remplissage du formulaire ──────────
        private void DgvLivres_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvLivres.CurrentRow == null) return;

            var row = dgvLivres.CurrentRow;
            _selectedBookId  = Convert.ToInt32(row.Cells["colId"].Value);
            txtTitre.Text    = row.Cells["colTitre"].Value?.ToString()  ?? "";
            txtAuteur.Text   = row.Cells["colAuteur"].Value?.ToString() ?? "";
            txtISBN.Text     = row.Cells["colISBN"].Value?.ToString()   ?? "";
            txtAnnee.Text    = row.Cells["colAnnee"].Value?.ToString()  ?? "";
            txtGenre.Text    = row.Cells["colGenre"].Value?.ToString()  ?? "";
            txtRayon.Text    = row.Cells["colRayon"].Value?.ToString()  ?? "";
            txtEtagere.Text  = row.Cells["colEtag"].Value?.ToString()   ?? "";
            chkDisponible.Checked = Convert.ToBoolean(row.Cells["colDispo"].Value);
        }

        // ── CRUD ───────────────────────────────────────────────────────
        private void BtnAjouter_Click(object? sender, EventArgs e)
        {
            if (!ValiderChamps()) return;

            try
            {
                var livre = LireFomulaire();
                _repo.AddBook(livre);
                ViderFormulaire();
                ChargerLivres();
                SetStatus("Livre ajouté avec succès.", success: true);
            }
            catch (Exception ex)
            {
                AfficherErreur(ex.Message);
            }
        }

        private void BtnModifier_Click(object? sender, EventArgs e)
        {
            if (_selectedBookId == -1)
            {
                MessageBox.Show("Veuillez d'abord sélectionner un livre dans la liste.",
                    "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValiderChamps()) return;

            try
            {
                var livre = LireFomulaire();
                livre.Id = _selectedBookId;
                _repo.UpdateBook(livre);
                ChargerLivres();
                SetStatus("Livre modifié avec succès.", success: true);
            }
            catch (Exception ex)
            {
                AfficherErreur(ex.Message);
            }
        }

        private void BtnSupprimer_Click(object? sender, EventArgs e)
        {
            if (_selectedBookId == -1)
            {
                MessageBox.Show("Veuillez d'abord sélectionner un livre dans la liste.",
                    "Aucune sélection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Confirmer la suppression du livre « {txtTitre.Text} » ?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                _repo.DeleteBook(_selectedBookId);
                ViderFormulaire();
                ChargerLivres();
                SetStatus("Livre supprimé.", success: true);
            }
            catch (Exception ex)
            {
                AfficherErreur(ex.Message);
            }
        }

        // ── Recherche ──────────────────────────────────────────────────
        private void BtnRechercher_Click(object? sender, EventArgs e)
        {
            string terme = txtRecherche.Text.Trim();
            if (string.IsNullOrEmpty(terme))
            {
                ChargerLivres();
                return;
            }

            try
            {
                var resultats = _repo.SearchBooks(terme);
                ChargerLivres(resultats);
                SetStatus($"{resultats.Count} résultat(s) pour « {terme} ».");
            }
            catch (Exception ex)
            {
                AfficherErreur(ex.Message);
            }
        }

        private void BtnAfficherTout_Click(object? sender, EventArgs e)
        {
            txtRecherche.Clear();
            ViderFormulaire();
            ChargerLivres();
        }

        // ── Vider les champs ───────────────────────────────────────────
        private void BtnVider_Click(object? sender, EventArgs e) => ViderFormulaire();

        private void ViderFormulaire()
        {
            _selectedBookId = -1;
            txtTitre.Clear();
            txtAuteur.Clear();
            txtISBN.Clear();
            txtAnnee.Clear();
            txtGenre.Clear();
            txtRayon.Clear();
            txtEtagere.Clear();
            chkDisponible.Checked = true;
            dgvLivres.ClearSelection();
        }

        // ── Helpers ────────────────────────────────────────────────────
        /// <summary>Construit un Book à partir des valeurs des TextBoxes.</summary>
        private Book LireFomulaire() => new Book
        {
            Titre      = txtTitre.Text.Trim(),
            Auteur     = txtAuteur.Text.Trim(),
            ISBN       = txtISBN.Text.Trim(),
            AnneePubli = int.TryParse(txtAnnee.Text.Trim(), out int a) ? a : 0,
            Genre      = txtGenre.Text.Trim(),
            Rayon      = txtRayon.Text.Trim(),
            Etagere    = txtEtagere.Text.Trim(),
            Disponible = chkDisponible.Checked,
        };

        /// <summary>Vérifie que les champs obligatoires sont remplis.</summary>
        private bool ValiderChamps()
        {
            if (string.IsNullOrWhiteSpace(txtTitre.Text))
            {
                MessageBox.Show("Le champ Titre est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTitre.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAuteur.Text))
            {
                MessageBox.Show("Le champ Auteur est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAuteur.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtISBN.Text))
            {
                MessageBox.Show("Le champ ISBN est obligatoire.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtISBN.Focus();
                return false;
            }
            if (!int.TryParse(txtAnnee.Text, out int annee) || annee < 1 || annee > DateTime.Now.Year)
            {
                MessageBox.Show($"L'année doit être un entier entre 1 et {DateTime.Now.Year}.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAnnee.Focus();
                return false;
            }
            return true;
        }

        private void SetStatus(string message, bool success = false)
        {
            lblStatus.Text      = message;
            lblStatus.ForeColor = success
                ? Color.FromArgb(46, 125, 50)
                : Color.FromArgb(90, 90, 90);
        }

        private void AfficherErreur(string message)
        {
            lblStatus.Text      = $"Erreur : {message}";
            lblStatus.ForeColor = Color.FromArgb(198, 40, 40);
            MessageBox.Show(message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

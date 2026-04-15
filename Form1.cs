using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
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
        private string _currentCoverPath = "";
        
        // Liste en mémoire pour le tri/filtre
        private List<Book> _livresActuels = new List<Book>();

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
            dgvLivres.AutoGenerateColumns = false;
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
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCouverture", DataPropertyName = "Couverture", Visible = false });
            dgvLivres.Columns.Add(new DataGridViewTextBoxColumn { Name = "colEmprunt", HeaderText = "Emprunteur",  DataPropertyName = "Emprunteur" });
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

            // Nouveaux événements pour les bonus
            btnCouverture.Click += BtnCouverture_Click;
            btnEmprunter.Click += BtnEmprunter_Click;
            btnRendre.Click += BtnRendre_Click;
            cmbFiltreDispo.SelectedIndexChanged += CmbFiltreDispo_SelectedIndexChanged;
            dgvLivres.ColumnHeaderMouseClick += DgvLivres_ColumnHeaderMouseClick;

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
                _livresActuels = livres ?? _repo.GetAllBooks();
                AppliquerFiltreEtTri();
            }
            catch (Exception ex)
            {
                AfficherErreur(ex.Message);
            }
        }

        private void AppliquerFiltreEtTri()
        {
            IEnumerable<Book> liste = _livresActuels;

            // Filtrage basique
            string filtre = cmbFiltreDispo.SelectedItem?.ToString() ?? "Tous";
            if (filtre == "Disponibles") liste = liste.Where(b => b.Disponible);
            else if (filtre == "Empruntés") liste = liste.Where(b => !b.Disponible);

            dgvLivres.DataSource = null;
            var finalListe = liste.ToList();
            dgvLivres.DataSource = finalListe;
            SetStatus($"{finalListe.Count} livre(s) affiché(s).");
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
            
            _currentCoverPath = row.Cells["colCouverture"].Value?.ToString() ?? "";
            AfficherImageCouverture(_currentCoverPath);
        }

        private void AfficherImageCouverture(string path)
        {
            try {
                if (pbCouverture.Image != null) pbCouverture.Image.Dispose();
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    pbCouverture.Image = Image.FromFile(path);
                else
                    pbCouverture.Image = null;
            } catch { pbCouverture.Image = null; }
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
                var original = _repo.GetAllBooks().FirstOrDefault(b => b.Id == _selectedBookId);
                var livre = LireFomulaire();
                livre.Id = _selectedBookId;
                if (original != null) {
                    livre.Emprunteur = original.Emprunteur;
                    livre.DateEmprunt = original.DateEmprunt;
                }
                
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
            _currentCoverPath = "";
            AfficherImageCouverture("");
            dgvLivres.ClearSelection();
        }

        // ── Bonus: Couvertures, Emprunt, Tri/Filtre ────────────────────

        private void BtnCouverture_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Fichiers image|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string destFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
                if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);
                string fileName = Path.GetFileName(ofd.FileName);
                string destPath = Path.Combine(destFolder, fileName);
                if (!File.Exists(destPath)) File.Copy(ofd.FileName, destPath);
                
                _currentCoverPath = destPath;
                AfficherImageCouverture(destPath);
            }
        }

        private void BtnEmprunter_Click(object? sender, EventArgs e)
        {
            if (_selectedBookId == -1) { MessageBox.Show("Sélectionnez un livre."); return; }
            var original = _repo.GetAllBooks().FirstOrDefault(b => b.Id == _selectedBookId);
            if (original == null) return;
            if (!original.Disponible) { MessageBox.Show("Ce livre est déjà emprunté."); return; }
            
            // Interaction simpliste : on demande le nom de l'emprunteur
            string emprunteur = "Lecteur"; 
            Form prompt = new Form() { Width = 300, Height = 150, Text = "Emprunt", StartPosition = FormStartPosition.CenterParent };
            TextBox txt = new TextBox() { Left = 20, Top = 20, Width = 200, Text = emprunteur };
            Button btnOk = new Button() { Text = "OK", Left = 20, Top = 60, DialogResult = DialogResult.OK };
            prompt.Controls.Add(txt); prompt.Controls.Add(btnOk);
            prompt.AcceptButton = btnOk;
            if (prompt.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text))
            {
                original.Disponible = false;
                original.Emprunteur = txt.Text.Trim();
                original.DateEmprunt = DateTime.Now;
                _repo.UpdateBook(original);
                ChargerLivres();
            }
        }

        private void BtnRendre_Click(object? sender, EventArgs e)
        {
            if (_selectedBookId == -1) { MessageBox.Show("Sélectionnez un livre."); return; }
            var original = _repo.GetAllBooks().FirstOrDefault(b => b.Id == _selectedBookId);
            if (original == null) return;
            if (original.Disponible) { MessageBox.Show("Livre déjà disponible."); return; }

            original.Disponible = true;
            original.Emprunteur = "";
            original.DateEmprunt = null;
            _repo.UpdateBook(original);
            ChargerLivres();
        }

        private void CmbFiltreDispo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            AppliquerFiltreEtTri();
        }

        private void DgvLivres_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dgvLivres.Columns[e.ColumnIndex];
            if (col.DataPropertyName == "Titre") _livresActuels = _livresActuels.OrderBy(b => b.Titre).ToList();
            else if (col.DataPropertyName == "Auteur") _livresActuels = _livresActuels.OrderBy(b => b.Auteur).ToList();
            else if (col.DataPropertyName == "AnneePubli") _livresActuels = _livresActuels.OrderBy(b => b.AnneePubli).ToList();
            AppliquerFiltreEtTri();
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
            Couverture = _currentCoverPath
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

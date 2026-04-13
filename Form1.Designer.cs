namespace BibliothequeApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            // ── Instanciation ──────────────────────────────────────────
            grpFormulaire   = new System.Windows.Forms.GroupBox();
            lblTitre        = new System.Windows.Forms.Label();
            txtTitre        = new System.Windows.Forms.TextBox();
            lblAuteur       = new System.Windows.Forms.Label();
            txtAuteur       = new System.Windows.Forms.TextBox();
            lblISBN         = new System.Windows.Forms.Label();
            txtISBN         = new System.Windows.Forms.TextBox();
            lblAnnee        = new System.Windows.Forms.Label();
            txtAnnee        = new System.Windows.Forms.TextBox();
            lblGenre        = new System.Windows.Forms.Label();
            txtGenre        = new System.Windows.Forms.TextBox();
            lblRayon        = new System.Windows.Forms.Label();
            txtRayon        = new System.Windows.Forms.TextBox();
            lblEtagere      = new System.Windows.Forms.Label();
            txtEtagere      = new System.Windows.Forms.TextBox();
            lblDisponible   = new System.Windows.Forms.Label();
            chkDisponible   = new System.Windows.Forms.CheckBox();
            grpActions      = new System.Windows.Forms.GroupBox();
            btnAjouter      = new System.Windows.Forms.Button();
            btnModifier     = new System.Windows.Forms.Button();
            btnSupprimer    = new System.Windows.Forms.Button();
            btnVider        = new System.Windows.Forms.Button();
            grpRecherche    = new System.Windows.Forms.GroupBox();
            txtRecherche    = new System.Windows.Forms.TextBox();
            btnRechercher   = new System.Windows.Forms.Button();
            btnAfficherTout = new System.Windows.Forms.Button();
            dgvLivres       = new System.Windows.Forms.DataGridView();
            lblStatus       = new System.Windows.Forms.Label();

            grpFormulaire.SuspendLayout();
            grpActions.SuspendLayout();
            grpRecherche.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLivres).BeginInit();
            SuspendLayout();

            // ── GroupBox Formulaire ────────────────────────────────────
            grpFormulaire.Text     = "Informations du livre";
            grpFormulaire.Location = new System.Drawing.Point(12, 12);
            grpFormulaire.Size     = new System.Drawing.Size(580, 230);

            // Titre
            lblTitre.Text     = "Titre *";
            lblTitre.Location = new System.Drawing.Point(10, 25);
            lblTitre.AutoSize = true;
            txtTitre.Location = new System.Drawing.Point(10, 43);
            txtTitre.Size     = new System.Drawing.Size(250, 23);

            // Auteur
            lblAuteur.Text     = "Auteur *";
            lblAuteur.Location = new System.Drawing.Point(280, 25);
            lblAuteur.AutoSize = true;
            txtAuteur.Location = new System.Drawing.Point(280, 43);
            txtAuteur.Size     = new System.Drawing.Size(285, 23);

            // ISBN
            lblISBN.Text     = "ISBN *";
            lblISBN.Location = new System.Drawing.Point(10, 78);
            lblISBN.AutoSize = true;
            txtISBN.Location = new System.Drawing.Point(10, 96);
            txtISBN.Size     = new System.Drawing.Size(160, 23);

            // Année
            lblAnnee.Text     = "Année *";
            lblAnnee.Location = new System.Drawing.Point(185, 78);
            lblAnnee.AutoSize = true;
            txtAnnee.Location = new System.Drawing.Point(185, 96);
            txtAnnee.Size     = new System.Drawing.Size(80, 23);

            // Genre
            lblGenre.Text     = "Genre";
            lblGenre.Location = new System.Drawing.Point(280, 78);
            lblGenre.AutoSize = true;
            txtGenre.Location = new System.Drawing.Point(280, 96);
            txtGenre.Size     = new System.Drawing.Size(140, 23);

            // Rayon
            lblRayon.Text     = "Rayon";
            lblRayon.Location = new System.Drawing.Point(10, 132);
            lblRayon.AutoSize = true;
            txtRayon.Location = new System.Drawing.Point(10, 150);
            txtRayon.Size     = new System.Drawing.Size(160, 23);

            // Étagère
            lblEtagere.Text     = "Étagère";
            lblEtagere.Location = new System.Drawing.Point(185, 132);
            lblEtagere.AutoSize = true;
            txtEtagere.Location = new System.Drawing.Point(185, 150);
            txtEtagere.Size     = new System.Drawing.Size(80, 23);

            // Disponible
            lblDisponible.Text     = "Disponible";
            lblDisponible.Location = new System.Drawing.Point(280, 132);
            lblDisponible.AutoSize = true;
            chkDisponible.Location = new System.Drawing.Point(280, 150);
            chkDisponible.Text     = "Oui";
            chkDisponible.Checked  = true;
            chkDisponible.AutoSize = true;

            grpFormulaire.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitre, txtTitre, lblAuteur, txtAuteur,
                lblISBN, txtISBN, lblAnnee, txtAnnee, lblGenre, txtGenre,
                lblRayon, txtRayon, lblEtagere, txtEtagere, lblDisponible, chkDisponible
            });

            // ── GroupBox Actions ───────────────────────────────────────
            grpActions.Text     = "Actions";
            grpActions.Location = new System.Drawing.Point(12, 252);
            grpActions.Size     = new System.Drawing.Size(580, 60);

            btnAjouter.Text      = "Ajouter";
            btnAjouter.Location  = new System.Drawing.Point(10, 24);
            btnAjouter.Size      = new System.Drawing.Size(110, 28);
            btnAjouter.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            btnAjouter.ForeColor = System.Drawing.Color.White;
            btnAjouter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnModifier.Text      = "Modifier";
            btnModifier.Location  = new System.Drawing.Point(134, 24);
            btnModifier.Size      = new System.Drawing.Size(110, 28);
            btnModifier.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            btnModifier.ForeColor = System.Drawing.Color.White;
            btnModifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnSupprimer.Text      = "Supprimer";
            btnSupprimer.Location  = new System.Drawing.Point(258, 24);
            btnSupprimer.Size      = new System.Drawing.Size(110, 28);
            btnSupprimer.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
            btnSupprimer.ForeColor = System.Drawing.Color.White;
            btnSupprimer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnVider.Text      = "Vider les champs";
            btnVider.Location  = new System.Drawing.Point(382, 24);
            btnVider.Size      = new System.Drawing.Size(150, 28);
            btnVider.BackColor = System.Drawing.Color.FromArgb(120, 120, 120);
            btnVider.ForeColor = System.Drawing.Color.White;
            btnVider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            grpActions.Controls.AddRange(new System.Windows.Forms.Control[] {
                btnAjouter, btnModifier, btnSupprimer, btnVider
            });

            // ── GroupBox Recherche ─────────────────────────────────────
            grpRecherche.Text     = "Recherche  (titre / auteur / genre / ISBN)";
            grpRecherche.Location = new System.Drawing.Point(12, 324);
            grpRecherche.Size     = new System.Drawing.Size(580, 58);

            txtRecherche.Location        = new System.Drawing.Point(10, 22);
            txtRecherche.Size            = new System.Drawing.Size(310, 23);
            txtRecherche.PlaceholderText = "Terme de recherche...";

            btnRechercher.Text      = "Rechercher";
            btnRechercher.Location  = new System.Drawing.Point(334, 21);
            btnRechercher.Size      = new System.Drawing.Size(105, 25);
            btnRechercher.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
            btnRechercher.ForeColor = System.Drawing.Color.White;
            btnRechercher.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            btnAfficherTout.Text      = "Tout afficher";
            btnAfficherTout.Location  = new System.Drawing.Point(452, 21);
            btnAfficherTout.Size      = new System.Drawing.Size(110, 25);
            btnAfficherTout.BackColor = System.Drawing.Color.FromArgb(96, 125, 139);
            btnAfficherTout.ForeColor = System.Drawing.Color.White;
            btnAfficherTout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            grpRecherche.Controls.AddRange(new System.Windows.Forms.Control[] {
                txtRecherche, btnRechercher, btnAfficherTout
            });

            // ── DataGridView ───────────────────────────────────────────
            dgvLivres.Location            = new System.Drawing.Point(12, 394);
            dgvLivres.Size                = new System.Drawing.Size(1160, 330);
            dgvLivres.ReadOnly            = true;
            dgvLivres.AllowUserToAddRows  = false;
            dgvLivres.SelectionMode       = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvLivres.MultiSelect         = false;
            dgvLivres.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvLivres.BackgroundColor     = System.Drawing.Color.White;
            dgvLivres.BorderStyle         = System.Windows.Forms.BorderStyle.Fixed3D;
            dgvLivres.Font                = new System.Drawing.Font("Segoe UI", 9f);
            dgvLivres.RowHeadersWidth     = 30;
            dgvLivres.AlternatingRowsDefaultCellStyle.BackColor     = System.Drawing.Color.FromArgb(245, 245, 245);
            dgvLivres.ColumnHeadersDefaultCellStyle.BackColor       = System.Drawing.Color.FromArgb(45, 45, 45);
            dgvLivres.ColumnHeadersDefaultCellStyle.ForeColor       = System.Drawing.Color.White;
            dgvLivres.ColumnHeadersDefaultCellStyle.Font            = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dgvLivres.EnableHeadersVisualStyles                     = false;
            dgvLivres.ColumnHeadersHeight                           = 30;

            // ── Label Status ───────────────────────────────────────────
            lblStatus.Location  = new System.Drawing.Point(12, 732);
            lblStatus.Size      = new System.Drawing.Size(1160, 22);
            lblStatus.Text      = "Prêt.";
            lblStatus.Font      = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Italic);
            lblStatus.ForeColor = System.Drawing.Color.FromArgb(90, 90, 90);

            // ── Form ───────────────────────────────────────────────────
            AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize          = new System.Drawing.Size(1184, 762);
            MinimumSize         = new System.Drawing.Size(1200, 800);
            StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text                = "BibliothequeApp — Gestion de la bibliothèque";
            BackColor           = System.Drawing.Color.FromArgb(250, 250, 250);

            Controls.AddRange(new System.Windows.Forms.Control[] {
                grpFormulaire, grpActions, grpRecherche, dgvLivres, lblStatus
            });

            grpFormulaire.ResumeLayout(false);
            grpFormulaire.PerformLayout();
            grpActions.ResumeLayout(false);
            grpRecherche.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLivres).EndInit();
            ResumeLayout(false);
        }

        #endregion

        // Contrôles du formulaire
        private System.Windows.Forms.GroupBox    grpFormulaire;
        private System.Windows.Forms.Label       lblTitre;
        private System.Windows.Forms.TextBox     txtTitre;
        private System.Windows.Forms.Label       lblAuteur;
        private System.Windows.Forms.TextBox     txtAuteur;
        private System.Windows.Forms.Label       lblISBN;
        private System.Windows.Forms.TextBox     txtISBN;
        private System.Windows.Forms.Label       lblAnnee;
        private System.Windows.Forms.TextBox     txtAnnee;
        private System.Windows.Forms.Label       lblGenre;
        private System.Windows.Forms.TextBox     txtGenre;
        private System.Windows.Forms.Label       lblRayon;
        private System.Windows.Forms.TextBox     txtRayon;
        private System.Windows.Forms.Label       lblEtagere;
        private System.Windows.Forms.TextBox     txtEtagere;
        private System.Windows.Forms.Label       lblDisponible;
        private System.Windows.Forms.CheckBox    chkDisponible;
        // Contrôles Actions
        private System.Windows.Forms.GroupBox    grpActions;
        private System.Windows.Forms.Button      btnAjouter;
        private System.Windows.Forms.Button      btnModifier;
        private System.Windows.Forms.Button      btnSupprimer;
        private System.Windows.Forms.Button      btnVider;
        // Contrôles Recherche
        private System.Windows.Forms.GroupBox    grpRecherche;
        private System.Windows.Forms.TextBox     txtRecherche;
        private System.Windows.Forms.Button      btnRechercher;
        private System.Windows.Forms.Button      btnAfficherTout;
        // Grille + status
        private System.Windows.Forms.DataGridView dgvLivres;
        private System.Windows.Forms.Label        lblStatus;
    }
}

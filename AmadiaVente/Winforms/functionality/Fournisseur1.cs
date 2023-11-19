using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.functionality
{
    public partial class Fournisseur1 : Form
    {
        //Déclaration Globale
        private Form activeForm;
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        string sessionId;
        private bool indicationErreurAchat = false;
        int prixAchat = 0;

        //Constructeur
        public Fournisseur1()
        {

            InitializeComponent();
            dataGridViewPanier.Columns.Add("NomProduit", "Nom Du Produit"); // Colonne pour le nom du produit
            dataGridViewPanier.Columns.Add("Quantite", "Quantité"); // Colonne pour la quantité du produit
            dataGridViewPanier.Columns.Add("prixDAchat", "Prix d'Achat");
            dataGridViewPanier.Columns.Add("prixDeVente", "Prix de Vente");// Colonne pour le prix unitaire du produit
            dataGridViewPanier.Columns.Add("prixDeVenteMembre", "PV Membre");// Colonne pour le prix unitaire du produit
            dataGridViewPanier.Columns.Add("Prix", "Prix");


        }

        private void afficheMedicComboBoxLoad()
        {
            Medicament.Items.Clear();
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT designation FROM article WHERE type_article = 'Médicaments'";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Medicament.Items.Add(reader["designation"].ToString());
                        }
                    }
                }
            }
        }

        private int[] AfficheArcticle(string article)
        {
            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string query = "SELECT prix_article,nbr_stock FROM article WHERE designation=@article";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@article", article);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();

                            int prixArticle = reader.GetInt32(0);
                            int NombreStock = reader.GetInt32(1);

                            return new int[] { prixArticle, NombreStock };
                        }
                    }
                }
            }
            return null;
        }

        private void Medicament_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeMedicament.Enabled = true;
            Medicament.Enabled = true;
            PUMedicament.Enabled = true;
            PrixMedicament.Enabled = true;
            QuantiteMedicament.Enabled = true;
            txtBoxPrixdeVente.Enabled = true;

            if (Medicament.SelectedItem != null)
            {
                string articleSelectionner = Medicament.SelectedItem.ToString();
                QuantiteMedicament.Text = string.Empty;
                int prixActuelle = AfficheArcticle(articleSelectionner)[0];
                txtBoxPrixdeVente.PlaceholderText = prixActuelle.ToString();
            }
            else
            {
                PUMedicament.Text = PrixMedicament.Text = null;
            }
        }

        private void TypeMedicament_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeMedicament.Enabled = true;
            Medicament.Enabled = true;
            PUMedicament.Enabled = false;
            PrixMedicament.Enabled = false;
            QuantiteMedicament.Enabled = false;

            if (TypeMedicament.SelectedItem != null && TypeMedicament.SelectedItem.ToString() == "Médicaments")
            {
                afficheMedicamentComboBox("Médicaments");
            }
            else if (TypeMedicament.SelectedItem != null && TypeMedicament.SelectedItem.ToString() == "Equipements")
            {
                afficheMedicamentComboBox("Equipements");
            }
        }

        private void afficheMedicamentComboBox(string type)
        {
            Medicament.Items.Clear();
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT designation FROM article WHERE type_article = @type";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@type", type);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Medicament.Items.Add(reader["designation"].ToString());
                        }
                    }
                }
            }
        }

        private void afficheNomFournisseurLoad()
        {
            Medicament.Items.Clear();
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT nomFournisseur FROM fournisseur";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            NomFournisseur.Items.Add(reader["nomFournisseur"].ToString());
                        }
                    }
                }
            }
        }

        private void disableFunction()
        {
            NomFournisseur.Enabled = false;
            PrixMedicament.Enabled = false;
        }

        private void cacherModifPanier()
        {
            btnSupprimerPanier.Visible = btnModifierPanier.Visible = false;
        }

        private void afficherModifPanier()
        {
            btnSupprimerPanier.Visible = btnModifierPanier.Visible = true;
        }

        private void DisableAllFunction()
        {
            disableFunction();
            txtBoxPrixdeVente.Enabled = QuantiteMedicament.Enabled = Medicament.Enabled = TypeMedicament.Enabled = txtBoxPrixdeVente.Enabled = false;
        }
        private void reinitialiseAllFunction()
        {
            DisableAllFunction();
            TypeMedicament.Text = "Médicaments";
            PrixMedicament.Text = PUMedicament.Text = QuantiteMedicament.Text = string.Empty;
            NomFournisseur.SelectedItem = TypeMedicament.SelectedItem = Medicament.SelectedItem = null;
            cacherModifPanier();
            btnAjoutPanier.Enabled = true;
            btnValiderAchat.Enabled = false;
            dataGridViewPanier.Rows.Clear();
            Medicament.Enabled = false;
            TypeMedicament.Enabled = false;
            txtBoxPrixdeVente.Enabled = false;
            NomFournisseur.Enabled = true;
            txtBoxPrixDeVenteMembre.Clear();
        }

        private void AjoutFournisseurPage_Click(object sender, EventArgs e)
        {
            popUp.popUpNewFournisseur popUpAddFournisseur = new popUp.popUpNewFournisseur();
            popUpAddFournisseur.ShowDialog();
            popUpAddFournisseur.Dispose();
            NomFournisseur.Items.Clear();
            afficheNomFournisseurLoad();
        }


        private int GetArticleIdByDesignation(string designation)
        {
            int articleId = -1;

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string query = "SELECT id_article FROM article WHERE designation = @designation";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@designation", designation);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            articleId = reader.GetInt32(0);
                        }
                    }
                }
            }

            return articleId;
        }
        public void validerAchat(int idArticle, int quantite, int prixVente, int prixVenteMembre, int prixAchat, int prix, int idCommande)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string checkStockQuery = "SELECT nbr_stock, designation FROM article WHERE id_article = @idArticle";

                using (SqliteCommand checkCommand = new SqliteCommand(checkStockQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@idArticle", idArticle);

                    using (SqliteDataReader reader = checkCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int quantiteEnStock = reader.GetInt32(0);
                            string designationArticle = reader.GetString(1);

                            if (quantite >= 0)
                            {
                                // Le stock est suffisant, procédez à l'achat...

                                string insertLigneCommandeQuery = "INSERT INTO ligneCommandeF (id_article, quantiteMedicament, prixVente,prixVenteMembre, prixAchat, prixMedicament, idCommandeF) VALUES (@idArticle, @quantite, @prixVente, @prixVenteMembre, @prixAchat, @prix, @idCommande)";

                                using (SqliteCommand insertCommand = new SqliteCommand(insertLigneCommandeQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@idArticle", idArticle);
                                    insertCommand.Parameters.AddWithValue("@quantite", quantite);
                                    insertCommand.Parameters.AddWithValue("@prixVente", prixVente);
                                    insertCommand.Parameters.AddWithValue("@prixVenteMembre", prixVenteMembre);
                                    insertCommand.Parameters.AddWithValue("@prixAchat", prixAchat);
                                    insertCommand.Parameters.AddWithValue("@prix", prix);
                                    insertCommand.Parameters.AddWithValue("@idCommande", idCommande);


                                    insertCommand.ExecuteNonQuery();
                                }

                                string updateStockQuery = "UPDATE article SET nbr_stock = nbr_stock + @quantite WHERE id_article = @idArticle";

                                using (SqliteCommand updateCommand = new SqliteCommand(updateStockQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@quantite", quantite);
                                    updateCommand.Parameters.AddWithValue("@idArticle", idArticle);

                                    updateCommand.ExecuteNonQuery();
                                }

                                string updatePrixQuery = "UPDATE article SET prix_article = @prixVente, prix_membre = @prixVenteMembre  WHERE id_article = @idArticle";

                                using (SqliteCommand updateCommand = new SqliteCommand(updatePrixQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@prixVente", prixVente);
                                    updateCommand.Parameters.AddWithValue("@prixVenteMembre", prixVenteMembre);
                                    updateCommand.Parameters.AddWithValue("@idArticle", idArticle);

                                    updateCommand.ExecuteNonQuery();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Impossible d'Ajouter cette nouveau Stock " + designationArticle + ".\nVeuillez vérifier les nombre de nouveau Article!\n\nLes autres achats du client vont quand même être enregistrer !", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                indicationErreurAchat = true;
                            }
                        }
                    }
                }
            }
        }

        public void makeHistory(int idArticle, int quantite, int idfournisseur)
        {
            string mouvement = "Entrer";

            DateTime dateHeureActuelles = DateTime.Now;
            string date = dateHeureActuelles.ToString("yyyy-MM-dd HH:mm:ss");

            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO stockHistoryEntrer (id_article, mouvement, quantite, id_source, date) VALUES (@idArticle, @mouvement, @quantite, @idFournisseur, @date)";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@idArticle", idArticle);
                    command.Parameters.AddWithValue("@mouvement", mouvement);
                    command.Parameters.AddWithValue("@quantite", quantite);
                    command.Parameters.AddWithValue("@idFournisseur", idfournisseur);
                    command.Parameters.AddWithValue("@date", date);

                    command.ExecuteNonQuery();
                }
            }
        }

        //Evenements

        private void btnAjoutPanier_Click(object sender, EventArgs e)
        {
            if (Medicament.SelectedItem != null && PrixMedicament.Text != string.Empty && txtBoxPrixdeVente.Text != string.Empty && QuantiteMedicament.Text != string.Empty)
            {
                btnValiderAchat.Enabled = true;
                NomFournisseur.Enabled = TypeMedicament.Enabled = false;

                string nomProduit = Medicament.SelectedItem.ToString();
                int quantite = int.Parse(QuantiteMedicament.Text);
                int prixTotal = int.Parse(PrixMedicament.Text);
                int Pu = int.Parse(PUMedicament.Text);
                int prixDeVente = int.Parse(txtBoxPrixdeVente.Text);
                int prixDeVenteMembre = 0;

                if (!string.IsNullOrEmpty(txtBoxPrixDeVenteMembre.Text))
                {
                    prixDeVenteMembre = int.Parse(txtBoxPrixDeVenteMembre.Text);
                }

                dataGridViewPanier.Rows.Add(nomProduit, quantite, Pu, prixDeVente, prixDeVenteMembre, prixTotal);

                Medicament.SelectedIndex = -1;
                Medicament.Text = "";
                QuantiteMedicament.Clear();
                PUMedicament.Clear();
                PrixMedicament.Clear();
                txtBoxPrixdeVente.Clear();
                txtBoxPrixDeVenteMembre.Clear();
            }
            else
            {
                MessageBox.Show("Veuillez remplir les informations nécessaires !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSupprimerPanier_Click(object sender, EventArgs e)
        {
            if (dataGridViewPanier.SelectedRows.Count > 0)
            {
                // Créez une liste pour stocker les indices des lignes à supprimer.
                List<int> rowsToDelete = new List<int>();

                // Parcourez toutes les lignes sélectionnées.
                foreach (DataGridViewRow selectedRow in dataGridViewPanier.SelectedRows)
                {
                    // Obtenez l'indice de la ligne sélectionnée.
                    int rowIndex = selectedRow.Index;

                    // Assurez-vous que l'indice est valide.
                    if (rowIndex >= 0 && rowIndex < dataGridViewPanier.Rows.Count)
                    {
                        // Ajoutez l'indice de la ligne à la liste des lignes à supprimer.
                        rowsToDelete.Add(rowIndex);

                    }
                }

                // Triez les indices en ordre décroissant
                rowsToDelete.Sort((a, b) => b.CompareTo(a));

                // Supprimez les lignes un par un, en partant de la fin
                foreach (int rowIndex in rowsToDelete)
                {
                    int montantLigne = Convert.ToInt32(dataGridViewPanier.Rows[rowIndex].Cells["Prix"].Value);
                    dataGridViewPanier.Rows.RemoveAt(rowIndex);
                }
                Medicament.SelectedIndex = -1;
                Medicament.Text = "";
                QuantiteMedicament.Clear();
                PUMedicament.Clear();
                PrixMedicament.Clear();
                txtBoxPrixdeVente.Clear();
            }
            btnAjoutPanier.Enabled = true;
            cacherModifPanier();
        }

        private void btnModifierPanier_Click(object sender, EventArgs e)
        {
            if (dataGridViewPanier.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridViewPanier.SelectedRows[0];

                int montantLigne = Convert.ToInt32(selectedRow.Cells["Prix"].Value);

                selectedRow.Cells["NomProduit"].Value = Medicament.SelectedItem;
                selectedRow.Cells["Quantite"].Value = QuantiteMedicament.Text;
                selectedRow.Cells["Prix"].Value = PrixMedicament.Text;
                selectedRow.Cells["prixDeVente"].Value = txtBoxPrixdeVente.Text;
                selectedRow.Cells["prixDeVenteMembre"].Value = txtBoxPrixDeVenteMembre.Text;
                selectedRow.Cells["prixDAchat"].Value = PUMedicament.Text;

                int nouveauPrix = Convert.ToInt32(selectedRow.Cells["Prix"].Value);

                Medicament.SelectedItem = null;
                PrixMedicament.Text = QuantiteMedicament.Text = txtBoxPrixdeVente.Text = string.Empty;
                txtBoxPrixDeVenteMembre.Clear();

            }
            btnAjoutPanier.Enabled = true;
            cacherModifPanier();
        }

        private int RecupererIdCommande()
        {
            int commandeId = -1;

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                string query = "SELECT last_insert_rowid();";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    commandeId = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return commandeId;
        }

        private void creeCommande(int FournisseurId, int idResponsable)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                string query = "INSERT INTO commandeFournisseur (idFournisseur, id_responsable, date_commandeF) VALUES(@FournisseurId, @idResponsable, @date)";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FournisseurId", FournisseurId);
                    command.Parameters.AddWithValue("@idResponsable", idResponsable);

                    DateTime dateHeureActuelles = DateTime.Now;
                    string dateHeureFormattee = dateHeureActuelles.ToString("yyyy-MM-dd HH:mm:ss");

                    command.Parameters.AddWithValue("@date", dateHeureFormattee);

                    command.ExecuteNonQuery();
                }
            }
        }


        private void btnValiderAchat_Click(object sender, EventArgs e)
        {

            DialogResult confirm = MessageBox.Show("Confirmer la transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                String nomFournisseur = NomFournisseur.SelectedItem.ToString();
                int idFournisseur = Convert.ToInt32(GetFournisseurId(nomFournisseur));
                int IdResponsable = Convert.ToInt32(sessionId);

                creeCommande(idFournisseur, IdResponsable);

                int idCommande = RecupererIdCommande();

                foreach (DataGridViewRow row in dataGridViewPanier.Rows)
                {

                    string nomProduit = row.Cells["NomProduit"].Value.ToString();
                    int quantite = Convert.ToInt32(row.Cells["Quantite"].Value);
                    int prix = Convert.ToInt32(row.Cells["Prix"].Value);
                    int prixAchat = Convert.ToInt32(row.Cells["prixDAchat"].Value.ToString());
                    int prixVente = Convert.ToInt32(row.Cells["prixDeVente"].Value.ToString());
                    int prixVenteMembre = Convert.ToInt32(row.Cells["prixDeVenteMembre"].Value.ToString());

                    int artcileId = GetArticleIdByDesignation(designation: nomProduit);

                    validerAchat(artcileId, quantite, prixVente, prixVenteMembre, prixAchat, prix, idCommande);
                    makeHistory(artcileId, quantite, idCommande);
                }
                if (indicationErreurAchat)
                {
                    reinitialiseAllFunction();
                }
                else
                {
                    reinitialiseAllFunction();
                    MessageBox.Show("Ajout nouveau stock Effectué !", "Réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                indicationErreurAchat = false;
            }
        }


        private void creeCommande(int session)
        {
            throw new NotImplementedException();
        }


        private void btnAnnulerAchat_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Etes-vous sûr de vouloir annuler ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                reinitialiseAllFunction();
            }
        }

        public string GetFournisseurId(string nomFournisseurId)
        {
            string IdFournisseurF = "";
            String NomF = nomFournisseurId;

            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string query = "SELECT idFournisseur FROM fournisseur WHERE nomFournisseur = @NomF";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NomF", NomF);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        IdFournisseurF = result.ToString();
                    }
                }

            }

            return IdFournisseurF;
        }

        private void NomFournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            TypeMedicament.Enabled = true;
            Medicament.Enabled = false;
            PUMedicament.Enabled = false;
            PrixMedicament.Enabled = false;
            QuantiteMedicament.Enabled = false;
            txtBoxPrixdeVente.Enabled = false;

            if (NomFournisseur.SelectedItem != null)
            {
                string nomEtPrenom = NomFournisseur.SelectedItem.ToString();
                string idFournisseur = GetFournisseurId(nomEtPrenom);

            }
            else
            {
                NomFournisseur.Text = null;
            }
        }

        private void Fournisseur1_Load(object sender, EventArgs e)
        {
            TypeMedicament.Items.Add("Médicaments");
            TypeMedicament.Items.Add("Equipements");
            PUMedicament.Enabled = false;
            btnValiderAchat.Enabled = false;
            txtBoxPrixdeVente.Enabled = false;

            TypeMedicament.Enabled = false;
            Medicament.Enabled = false;
            PUMedicament.Enabled = false;
            PrixMedicament.Enabled = false;
            QuantiteMedicament.Enabled = false;



            afficheMedicComboBoxLoad();
            afficheNomFournisseurLoad();

            sessionId = Classes.Storage.SessionId;

            cacherModifPanier();

        }

        private void QuantiteMedicament_TextChanged(object sender, EventArgs e)
        {
            if (QuantiteMedicament.Text != "")
            {
                string qteArticle = QuantiteMedicament.Text.ToString();
                string articleSelect = Medicament.SelectedItem.ToString();
                int stock = AfficheArcticle(articleSelect)[1];
                if (!string.IsNullOrEmpty(PUMedicament.Text.ToString()))
                {
                    prixAchat = Convert.ToInt32(PUMedicament.Text.ToString());
                }

                int prixAPayer = prixAchat * Convert.ToInt32(qteArticle);

                PrixMedicament.Text = prixAPayer.ToString();

                if (stock < Convert.ToInt32(qteArticle))
                {
                    QuantiteMedicament.BackColor = Color.Red;
                }
                else
                {
                    QuantiteMedicament.BackColor = Color.LightGreen;
                }
            }
            else
            {
                PrixMedicament.Text = "";
            }
        }

        private void dataGridViewPanier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewPanier.Rows[e.RowIndex];

                Medicament.SelectedItem = selectedRow.Cells["NomProduit"].Value.ToString();
                QuantiteMedicament.Text = selectedRow.Cells["Quantite"].Value.ToString();
                PrixMedicament.Text = selectedRow.Cells["Prix"].Value.ToString();
                txtBoxPrixdeVente.Text = selectedRow.Cells["prixDeVente"].Value.ToString();
                txtBoxPrixDeVenteMembre.Text = selectedRow.Cells["prixDeVenteMembre"].Value.ToString();
                PUMedicament.Text = selectedRow.Cells["prixDAchat"].Value.ToString();
            }
            btnAjoutPanier.Enabled = false;
            afficherModifPanier();
        }

        private void QuantiteMedicament_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PUMedicament_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBoxPrixdeVente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PrixMedicament_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void PUMedicament_TextChanged(object sender, EventArgs e)
        {
            int qteArticle = 0;

            // Tentative de conversion de la chaîne en entier
            if (int.TryParse(PUMedicament.Text, out prixAchat))
            {
                // Conversion réussie, procédez avec le reste
                if (!string.IsNullOrEmpty(QuantiteMedicament.Text))
                {
                    // Tentative de conversion de la quantité en entier
                    if (int.TryParse(QuantiteMedicament.Text, out qteArticle))
                    {
                        // Conversion réussie, calculez le prix à payer
                        int prixAPayer = prixAchat * qteArticle;
                        PrixMedicament.Text = prixAPayer.ToString();
                    }
                    else
                    {
                        // Gérer le cas où la conversion de la quantité échoue
                        // (par exemple, QuantiteMedicament.Text n'est pas un entier)
                        PrixMedicament.Text = "Erreur de quantité";
                    }
                }
                else
                {
                    // Gérer le cas où la quantité est vide
                    PrixMedicament.Text = "Quantité non spécifiée";
                }
            }
            else
            {
                // Gérer le cas où la conversion du prix d'achat échoue
                // (par exemple, PUMedicament.Text n'est pas un entier)
                PrixMedicament.Text = "Erreur de prix d'achat";
            }
        }
    }
}
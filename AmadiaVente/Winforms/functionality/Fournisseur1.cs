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
        private Form activeForm;
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");
        string sessionId;
        private int totalPayer;
        private bool indicationErreurAchat = false;


        public Fournisseur1()
        {

            InitializeComponent();
            dataGridViewPanier.Columns.Add("NomProduit", "Nom Du Produit"); // Colonne pour le nom du produit
            dataGridViewPanier.Columns.Add("Quantite", "Quantité"); // Colonne pour la quantité du produit
            dataGridViewPanier.Columns.Add("Prix", "Prix"); // Colonne pour le prix unitaire du produit

            labelTotal.Text = totalPayer.ToString() + " Ar";


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
            if (Medicament.SelectedItem != null)
            {
                string articleSelectionner = Medicament.SelectedItem.ToString();
                int prixArticle = AfficheArcticle(articleSelectionner)[0];
                int resteStock = AfficheArcticle(articleSelectionner)[1];
                PUMedicament.Text = prixArticle.ToString();
                labelStock.Text = resteStock.ToString();
                QuantiteMedicament.Text = string.Empty;
            }
            else
            {
                PUMedicament.Text = PrixMedicament.Text = labelStock.Text = null;
            }
        }

        private void TypeMedicament_SelectedIndexChanged(object sender, EventArgs e)
        {
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

        private void afficheNomMembreLoad()
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
            QuantiteMedicament.Enabled = Medicament.Enabled = TypeMedicament.Enabled = false;
        }
        private void reinitialiseAllFunction()
        {
            DisableAllFunction();
            TypeMedicament.Text = "Médicaments";
            PrixMedicament.Text = PUMedicament.Text = QuantiteMedicament.Text = labelStock.Text = string.Empty;
            cacherModifPanier();
            totalPayer = 0;
            labelTotal.Text = totalPayer.ToString() + " Ar";
            btnAjoutPanier.Enabled = true;
            btnValiderAchat.Enabled = false;
            dataGridViewPanier.Rows.Clear();
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            //ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.Controls.Add(childForm);
            this.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void AjoutFournisseurPage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new functionality.Fournisseur(), sender);
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
        public void validerAchat(int idArticle, int quantite, decimal prix, int idCommande)
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

                                string insertLigneCommandeQuery = "INSERT INTO ligneCommandeF (id_article, quantiteMedicament, prixMedicament, idCommandeF) VALUES (@idArticle, @quantite, @prix, @idCommande)";

                                using (SqliteCommand insertCommand = new SqliteCommand(insertLigneCommandeQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@idArticle", idArticle);
                                    insertCommand.Parameters.AddWithValue("@quantite", quantite);
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


        private void btnAjoutPanier_Click(object sender, EventArgs e)
        {
            if (Medicament.SelectedItem != null && PrixMedicament.Text != string.Empty && QuantiteMedicament.Text != string.Empty)
            {
                btnValiderAchat.Enabled = true;
                NomFournisseur.Enabled = TypeMedicament.Enabled = false;

                string nomProduit = Medicament.SelectedItem.ToString();
                int quantite = int.Parse(QuantiteMedicament.Text);
                int prixTotal = int.Parse(PrixMedicament.Text);
                int Pu = int.Parse(PUMedicament.Text);

                dataGridViewPanier.Rows.Add(nomProduit, quantite, prixTotal, Pu);

                Medicament.SelectedIndex = -1;
                Medicament.Text = "";
                QuantiteMedicament.Clear();
                PUMedicament.Clear();
                PrixMedicament.Clear();
                totalPayer += prixTotal;
                labelTotal.Text = totalPayer.ToString() + " Ar";
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
                    totalPayer -= montantLigne;
                }
                Medicament.SelectedIndex = -1;
                Medicament.Text = "";
                QuantiteMedicament.Clear();
                PUMedicament.Clear();
                PrixMedicament.Clear();
                labelTotal.Text = totalPayer.ToString() + " Ar";
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
                totalPayer -= montantLigne;

                selectedRow.Cells["NomProduit"].Value = Medicament.Items;
                selectedRow.Cells["Quantite"].Value = QuantiteMedicament.Text;
                selectedRow.Cells["Prix"].Value = PrixMedicament.Text;

                int nouveauPrix = Convert.ToInt32(selectedRow.Cells["Prix"].Value);
                totalPayer += nouveauPrix;

                Medicament.SelectedItem = null;
                QuantiteMedicament.Text = PrixMedicament.Text = null;

            }
            btnAjoutPanier.Enabled = true;
            cacherModifPanier();
            labelTotal.Text = totalPayer.ToString() + " Ar";
        }

        private void dataGridViewPanier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewPanier.Rows[e.RowIndex];

                Medicament.SelectedItem = selectedRow.Cells["NomProduit"].Value.ToString();
                QuantiteMedicament.Text = selectedRow.Cells["Quantite"].Value.ToString();
                PrixMedicament.Text = selectedRow.Cells["Prix"].Value.ToString();
            }
            btnAjoutPanier.Enabled = false;
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
                int idFournisseur = 2;
                int IdResponsable = Convert.ToInt32(sessionId);

                creeCommande(idFournisseur, IdResponsable);

                int idCommande = RecupererIdCommande();

                foreach (DataGridViewRow row in dataGridViewPanier.Rows)
                {

                    string nomProduit = row.Cells["NomProduit"].Value.ToString();
                    int quantite = Convert.ToInt32(row.Cells["Quantite"].Value);
                    decimal prix = Convert.ToDecimal(row.Cells["Prix"].Value);

                    int artcileId = GetArticleIdByDesignation(designation: nomProduit);

                    validerAchat(artcileId, quantite, prix, idCommande);
                }
                if (indicationErreurAchat)
                {
                    reinitialiseAllFunction();
                }
                else
                {
                    reinitialiseAllFunction();
                    MessageBox.Show("Achat Effectué !", "Réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string query = "SELECT idFournisseur FROM fournisseur WHERE nomFournisseur = @NomF";

                // Divisez la chaîne par le premier espace
                string[] parts = nomFournisseurId.Split(new char[] { ' ' }, 2);

                if (parts.Length == 2)
                {
                    string NomF = parts[0];
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
            }

            return IdFournisseurF;
        }
        private void NomFournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            TypeMedicament.SelectedItem = "Médicaments";
            /*PUMedicament.Enabled = false;
            btnValiderAchat.Enabled = false;*/

            afficheMedicComboBoxLoad();
            afficheNomMembreLoad();

            //DisableAllFunction();
            sessionId = Classes.Storage.SessionId;

            cacherModifPanier();

        }

        private void QuantiteMedicament_TextChanged(object sender, EventArgs e)
        {
            if (QuantiteMedicament.Text != "")
            {
                string qteArticle = QuantiteMedicament.Text.ToString();
                string articleSelect = Medicament.SelectedItem.ToString();
                int prixUnit = AfficheArcticle(articleSelect)[0];
                int stock = AfficheArcticle(articleSelect)[1];

                int prixAPayer = prixUnit * Convert.ToInt32(qteArticle);

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
    }
}
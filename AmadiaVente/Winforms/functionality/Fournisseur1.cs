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
        string cs = "Data Source=mydatabase.db;Version=3;Password=myPassword;";
        string sessionId;
        private int totalPayer;
        private bool indicationErreurAchat = false;

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
        public Fournisseur1()
        {

            InitializeComponent();
            dataGridViewPanier.Columns.Add("NomProduit", "Nom Du Produit"); // Colonne pour le nom du produit
            dataGridViewPanier.Columns.Add("Quantite", "Quantité"); // Colonne pour la quantité du produit
            dataGridViewPanier.Columns.Add("Prix", "Prix"); // Colonne pour le prix unitaire du produit

            labelTotal.Text = totalPayer.ToString() + " Ar";


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

                            if (quantiteEnStock >= quantite)
                            {
                                // Le stock est suffisant, procédez à l'achat...

                                string insertLigneCommandeQuery = "INSERT INTO lignecommandeFournisseur (id_article, qte_acheter, prix, id_commande) VALUES(@idArticle, @quantite, @prix, @idCommande)";

                                using (SqliteCommand insertCommand = new SqliteCommand(insertLigneCommandeQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@idArticle", idArticle);
                                    insertCommand.Parameters.AddWithValue("@quantite", quantite);
                                    insertCommand.Parameters.AddWithValue("@prix", prix);
                                    insertCommand.Parameters.AddWithValue("@idCommande", idCommande);

                                    insertCommand.ExecuteNonQuery();
                                }

                                string updateStockQuery = "UPDATE article SET nbr_stock = nbr_stock - @quantite WHERE id_article = @idArticle";

                                using (SqliteCommand updateCommand = new SqliteCommand(updateStockQuery, connection))
                                {
                                    updateCommand.Parameters.AddWithValue("@quantite", quantite);
                                    updateCommand.Parameters.AddWithValue("@idArticle", idArticle);

                                    updateCommand.ExecuteNonQuery();
                                }

                            }
                            else
                            {
                                MessageBox.Show("Stock Insuffisant pour " + designationArticle + ".\nVeuillez vérifier le stock avant d'effectuer à nouveau la vente de cet article!\n\nLes autres achats du client vont quand même être enregistrer !", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                indicationErreurAchat = true;
                            }
                        }
                    }
                }
            }
        }


        private void btnAjoutPanier_Click(object sender, EventArgs e)
        {
            if (PrixMedicament.Text != string.Empty && QuantiteMedicament.Text != string.Empty)
            {
                btnValiderAchat.Enabled = true;
                NomFournisseur.Enabled = TypeMedicament.Enabled = false;

                string nomProduit = Medicament.Text.ToString();
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
        }

        private void btnModifierPanier_Click(object sender, EventArgs e)
        {
            if (dataGridViewPanier.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridViewPanier.SelectedRows[0];

                int montantLigne = Convert.ToInt32(selectedRow.Cells["Prix"].Value);
                totalPayer -= montantLigne;

                selectedRow.Cells["NomProduit"].Value = Medicament.Text;
                selectedRow.Cells["Quantite"].Value = QuantiteMedicament.Text;
                selectedRow.Cells["Prix"].Value = PrixMedicament.Text;

                int nouveauPrix = Convert.ToInt32(selectedRow.Cells["Prix"].Value);
                totalPayer += nouveauPrix;

                Medicament.Text = null;
                QuantiteMedicament.Text = PrixMedicament.Text = null;

            }
            labelTotal.Text = totalPayer.ToString() + " Ar";
        }

        private void dataGridViewPanier_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridViewPanier.Rows[e.RowIndex];

                Medicament.Text = selectedRow.Cells["NomProduit"].Value.ToString();
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

        private void creeCommande(int membre, string idMembre, int idResponsable)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                string query = "INSERT INTO commande (membre, id_membre, id_responsable, date_achat) VALUES(@membre, @idMembre, @idResponsable, @date)";

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@membre", membre);

                    if (membre != 0)
                    {
                        command.Parameters.AddWithValue("@idMembre", idMembre);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@idMembre", DBNull.Value);
                    }
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
                int session = Convert.ToInt32(sessionId);
                string y_n_membre = NomFournisseur.Text.ToString();
                int membre = 0;
                string idMembre = "";

                creeCommande(membre, idMembre, session);
                int idCommande = RecupererIdCommande();

                foreach (DataGridViewRow row in dataGridViewPanier.Rows)
                {

                    string nomProduit = row.Cells["NomProduit"].Value.ToString();
                    int quantite = Convert.ToInt32(row.Cells["Quantite"].Value);
                    decimal prix = Convert.ToDecimal(row.Cells["Prix"].Value);

                    int artcileId = GetArticleIdByDesignation(nomProduit);

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

       
    }
}
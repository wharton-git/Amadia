﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace AmadiaVente.Winforms.functionality
{
    public partial class achat : Form
    {
        //Déclaration Globale
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        string sessionId;
        private bool isSortedAscending = false;
        private bool indicationErreurAchat = false;
        int totalPayer = 0;

        //Méthodes (fonctions)
        public achat()
        {
            InitializeComponent();
            dataGridViewPanier.Columns.Add("NomProduit", "Nom Du Produit"); // Colonne pour le nom du produit
            dataGridViewPanier.Columns.Add("Quantite", "Quantité"); // Colonne pour la quantité du produit
            dataGridViewPanier.Columns.Add("PrixUnitaire", "P.U."); // Colonne pour le prix unitaire du produit
            dataGridViewPanier.Columns.Add("Prix", "Prix"); // Colonne pour le prix unitaire du produit

            labelTotal.Text = totalPayer.ToString() + " Ar";

        }

        private void disableFunction()
        {
            txtBoxNumeroMembre.Enabled = false;
            comboBoxNomMembre.Enabled = false;
            txtBoxPrix.Enabled = false;
        }

        private void enableFunction()
        {
            txtBoxNumeroMembre.Enabled = true;
            comboBoxNomMembre.Enabled = true;
            txtBoxPrix.Enabled = true;
        }

        private void afficheMedicComboBoxLoad()
        {
            comboBoxDesignation.Items.Clear();
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
                            comboBoxDesignation.Items.Add(reader["designation"].ToString());
                        }
                    }
                }
            }
        }

        private void afficheMedicamentComboBox(string type)
        {
            comboBoxDesignation.Items.Clear();
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
                            comboBoxDesignation.Items.Add(reader["designation"].ToString());
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

                string query = "SELECT prix_article, prix_membre, nbr_stock, prix_utilisable FROM article WHERE designation=@article";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@article", article);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {

                        if (reader.HasRows)
                        {
                            reader.Read();

                            int prixArticle = reader.GetInt32(0);
                            int prixArticleMembre = reader.GetInt32(1);
                            int NombreStock = reader.GetInt32(2);
                            int prix_utilisable = reader.GetInt32(3);

                            return new int[] { prixArticle, prixArticleMembre, NombreStock, prix_utilisable };
                        }
                    }
                }
            }
            return null;
        }

        private void DisableAllFunction()
        {
            disableFunction();
            txtBoxQuantite.Enabled = comboBoxDesignation.Enabled = comboBoxTypeArticle.Enabled = false;
        }

        private void securiteMembreFunction()
        {
            txtBoxQuantite.Enabled = comboBoxDesignation.Enabled = comboBoxTypeArticle.Enabled = true;
            comboBoxMembre.Enabled = false;
        }

        private void reinitialiseAllFunction()
        {
            DisableAllFunction();
            comboBoxMembre.Enabled = true;
            comboBoxMembre.Text = comboBoxDesignation.Text = comboBoxNomMembre.Text = txtBoxNumeroMembre.Text = string.Empty;
            comboBoxTypeArticle.SelectedItem = "Médicaments";
            txtBoxPrix.Text = txtBoxPU.Text = txtBoxQuantite.Text = labelStock.Text = string.Empty;
            cacherModifPanier();
            totalPayer = 0;
            labelTotal.Text = totalPayer.ToString() + " Ar";
            btnAjoutPanier.Enabled = true;
            btnValiderAchat.Enabled = false;
            dataGridViewPanier.Rows.Clear();
            checkBoxUtil.Visible = true;
            checkBoxUtil.Checked = false;
        }

        private void afficheNomMembreLoad()
        {
            comboBoxNomMembre.Items.Clear();
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT nom_membre, prenom_membre FROM membre";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxNomMembre.Items.Add(reader["nom_membre"].ToString() + " " + reader["prenom_membre"].ToString());
                        }
                    }
                }
            }
        }

        private string[] SelectMembreById(int numero)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT id_membre, nom_membre, prenom_membre FROM membre WHERE id_membre=@numero";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@numero", numero);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            int id_membre = reader.GetInt32(0);
                            string nom_membre = reader.GetString(1);
                            string prenom_membre = reader.GetString(2);

                            return new string[] { id_membre.ToString(), nom_membre, prenom_membre };
                        }
                    }
                }
            }
            return null;
        }

        private void cacherModifPanier()
        {
            btnSupprimerPanier.Visible = btnModifierPanier.Visible = false;
        }

        private void afficherModifPanier()
        {
            btnSupprimerPanier.Visible = btnModifierPanier.Visible = true;
            btnValiderAchat.Enabled = false;
        }

        public string GetMemberId(string nomPrenomMembre)
        {
            string memberId = "";

            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string query = "SELECT id_membre FROM membre WHERE nom_membre = @nomMembre AND prenom_membre = @prenomMembre";

                // Divisez la chaîne par le premier espace
                string[] parts = nomPrenomMembre.Split(new char[] { ' ' }, 2);

                if (parts.Length == 2)
                {
                    string nomMembre = parts[0];
                    string prenomMembre = parts[1];

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nomMembre", nomMembre);
                        command.Parameters.AddWithValue("@prenomMembre", prenomMembre);

                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            memberId = result.ToString();
                        }
                    }
                }
            }

            return memberId;
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

        private void creeCommandeUtil(int membre, string idMembre, int idResponsable)
        {

            bool util = true;

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();
                string query = "INSERT INTO commande (membre, id_membre, id_responsable, date_achat, utilisation) VALUES(@membre, @idMembre, @idResponsable, @date, @util)";

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
                    command.Parameters.AddWithValue("@util", util);

                    DateTime dateHeureActuelles = DateTime.Now;
                    string dateHeureFormattee = dateHeureActuelles.ToString("yyyy-MM-dd HH:mm:ss");

                    command.Parameters.AddWithValue("@date", dateHeureFormattee);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void validerAchat(int idArticle, int quantite, decimal pu, decimal prix, int idCommande)
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

                                string insertLigneCommandeQuery = "INSERT INTO lignecommande (id_article, qte_acheter, pu, prix, id_commande) VALUES(@idArticle, @quantite, @pu, @prix, @idCommande)";

                                using (SqliteCommand insertCommand = new SqliteCommand(insertLigneCommandeQuery, connection))
                                {
                                    insertCommand.Parameters.AddWithValue("@idArticle", idArticle);
                                    insertCommand.Parameters.AddWithValue("@quantite", quantite);
                                    insertCommand.Parameters.AddWithValue("@pu", pu);
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

        public void makeHistory(int idArticle, int quantite, int idCommande)
        {
            string mouvement = "Sortie";

            DateTime dateHeureActuelles = DateTime.Now;
            string date = dateHeureActuelles.ToString("yyyy-MM-dd HH:mm:ss");

            using (SQLiteConnection connection = new SQLiteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "INSERT INTO stockHistorySortie (id_article, mouvement, quantite, id_source, date) VALUES (@idArticle, @mouvement, @quantite, @idCommande, @date)";

                using (SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@idArticle", idArticle);
                    command.Parameters.AddWithValue("@mouvement", mouvement);
                    command.Parameters.AddWithValue("@quantite", quantite);
                    command.Parameters.AddWithValue("@idCommande", idCommande);
                    command.Parameters.AddWithValue("@date", date);

                    command.ExecuteNonQuery();
                }
            }
        }


        //Evénements
        private void achat_Load(object sender, EventArgs e)
        {
            comboBoxMembre.Items.Add("Oui");
            comboBoxMembre.Items.Add("Non");

            comboBoxTypeArticle.Items.Add("Médicaments");
            comboBoxTypeArticle.Items.Add("Consommables");
            comboBoxTypeArticle.SelectedItem = "Médicaments";

            txtBoxPU.Enabled = false;

            afficheMedicComboBoxLoad();
            afficheNomMembreLoad();

            DisableAllFunction();
            sessionId = Classes.Storage.SessionId;

            cacherModifPanier();
            btnValiderAchat.Enabled = false;

        }

        private void comboBoxMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Oui")
            {
                enableFunction();
                securiteMembreFunction();
                checkBoxUtil.Visible = false;

            }
            else if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Non")
            {
                disableFunction();
                securiteMembreFunction();
                checkBoxUtil.Visible = false;
            }
        }

        private void comboBoxTypeArticle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypeArticle.SelectedItem != null && comboBoxTypeArticle.SelectedItem.ToString() == "Médicaments")
            {
                afficheMedicamentComboBox("Médicaments");
            }
            else if (comboBoxTypeArticle.SelectedItem != null && comboBoxTypeArticle.SelectedItem.ToString() == "Consommables")
            {
                afficheMedicamentComboBox("Equipements");
            }
        }

        private void comboBoxDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDesignation.SelectedItem != null)
            {
                string articleSelectionner = comboBoxDesignation.SelectedItem.ToString();
                int prixArticle = AfficheArcticle(articleSelectionner)[0];

                if (checkBoxUtil.Checked == true)
                {
                    prixArticle = AfficheArcticle(articleSelectionner)[3];

                    if (prixArticle == 0)
                    {
                        prixArticle = AfficheArcticle(articleSelectionner)[0];
                    }

                }

                if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Oui")
                {
                    int prixArticleTrans = AfficheArcticle(articleSelectionner)[1];

                    if (prixArticleTrans != 0)
                    {
                        prixArticle = AfficheArcticle(articleSelectionner)[1];
                    }
                }

                int resteStock = AfficheArcticle(articleSelectionner)[2];
                txtBoxPU.Text = prixArticle.ToString();
                labelStock.Text = resteStock.ToString();
                txtBoxQuantite.Text = string.Empty;
            }
            else
            {
                txtBoxPU.Text = txtBoxPrix.Text = labelStock.Text = null;
            }
        }

        private void txtBoxQuantite_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxDesignation.SelectedItem != null)
            {
                if (txtBoxQuantite.Text != "")
                {
                    string qteArticle = txtBoxQuantite.Text.ToString();
                    string articleSelect = comboBoxDesignation.SelectedItem.ToString();
                    int prixUnit = AfficheArcticle(articleSelect)[0];

                    if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Oui")
                    {
                        //prixUnit = AfficheArcticle(articleSelect)[1];
                        int prixArticleTrans = AfficheArcticle(articleSelect)[1];

                        if (prixArticleTrans != 0)
                        {
                            prixUnit = AfficheArcticle(articleSelect)[1];
                        }
                    }

                    int stock = AfficheArcticle(articleSelect)[2];

                    int prixAPayer = prixUnit * Convert.ToInt32(qteArticle);

                    txtBoxPrix.Text = prixAPayer.ToString();

                    if (stock < Convert.ToInt32(qteArticle))
                    {
                        txtBoxQuantite.BackColor = Color.Red;
                    }
                    else
                    {
                        txtBoxQuantite.BackColor = Color.LightGreen;
                    }
                }
                else
                {
                    txtBoxPrix.Text = "";
                }
            }
        }

        private void btnAnnulerAchat_Click(object sender, EventArgs e)
        {
            DialogResult confirm = MessageBox.Show("Etes-vous sûr de vouloir annuler ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                reinitialiseAllFunction();
            }
        }

        private void btnValiderAchat_Click(object sender, EventArgs e)
        {

            DialogResult confirm = MessageBox.Show("Confirmer la transaction ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                int session = Convert.ToInt32(sessionId);
                string y_n_membre = comboBoxMembre.Text.ToString();
                int membre = 0;
                string idMembre = "";
                string nomMembre = comboBoxNomMembre.Text;
                if (y_n_membre == "Oui")
                {
                    membre = 1;
                    idMembre = GetMemberId(nomMembre);
                }
                else
                {
                    membre = membre;
                    idMembre = idMembre;
                }

                if (checkBoxUtil.Checked == true)
                {
                    creeCommandeUtil(membre, idMembre, session);
                }
                else
                {
                    creeCommande(membre, idMembre, session);
                }
                int idCommande = RecupererIdCommande();

                foreach (DataGridViewRow row in dataGridViewPanier.Rows)
                {

                    string nomProduit = row.Cells["NomProduit"].Value.ToString();
                    int quantite = Convert.ToInt32(row.Cells["Quantite"].Value);
                    decimal pu = Convert.ToDecimal(row.Cells["PrixUnitaire"].Value);
                    decimal prix = Convert.ToDecimal(row.Cells["Prix"].Value);

                    int artcileId = GetArticleIdByDesignation(nomProduit);

                    validerAchat(artcileId, quantite, pu, prix, idCommande);
                    makeHistory(artcileId, quantite, idCommande);

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

        private void txtBoxNumeroMembre_TextChanged(object sender, EventArgs e)
        {
            if (txtBoxNumeroMembre.Text != null && txtBoxNumeroMembre.Text != "")
            {
                int numero = Convert.ToInt32(txtBoxNumeroMembre.Text.ToString());
                if (SelectMembreById(numero) != null)
                {
                    string[] infoMembre = SelectMembreById(numero);
                    string nomEtPrenom = infoMembre[1] + " " + infoMembre[2];

                    comboBoxNomMembre.SelectedItem = nomEtPrenom;
                    labelAlertNumeroMembre.Text = string.Empty;
                }
                else
                {
                    labelAlertNumeroMembre.Text = "Membre n'existe pas !";
                    comboBoxNomMembre.SelectedItem = null;
                }


            }
            else
            {
                comboBoxNomMembre.SelectedItem = null;
            }
        }

        private void txtBoxPrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if (e.KeyChar != '.' || txtBoxPrix.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtBoxQuantite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBoxNumeroMembre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnAjoutPanier_Click(object sender, EventArgs e)
        {
            btnValiderAchat.Enabled = true;
            if (comboBoxDesignation.SelectedItem != null && txtBoxPrix.Text != string.Empty && txtBoxQuantite.Text != string.Empty)
            {
                int quantitePiece = Convert.ToInt32(txtBoxQuantite.Text);
                int stockPiece = Convert.ToInt32(labelStock.Text);
                if (stockPiece >= quantitePiece)
                {
                    txtBoxNumeroMembre.Enabled = comboBoxNomMembre.Enabled = false;

                    string nomProduit = comboBoxDesignation.SelectedItem.ToString();
                    int quantite = int.Parse(txtBoxQuantite.Text);
                    int pu = int.Parse(txtBoxPU.Text);
                    int prixTotal = int.Parse(txtBoxPrix.Text);


                    dataGridViewPanier.Rows.Add(nomProduit, quantite, pu, prixTotal);

                    comboBoxDesignation.SelectedIndex = -1;
                    txtBoxQuantite.Clear();
                    txtBoxPU.Clear();
                    labelStock.Text = "";
                    txtBoxPrix.Clear();
                    totalPayer += prixTotal;
                    labelTotal.Text = totalPayer.ToString() + " Ar";
                }
                else
                {
                    MessageBox.Show("Le stock pour cette article est insuffisant.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir les informations nécessaires !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewPanier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            afficherModifPanier();
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dataGridViewPanier.Rows[e.RowIndex];

                comboBoxDesignation.SelectedItem = selectedRow.Cells["NomProduit"].Value.ToString();
                txtBoxQuantite.Text = selectedRow.Cells["Quantite"].Value.ToString();
                txtBoxPU.Text = selectedRow.Cells["PrixUnitaire"].Value.ToString();
                txtBoxPrix.Text = selectedRow.Cells["Prix"].Value.ToString();

            }
            btnAjoutPanier.Enabled = false;
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
                labelTotal.Text = totalPayer.ToString() + " Ar";
            }

            btnAjoutPanier.Enabled = btnValiderAchat.Enabled = true;
            cacherModifPanier();

        }

        private void btnModifierPanier_Click(object sender, EventArgs e)
        {

            if (dataGridViewPanier.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridViewPanier.SelectedRows[0];

                int montantLigne = Convert.ToInt32(selectedRow.Cells["Prix"].Value);
                totalPayer -= montantLigne;

                selectedRow.Cells["NomProduit"].Value = comboBoxDesignation.SelectedItem;
                selectedRow.Cells["Quantite"].Value = txtBoxQuantite.Text;
                selectedRow.Cells["PrixUnitaire"].Value = txtBoxPU.Text;
                selectedRow.Cells["Prix"].Value = txtBoxPrix.Text;

                int nouveauPrix = Convert.ToInt32(selectedRow.Cells["Prix"].Value);
                totalPayer += nouveauPrix;

                comboBoxDesignation.SelectedItem = null;
                txtBoxQuantite.Text = txtBoxPrix.Text = null;

            }
            btnAjoutPanier.Enabled = btnValiderAchat.Enabled = true;
            cacherModifPanier();
            labelTotal.Text = totalPayer.ToString() + " Ar";
        }

        private void dataGridViewPanier_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewPanier.Columns["Quantite"].Index)
            {
                if (isSortedAscending)
                {
                    dataGridViewPanier.Sort(dataGridViewPanier.Columns["Quantite"], ListSortDirection.Descending);
                }
                else
                {
                    dataGridViewPanier.Sort(dataGridViewPanier.Columns["Quantite"], ListSortDirection.Ascending);
                }
                isSortedAscending = !isSortedAscending;
            }
        }

        private void comboBoxNomMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxNomMembre.SelectedItem != null)
            {
                string nomEtPrenom = comboBoxNomMembre.SelectedItem.ToString();
                string idMembre = GetMemberId(nomEtPrenom);
                txtBoxNumeroMembre.Text = idMembre;
            }
            else
            {
                txtBoxNumeroMembre.Text = null;
            }
        }

        private void checkBoxUtil_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUtil.Checked == true)
            {
                txtBoxNumeroMembre.Enabled = comboBoxMembre.Enabled = comboBoxNomMembre.Enabled = false;
                comboBoxTypeArticle.Enabled = comboBoxDesignation.Enabled = txtBoxPrix.Enabled = txtBoxQuantite.Enabled = true;

            }
            else
            {
                reinitialiseAllFunction();
            }
        }
    }
}

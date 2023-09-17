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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace AmadiaVente.Winforms.functionality
{
    public partial class achat : Form
    {
        //Déclaration Globale
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");
        string sessionId;
        private bool isSortedAscending = false;
        private string whart;

        //Méthodes (fonctions)
        public achat()
        {
            InitializeComponent();
            dataGridViewPanier.Columns.Add("NomProduit", "Nom Du Produit"); // Colonne pour le nom du produit
            dataGridViewPanier.Columns.Add("Quantite", "Quantité"); // Colonne pour la quantité du produit
            dataGridViewPanier.Columns.Add("Prix", "Prix"); // Colonne pour le prix unitaire du produit

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
            btnAjoutPanier.Enabled = true;
            dataGridViewPanier.Rows.Clear();
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
        }

        private void creeCommande(int membre, string idMembre, int idResponsable, string date)
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
                    command.Parameters.AddWithValue("@date", date);

                    command.ExecuteNonQuery();
                }
            }
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

        //Evénements
        private void achat_Load(object sender, EventArgs e)
        {
            comboBoxMembre.Items.Add("Oui");
            comboBoxMembre.Items.Add("Non");

            comboBoxTypeArticle.Items.Add("Médicaments");
            comboBoxTypeArticle.Items.Add("Equipements");
            comboBoxTypeArticle.SelectedItem = "Médicaments";

            txtBoxPU.Enabled = false;

            afficheMedicComboBoxLoad();
            afficheNomMembreLoad();

            DisableAllFunction();
            sessionId = Classes.Storage.SessionId;

            cacherModifPanier();

        }

        private void comboBoxMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Oui")
            {
                enableFunction();
                securiteMembreFunction();

            }
            else if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Non")
            {
                disableFunction();
                securiteMembreFunction();
            }
        }

        private void comboBoxTypeArticle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypeArticle.SelectedItem != null && comboBoxTypeArticle.SelectedItem.ToString() == "Médicaments")
            {
                afficheMedicamentComboBox("Médicaments");
            }
            else if (comboBoxTypeArticle.SelectedItem != null && comboBoxTypeArticle.SelectedItem.ToString() == "Equipements")
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
                int resteStock = AfficheArcticle(articleSelectionner)[1];
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
            if (txtBoxQuantite.Text != "")
            {
                string qteArticle = txtBoxQuantite.Text.ToString();
                string articleSelect = comboBoxDesignation.SelectedItem.ToString();
                int prixUnit = AfficheArcticle(articleSelect)[0];
                int stock = AfficheArcticle(articleSelect)[1];

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

        private void btnAnnulerAchat_Click(object sender, EventArgs e)
        {
            reinitialiseAllFunction();
        }

        private void btnValiderAchat_Click(object sender, EventArgs e)
        {
            int session = Convert.ToInt32(sessionId);
            string y_n_membre = comboBoxMembre.Text.ToString();
            int membre = 0;
            DateTime aujourdhui = DateTime.Now;
            string temps = aujourdhui.ToString("yyy-MM-dd");
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
            creeCommande(membre, idMembre, session, temps);
            reinitialiseAllFunction();
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
            txtBoxNumeroMembre.Enabled = comboBoxNomMembre.Enabled = false;
            if (comboBoxDesignation.SelectedItem != null && txtBoxPrix.Text != string.Empty && txtBoxQuantite.Text != string.Empty)
            {
                string nomProduit = comboBoxDesignation.SelectedItem.ToString();
                int quantite = int.Parse(txtBoxQuantite.Text);
                int prixTotal = int.Parse(txtBoxPrix.Text);

                dataGridViewPanier.Rows.Add(nomProduit, quantite, prixTotal);

                comboBoxDesignation.SelectedIndex = -1;
                txtBoxQuantite.Clear();
                txtBoxPU.Clear();
                labelStock.Text = "";
                txtBoxPrix.Clear();
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
                    dataGridViewPanier.Rows.RemoveAt(rowIndex);
                }
            }

            btnAjoutPanier.Enabled = true;
            cacherModifPanier();
        }

        private void btnModifierPanier_Click(object sender, EventArgs e)
        {

            if (dataGridViewPanier.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = dataGridViewPanier.SelectedRows[0];

                selectedRow.Cells["NomProduit"].Value = comboBoxDesignation.SelectedItem;
                selectedRow.Cells["Quantite"].Value = txtBoxQuantite.Text;
                selectedRow.Cells["Prix"].Value = txtBoxPrix.Text;

                comboBoxDesignation.SelectedItem = null;
                txtBoxQuantite.Text = txtBoxPrix.Text = null;

            }
            btnAjoutPanier.Enabled = true;
            cacherModifPanier();
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
    }
}

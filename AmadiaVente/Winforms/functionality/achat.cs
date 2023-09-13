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
            comboBoxNumeroMembre.Enabled = false;
            comboBoxNomMembre.Enabled = false;
            txtBoxPrix.Enabled = false;
        }

        private void enableFunction()
        {
            comboBoxNumeroMembre.Enabled = true;
            comboBoxNomMembre.Enabled = true;
            txtBoxPrix.Enabled = true;
        }

        private void afficheMedicComboBoxLoad()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT designation FROM article";

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
            comboBoxMembre.Text = comboBoxDesignation.Text = comboBoxNomMembre.Text = comboBoxNumeroMembre.Text = comboBoxTypeArticle.Text = string.Empty;
            txtBoxPrix.Text = txtBoxPU.Text = txtBoxQuantite.Text = labelStock.Text = string.Empty;
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

        private void afficheNumeroMembreLoad()
        {
            comboBoxNumeroMembre.Items.Clear();
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT id_membre FROM membre";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBoxNumeroMembre.Items.Add(reader["id_membre"].ToString());
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
            afficheNumeroMembreLoad();

            DisableAllFunction();


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
                txtBoxPrix.Text = prixArticle.ToString();
                txtBoxQuantite.Text = "";
            }
            else
            {
                txtBoxPU.Text = txtBoxPrix.Text = labelStock.Text = "";
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
            reinitialiseAllFunction();
        }

        private void comboBoxNumeroMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numero = Convert.ToInt32(comboBoxNumeroMembre.SelectedItem.ToString());
            string[] selectionNom = SelectMembreById(numero);

            if (selectionNom != null)
            {
                string nomEtPrenom = selectionNom[1] + " " + selectionNom[2];
                comboBoxNomMembre.SelectedItem = nomEtPrenom;

                comboBoxNumeroMembre.SelectedItem = numero.ToString();
            }
        }
    }
}

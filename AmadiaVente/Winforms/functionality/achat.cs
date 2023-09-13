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

        //Evénements
        private void achat_Load(object sender, EventArgs e)
        {
            comboBoxMembre.Items.Add("Oui");
            comboBoxMembre.Items.Add("Non");

            comboBoxTypeArticle.Items.Add("Médicaments");
            comboBoxTypeArticle.Items.Add("Equipements");
            comboBoxTypeArticle.SelectedItem = "Médicaments";

            //comboBoxNomMembre.DropDownStyle = ComboBoxStyle.DropDown;
            /*comboBoxNomMembre.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxNomMembre.AutoCompleteSource = AutoCompleteSource.ListItems;*/

            txtBoxPU.Text = "300";
            txtBoxPU.Enabled = false;

            afficheMedicComboBoxLoad();

        }

        private void comboBoxMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Oui")
            {
                enableFunction();
            }
            else if (comboBoxMembre.SelectedItem != null && comboBoxMembre.SelectedItem.ToString() == "Non")
            {
                disableFunction();
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
    }
}

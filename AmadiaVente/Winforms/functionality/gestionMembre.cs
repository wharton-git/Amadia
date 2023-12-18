using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.functionality
{
    public partial class gestionMembre : Form
    {
        //Declaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        string idMembre = null;

        //Constructeur
        public gestionMembre()
        {
            InitializeComponent();
        }
        //Méthodes
        private void afficheMembre()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_membre AS Numéro, nom_membre AS Nom, prenom_membre AS Prénom, adresse AS Adresse, contact AS Contact, contact2 AS 'Contact 2', date_naiss AS 'Date de Naissance', date_adhesion AS 'Date Adh' FROM membre";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewListMember.DataSource = dataTable;
                    }
                }
            }
        }

        private void searchMembre(string value)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_membre AS Numéro, nom_membre AS Nom, prenom_membre AS Prénom, adresse AS Adresse, contact AS Contact, contact2 AS 'Contact 2', date_naiss AS 'Date de Naissance', date_adhesion AS 'Date Adh' FROM membre WHERE id_membre LIKE '%" + value + "%' OR nom_membre LIKE '%" + value + "%' OR prenom_membre LIKE '%" + value + "%' OR adresse LIKE '%" + value + "%' OR contact LIKE '%" + value + "%' OR contact2 LIKE '%" + value + "%' OR date_naiss LIKE '%" + value + "%' OR date_adhesion LIKE '%" + value + "%'";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewListMember.DataSource = dataTable;
                    }
                }
            }
        }

        private void removeMember(string id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();
                    string query = "DELETE from membre WHERE id_membre = @id";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur lors de la suppression : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Evenements
        private void gestionMembre_Load(object sender, EventArgs e)
        {
            afficheMembre();

            labelNom.Text = labelPrenom.Text = String.Empty;
        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            popUp.popUpAddMember popUp = new popUp.popUpAddMember();
            popUp.ShowDialog();
            popUp.Dispose();
            afficheMembre();
            labelNom.Text = labelPrenom.Text = txtBoxSearchMembre.Text = String.Empty;
        }

        private void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (dataGridViewListMember.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Confirmez-vous la suppression de ce membre ?\nCette action est irréversible !", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {

                    DataGridViewRow selectedRow = dataGridViewListMember.SelectedRows[0];
                    String valeurCellule = selectedRow.Cells[0].Value.ToString();
                    removeMember(valeurCellule);
                    afficheMembre();
                    idMembre = null;
                }

                labelNom.Text = labelPrenom.Text = txtBoxSearchMembre.Text = String.Empty;

            }
            else
            {
                MessageBox.Show("Veuillez Selectionner un membre à Supprimer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void btnEditMember_Click(object sender, EventArgs e)
        {
            if (idMembre != null)
            {
                popUp.popUpModifierMembre popUp = new popUp.popUpModifierMembre(idMembre);
                popUp.ShowDialog();
                popUp.Dispose();
                afficheMembre();
                idMembre = null;
                labelNom.Text = labelPrenom.Text = txtBoxSearchMembre.Text = String.Empty;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un membre avant de procéder à l'édition !", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dataGridViewListMember_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dataGridViewListMember.Rows[e.RowIndex];

                string nom = selectedRow.Cells["Nom"].Value.ToString();
                string prenom = selectedRow.Cells["Prénom"].Value.ToString();
                string contact = selectedRow.Cells["Contact"].Value.ToString();
                string adresse = selectedRow.Cells["Adresse"].Value.ToString();

                if (string.IsNullOrEmpty(contact))
                {
                    contact = "Pas de contact";
                }
                if (string.IsNullOrEmpty(adresse))
                {
                    adresse = "Pas d'adresse défini";
                }

                idMembre = selectedRow.Cells["Numéro"].Value.ToString();

                labelNom.Text = prenom + " " + nom;
                labelPrenom.Text = contact + ", " + adresse;


            }
        }

        private void txtBoxSearchMembre_TextChanged(object sender, EventArgs e)
        {
            string value = txtBoxSearchMembre.Text.ToString();

            searchMembre(value);
        }
    }
}

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
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpNewFournisseur : Form
    {
        //Declaration Globale
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");
        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        //Constructeur
        public popUpNewFournisseur()
        {
            InitializeComponent();
        }

        //Methodes
        private bool IsValidEmail(string email)
        {
            try
            {
                // Utilisez une expression régulière pour valider le format de l'adresse e-mail
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Regex regex = new Regex(pattern);

                return regex.IsMatch(email);
            }
            catch (Exception)
            {
                // En cas d'erreur lors de la création de l'expression régulière
                return false;
            }
        }

        private void afficheListeFournisseur()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT idFournisseur AS ID, nomFournisseur AS Nom, contact AS Contact, adresse AS Adresse, email AS Email FROM fournisseur ";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewListFournisseur.DataSource = dataTable;
                    }
                }
            }
        }

        private void searchFunction(string searchTerme)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT idFournisseur AS ID, nomFournisseur AS Nom, contact AS Contact, adresse AS Adresse, email AS Email FROM fournisseur WHERE idFournisseur LIKE '%"+ searchTerme + "%' OR nomFournisseur LIKE '%"+ searchTerme + "%' OR contact LIKE '%"+ searchTerme + "%' OR adresse LIKE '%"+ searchTerme + "%' OR email LIKE '%"+ searchTerme +"%'";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewListFournisseur.DataSource = dataTable;
                    }
                }
            }
        }
        private void removeFournisseur(string id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();
                    string query = "DELETE from fournisseur WHERE idFournisseur = @id";

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

        //Evenement
        private void btnQuitList_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjoutFornisseur_Click(object sender, EventArgs e)
        {
            if (NomFournisseurs.Text != string.Empty && ContactFourisseur.Text != string.Empty && AdresseFournisseur.Text != string.Empty)
            {
                if (string.IsNullOrEmpty(EmailFournisseur.Text) || IsValidEmail(EmailFournisseur.Text))
                {
                    using (SQLiteConnection connection = new SQLiteConnection(cs))
                    {

                        string nomF = NomFournisseurs.Text;
                        string contact = ContactFourisseur.Text;
                        string emailF = EmailFournisseur.Text;
                        string adresseF = AdresseFournisseur.Text;

                        connection.Open();

                        // 3. Créez une commande SQL d'insertion
                        string sql = "INSERT INTO fournisseur (nomFournisseur, contact, email, adresse) VALUES (@NomFournisseur, @ContactFournisseur, @EmailFournisseur, @AdresseFournisseur)";
                        using (SQLiteCommand co = new SQLiteCommand(sql, connection))
                        {
                            // 4. Ajoutez des paramètres pour les valeurs que vous souhaitez insérer
                            co.Parameters.AddWithValue("@NomFournisseur", nomF); // Utilisez le contrôle NomFournisseur.Text ici
                            co.Parameters.AddWithValue("@ContactFournisseur", contact); // Utilisez le contrôle ContactFourisseur.Text ici
                            co.Parameters.AddWithValue("@EmailFournisseur", emailF); // Utilisez le contrôle EmailFournisseur.Text ici
                            co.Parameters.AddWithValue("@AdresseFournisseur", adresseF); // Utilisez le contrôle AdresseFournisseur.Text ici

                            // 5. Exécutez la commande SQL
                            co.ExecuteNonQuery();
                            MessageBox.Show("Successfully");
                        }
                    }
                    afficheListeFournisseur();
                }
                else
                {
                    MessageBox.Show("E-mail non Valide", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir les champs nécessaires", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSupFournisseur_Click(object sender, EventArgs e)
        {
            if (dataGridViewListFournisseur.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Confirmez-vous la suppression de ce Fournisseur ?\nCette action est irréversible !", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {

                    DataGridViewRow selectedRow = dataGridViewListFournisseur.SelectedRows[0];
                    String valeurCellule = selectedRow.Cells[0].Value.ToString();
                    removeFournisseur(valeurCellule);
                    afficheListeFournisseur();
                }
            }
            else
            {
                MessageBox.Show("Veuillez Selectionner le Fournisseur à Supprimer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void panelNewFournisseur_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelNewFournisseur_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelNewFournisseur_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void ContactFourisseur_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+')
            {
                e.Handled = true;
            }
        }

        private void EmailFournisseur_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EmailFournisseur.Text))
            {
                EmailFournisseur.ForeColor = Color.Black;
                if (IsValidEmail(EmailFournisseur.Text))
                {
                    EmailFournisseur.FillColor = Color.PaleGreen;
                }
                else
                {
                    EmailFournisseur.FillColor = Color.LightPink;
                }
            }
            else
            {
                EmailFournisseur.FillColor = Color.White;
            }
        }

        private void popUpNewFournisseur_Load(object sender, EventArgs e)
        {
            afficheListeFournisseur();
        }

        private void txtBoxSearchFournisseur_TextChanged(object sender, EventArgs e)
        {
            string searchValueFournisseur = txtBoxSearchFournisseur.Text.ToString();
            searchFunction(searchValueFournisseur);
        }
    }
}

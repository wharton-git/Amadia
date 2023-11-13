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
        string idFournisseur = "0";
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

                string sqlQuery = "SELECT idFournisseur AS ID, nomFournisseur AS Nom, contact AS Contact, adresse AS Adresse, email AS Email FROM fournisseur WHERE idFournisseur LIKE '%" + searchTerme + "%' OR nomFournisseur LIKE '%" + searchTerme + "%' OR contact LIKE '%" + searchTerme + "%' OR adresse LIKE '%" + searchTerme + "%' OR email LIKE '%" + searchTerme + "%'";

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

        private void cacheActionModif()
        {
            btnValideModifFournisseur.Visible = btnAnnulerModifFournisseur.Visible = false;
            btnModifierFournisseur.Visible = true;
        }

        private void showActionModif()
        {
            btnValideModifFournisseur.Visible = btnAnnulerModifFournisseur.Visible = true;
            btnModifierFournisseur.Visible = false;
        }

        private void modifFournisseur(string id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();

                    string query = "UPDATE fournisseur SET nomFournisseur = @nom, contact = @contact, adresse = @adresse, email = @email WHERE idFournisseur = @id";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nom", NomFournisseurs.Text);
                        command.Parameters.AddWithValue("@contact", ContactFourisseur.Text);
                        command.Parameters.AddWithValue("@adresse", AdresseFournisseur.Text);
                        command.Parameters.AddWithValue("@email", EmailFournisseur.Text);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérez l'exception ici (par exemple, affichez un message d'erreur)
                Console.WriteLine("Erreur lors de la mise à jour : " + ex.Message);
            }
        }

        private void clear()
        {
            NomFournisseurs.Text = ContactFourisseur.Text = AdresseFournisseur.Text = EmailFournisseur.Text = string.Empty;
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

                        string sql = "INSERT INTO fournisseur (nomFournisseur, contact, email, adresse) VALUES (@NomFournisseur, @ContactFournisseur, @EmailFournisseur, @AdresseFournisseur)";
                        using (SQLiteCommand co = new SQLiteCommand(sql, connection))
                        {
                            co.Parameters.AddWithValue("@NomFournisseur", nomF);
                            co.Parameters.AddWithValue("@ContactFournisseur", contact);
                            co.Parameters.AddWithValue("@EmailFournisseur", emailF);
                            co.Parameters.AddWithValue("@AdresseFournisseur", adresseF);

                            co.ExecuteNonQuery();
                            MessageBox.Show("Successfully");
                        }
                    }
                    afficheListeFournisseur();
                    clear();
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

        private void ContactFourisseur_KeyDown(object sender, KeyEventArgs e)
        {
            // Capture la touche Backspace (Suppression) et la touche Delete
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                e.Handled = false; // Autorise la suppression
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
            cacheActionModif();
        }

        private void txtBoxSearchFournisseur_TextChanged(object sender, EventArgs e)
        {
            string searchValueFournisseur = txtBoxSearchFournisseur.Text.ToString();
            searchFunction(searchValueFournisseur);
        }

        private void btnModifierFournisseur_Click(object sender, EventArgs e)
        {
            if (dataGridViewListFournisseur.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridViewListFournisseur.SelectedRows[0];
                idFournisseur = selectedRow.Cells[0].Value.ToString();
                String nomFournisseur = selectedRow.Cells[1].Value.ToString();
                String contactFournisseur = selectedRow.Cells[2].Value.ToString();
                String adresseFournisseur = selectedRow.Cells[3].Value.ToString();
                String emailFournisseur = selectedRow.Cells[4].Value.ToString();

                NomFournisseurs.Text = nomFournisseur;
                ContactFourisseur.Text = contactFournisseur;
                AdresseFournisseur.Text = adresseFournisseur;
                EmailFournisseur.Text = emailFournisseur;

                showActionModif();
            }
            else
            {
                MessageBox.Show("Veuillez Selectionner le Fournisseur à Modifier", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAnnulerModifFournisseur_Click(object sender, EventArgs e)
        {
            cacheActionModif();
            clear();
        }

        private void btnValideModifFournisseur_Click(object sender, EventArgs e)
        {
            modifFournisseur(idFournisseur);
            cacheActionModif();
            afficheListeFournisseur();
            clear();

        }

    }
}

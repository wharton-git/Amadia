using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using static iTextSharp.text.pdf.PdfDocument;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpAddMember : Form
    {
        //Declaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;


        //Constructeur
        public popUpAddMember()
        {
            InitializeComponent();
        }

        //Méthodes
        private string getAmountAdh()
        {
            string result = null;
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM value_adhesion";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            return result;
        }

        private string getIdMembre(string nom, string prenom)
        {
            string result = null;
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT id_membre FROM membre WHERE nom_membre LIKE @nom AND prenom_membre LIKE @prenom ";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@nom", nom);
                    command.Parameters.AddWithValue("@prenom", prenom);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            return result;
        }

        private void makeAdhesion(string id, string droit)
        {
            string droit_adhesion = getAmountAdh();
            int restant = Convert.ToInt32(droit_adhesion) - Convert.ToInt32(droit);
            string droit_restant = Convert.ToString(restant);
            string date_adhesion = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO adhesion (id_membre, droit_payee, droit_restant, droit_adhesion, date_adhesion) VALUES (@id, @droit_payee, @droit_restant, @droit_adhesion, @dateAdhesion)";
                    using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@droit_payee", droit);
                        command.Parameters.AddWithValue("@droit_restant", droit_restant);
                        command.Parameters.AddWithValue("@droit_adhesion", droit_adhesion);
                        command.Parameters.AddWithValue("@dateAdhesion", date_adhesion);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur avec l'adhésion: " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private void addMember(string numero, string nom, string prenom, string adresse, string contact1, string contact2, string dateNaiss, string droit)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();

                    // Vérifier si l'ID ou le numéro existe déjà
                    string checkQuery = "SELECT COUNT(*) FROM membre WHERE id_membre = @id OR id_membre = @numero";
                    using (SqliteCommand checkCommand = new SqliteCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@id", numero);
                        checkCommand.Parameters.AddWithValue("@numero", numero);

                        int existingRecords = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (existingRecords > 0)
                        {
                            MessageBox.Show("Un membre avec le numéro existe déjà. Veuillez choisir un numéro différent.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return; // Sortir de la fonction si l'ID ou le numéro existe déjà
                        }
                    }

                    // Continuer avec l'insertion si l'ID ou le numéro n'existe pas encore
                    string dateAdhesion = DateTime.Today.ToString("yyyy-MM-dd");

                    string insertQuery = "INSERT INTO membre (id_membre, nom_membre, prenom_membre, adresse, contact, contact2, date_naiss, date_adhesion) VALUES (@id, @nom, @prenom, @adresse, @contact, @contact2, @dateNaiss, @dateAdhesion)";

                    if (string.IsNullOrEmpty(numero))
                    {
                        insertQuery = "INSERT INTO membre (nom_membre, prenom_membre, adresse, contact, contact2, date_naiss, date_adhesion) VALUES (@nom, @prenom, @adresse, @contact, @contact2, @dateNaiss, @dateAdhesion)";
                    }

                    using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@id", numero);
                        command.Parameters.AddWithValue("@nom", nom);
                        command.Parameters.AddWithValue("@prenom", prenom);
                        command.Parameters.AddWithValue("@adresse", adresse);
                        command.Parameters.AddWithValue("@contact", contact1);
                        command.Parameters.AddWithValue("@contact2", contact2);
                        command.Parameters.AddWithValue("@dateNaiss", dateNaiss);
                        command.Parameters.AddWithValue("@dateAdhesion", dateAdhesion);
                        command.ExecuteNonQuery();
                    }

                    if (string.IsNullOrEmpty(numero))
                    {
                        numero = getIdMembre(nom, prenom);
                    }
                    makeAdhesion(numero, droit);
                }


                MessageBox.Show("Membre ajouté avec Succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void Reset()
        {
            txtBoxNumero.Text = string.Empty;
            txtBoxNom.Text = string.Empty;
            txtBoxPrenom.Text = string.Empty;
            txtBoxAdresse.Text = string.Empty;
            txtBoxContact1.Text = string.Empty;
            txtBoxContact2.Text = string.Empty;
            txtBoxDroitAdh.Text = getAmountAdh();
            dateTimePickerBirth.Value = DateTime.Today;
        }


        //Evenements
        private void popUpAddMember_Load(object sender, EventArgs e)
        {
            dateTimePickerBirth.Value = DateTime.Today;
            txtBoxDroitAdh.Text = getAmountAdh();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelAddList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelAddList_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelAddList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void btnSaveMember_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxAdresse.Text) && !string.IsNullOrEmpty(txtBoxContact1.Text) && !string.IsNullOrEmpty(txtBoxNom.Text))
            {
                string numero = txtBoxNumero.Text;
                string nom = txtBoxNom.Text;
                string prenom = txtBoxPrenom.Text;
                string adresse = txtBoxAdresse.Text;
                string contact1 = txtBoxContact1.Text;
                string contact2 = txtBoxContact2.Text;
                string dateNaisse = dateTimePickerBirth.Value.ToString("yyyy-MM-dd");
                string droit = txtBoxDroitAdh.Text;

                DialogResult result = MessageBox.Show("Voulez-vous vraiment ajouter ce membre ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    addMember(numero, nom, prenom, adresse, contact1, contact2, dateNaisse, droit);
                    Reset();

                }
            }
            else
            {
                MessageBox.Show("Veuiller remplir les information nécessairement obligatoire ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnInfoAddMember_Click(object sender, EventArgs e)
        {
            popUp.popUpAddMemberInfo popUp = new popUpAddMemberInfo();
            popUp.ShowDialog();
            popUp.Dispose();
        }

        private void btnModifDroitAdhesion_Click(object sender, EventArgs e)
        {
            popUp.popUpChangeAdhesion popUp = new popUpChangeAdhesion();
            popUp.ShowDialog();
            popUp.Dispose();
            txtBoxDroitAdh.Text = getAmountAdh();
        }
    }
}

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
using AmadiaVente.Winforms.functionality;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpModifierMembre : Form
    {
        //Declaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;

        string idMembre = null;

        //Constructeur
        public popUpModifierMembre(string id)
        {
            InitializeComponent();
            idMembre = id;
        }

        //Méthodes
        private string GetStringOrDefault(SqliteDataReader reader, int ordinal)
        {
            return reader.IsDBNull(ordinal) ? " " : reader.GetString(ordinal);
        }

        private String[] takeInfoMembre(string id)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM membre WHERE id_membre=@id";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string id_membre = GetStringOrDefault(reader, 0);
                            string nom_membre = GetStringOrDefault(reader, 1);
                            string prenom_membre = GetStringOrDefault(reader, 2);
                            string adresse_membre = GetStringOrDefault(reader, 3);
                            string contact1_membre = GetStringOrDefault(reader, 4);
                            string contact2_membre = GetStringOrDefault(reader, 5);
                            string date_naiss_membre = GetStringOrDefault(reader, 6);


                            return new string[] { id_membre, nom_membre, prenom_membre, adresse_membre, contact1_membre, contact2_membre, date_naiss_membre };
                        }
                    }
                }
            }
            return null;
        }

        private void UpdateMemberData(string id, string nom, string prenom, string adresse, string contact1, string contact2, DateTime dateNaiss)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();

                    string query = "UPDATE membre SET nom_membre = @nom, prenom_membre = @prenom, adresse = @adresse, contact = @contact1, contact2 = @contact2, date_naiss = @dateNaiss WHERE id_membre = @id";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nom", nom);
                        command.Parameters.AddWithValue("@prenom", prenom);
                        command.Parameters.AddWithValue("@adresse", adresse);
                        command.Parameters.AddWithValue("@contact1", contact1);
                        command.Parameters.AddWithValue("@contact2", contact2);
                        command.Parameters.AddWithValue("@dateNaiss", dateNaiss.ToString("yyyy-MM-dd"));

                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Les données du membre ont été mises à jour avec succès.", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Evenements

        private void popUpModifierMembre_Load(object sender, EventArgs e)
        {
            string[] info = takeInfoMembre(idMembre);

            if (info[0] != null)
            {
                txtBoxNumero.Text = info[0];
                txtBoxNom.Text = info[1];
                txtBoxPrenom.Text = info[2];
                txtBoxAdresse.Text = info[3];
                txtBoxContact1.Text = info[4];
                txtBoxContact2.Text = info[5];
                dateTimePickerBirth.Value = DateTime.Parse(info[6]);
            }
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

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveModifMember_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxAdresse.Text) && !string.IsNullOrEmpty(txtBoxContact1.Text) && !string.IsNullOrEmpty(txtBoxNom.Text))
            {
                string id = txtBoxNumero.Text;
                string nom = txtBoxNom.Text;
                string prenom = txtBoxPrenom.Text;
                string adresse = txtBoxAdresse.Text;
                string contact1 = txtBoxContact1.Text;
                string contact2 = txtBoxContact2.Text;
                DateTime dateNaiss = dateTimePickerBirth.Value;

                DialogResult result = MessageBox.Show("Etes-vous sûr de vouloir appliquer les modifications ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    UpdateMemberData(id, nom, prenom, adresse, contact1, contact2, dateNaiss);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Information obligatoire incomplète", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}

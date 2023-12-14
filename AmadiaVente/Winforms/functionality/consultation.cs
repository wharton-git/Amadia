using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace AmadiaVente.Winforms.functionality
{
    public partial class consultation : Form
    {
        //Declaration Globale
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");




        //Méthodes
        private void resetBoxMembre()
        {
            txtBoxNumeroMembre.Enabled = false;
            comboBoxNomMembre.Enabled = false;
            txtBoxNumeroMembre.Text = string.Empty;
            comboBoxNomMembre.SelectedItem = null;
        }

        private void reset()
        {
            resetBoxMembre();
            comboBoxMembre.SelectedItem = null;
            txtBoxPrix.Text = string.Empty;
            txtBoxNumeroMembre.Text = string.Empty;
            comboBoxNomMembre.SelectedItem = null;
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

        private string[] getAmountConsultationM()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM value_consultation WHERE type LIKE 'M'";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string cons_membre = reader.GetString(0);
                            string cons_non_membre = reader.GetString(1);

                            return new string[] { cons_membre, cons_non_membre };
                        }
                    }
                }
            }
            return null;
        }

        private string[] getAmountConsultationNM()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM value_consultation WHERE type LIKE 'NM'";

                using (SqliteCommand command = new SqliteCommand(selectMedicamentsQuery, connection))
                {

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();

                            string cons_membre = reader.GetString(0);
                            string cons_non_membre = reader.GetString(1);

                            return new string[] { cons_membre, cons_non_membre };
                        }
                    }
                }
            }
            return null;
        }

        private void saveConsultation(string id, int prix)
        {
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO consultation (numero_membre, prix_consultation, date_consultation) VALUES (@id, @prix, @date)";
                    using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            command.Parameters.AddWithValue("@id", id);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@id", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@prix", prix);
                        command.Parameters.AddWithValue("@date", today);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Consultation Engregistré !", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reset();

            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Constructeur
        public consultation()
        {
            InitializeComponent();
        }

        //Evenements
        private void consultation_Load(object sender, EventArgs e)
        {
            comboBoxMembre.Items.Add("Oui");
            comboBoxMembre.Items.Add("Non");
            resetBoxMembre();

            afficheNomMembreLoad();

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

        private void txtBoxNumeroMembre_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxNumeroMembre.Text))
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

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void comboBoxMembre_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] prix_consultationM = getAmountConsultationM();
            string[] prix_consultationNM = getAmountConsultationNM();

            if (comboBoxMembre.SelectedItem == "Oui")
            {
                txtBoxNumeroMembre.Enabled = true;
                comboBoxNomMembre.Enabled = true;
                txtBoxPrix.Text = prix_consultationM[1];
            }
            else
            {
                resetBoxMembre();
                txtBoxPrix.Text = prix_consultationNM[1];
            }
        }

        private void txtBoxPrix_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBoxNumeroMembre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBoxPrix_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnValiderConsultation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBoxPrix.Text))
            {
                string id = null;

                if (comboBoxMembre.SelectedItem == "Oui")
                {
                    if (!string.IsNullOrEmpty(txtBoxNumeroMembre.Text))
                    {
                        id = txtBoxNumeroMembre.Text.ToString();

                        int prix = Convert.ToInt32(txtBoxPrix.Text.ToString());

                        saveConsultation(id, prix);
                    }
                    else
                    {
                        MessageBox.Show("Erreur : Veuillez Choisir un membre", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    int prix = Convert.ToInt32(txtBoxPrix.Text.ToString());

                    saveConsultation(id, prix);
                }

            }
            else
            {
                MessageBox.Show("Erreur : Champ obligatoire non rempli", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

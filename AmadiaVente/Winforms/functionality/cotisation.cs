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

namespace AmadiaVente.Winforms.functionality
{
    public partial class cotisation : Form
    {
        //Declaration globales
        string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        string sessionId;

        List<string> mois = new List<string>
            {
                "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
                "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
            };


        //Constructeur
        public cotisation()
        {
            InitializeComponent();
        }

        //Methodes
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

        private void reset()
        {
            comboBoxNomMembre.SelectedItem = null;
            txtBoxNumeroMembre.Clear();
            txtBoxSommeCot.Clear();
            comboBoxMoisCot.SelectedItem = null;
        }

        private void AfficherInformationsCotisation(string mois, int annee)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();

                    // Sélectionnez toutes les colonnes de la table "cotisation"
                    string query = "SELECT numero_membre AS 'Numéro', m.nom_membre AS Nom, m.prenom_membre AS Prénom, periode AS 'Mois', date_payement AS 'Payé le', payee AS Payée, restant AS Restant FROM cotisation c INNER JOIN membre m ON c.numero_membre = m.id_membre WHERE periode = @periode AND annee = @annee;";

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@periode", mois);
                        command.Parameters.AddWithValue("@annee", annee);

                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            // Créez une DataTable pour stocker les résultats
                            DataTable dataTable = new DataTable();

                            // Remplissez la DataTable avec les résultats du lecteur
                            dataTable.Load(reader);

                            // Assurez-vous que votre DataGridView est nommé dataGridView1 (vous pouvez ajuster selon le nom réel)
                            dataGridViewCotisationStatus.DataSource = dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void validerCotisation(string idMembre, string periode, string payee, int annee)
        {
            bool statut = true;
            string datePayement = DateTime.Today.ToString("yyyy-MM-dd");
            int somme = 20000;
            int restant = somme - Convert.ToInt32(payee);

            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();

                    // Vérifiez si un enregistrement existe déjà pour le même membre et la même période
                    string checkExistingQuery = "SELECT COUNT(*) FROM cotisation WHERE numero_membre = @numero_membre AND periode = @periode";

                    using (SqliteCommand checkExistingCommand = new SqliteCommand(checkExistingQuery, connection))
                    {
                        checkExistingCommand.Parameters.AddWithValue("@numero_membre", idMembre);
                        checkExistingCommand.Parameters.AddWithValue("@periode", periode);

                        int existingRecordsCount = Convert.ToInt32(checkExistingCommand.ExecuteScalar());

                        // Si un enregistrement existe déjà, affichez un message d'erreur
                        if (existingRecordsCount > 0)
                        {
                            MessageBox.Show("Cet utilisateur a déjà payé pour cette période.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // Si aucun enregistrement n'existe, effectuez l'insertion
                    string insertQuery = "INSERT INTO cotisation (numero_membre, status, periode, annee, date_payement, somme, payee, restant) VALUES (@numero_membre, @statut, @periode, @annee, @date_payement, @somme, @payee, @restant)";

                    using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@numero_membre", idMembre);
                        command.Parameters.AddWithValue("@statut", statut);
                        command.Parameters.AddWithValue("@periode", periode);
                        command.Parameters.AddWithValue("@annee", annee);
                        command.Parameters.AddWithValue("@date_payement", datePayement);
                        command.Parameters.AddWithValue("@somme", somme);
                        command.Parameters.AddWithValue("@payee", payee);
                        command.Parameters.AddWithValue("@restant", restant);

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Cotisation Validée", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erreur : " + e.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public string getAmountCot()
        {
            string result = null;
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string selectMedicamentsQuery = "SELECT * FROM value_cotisation";

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

        //Evenements
        private void btnShowAndHideOption_Click(object sender, EventArgs e)
        {
            if (btnShowAndHideOption.Text == "Voir les détailles de la cotisation")
            {
                btnShowAndHideOption.Text = "Cacher les détailles de la cotisation";
                panelCotisation.Visible = true;

            }
            else
            {
                btnShowAndHideOption.Text = "Voir les détailles de la cotisation";
                panelCotisation.Visible = false;
            }
        }

        private void cotisation_Load(object sender, EventArgs e)
        {
            panelCotisation.Visible = false;
            afficheNomMembreLoad();

            txtBoxSommeCot.Text = getAmountCot();

            foreach (string nomMois in mois)
            {
                comboBoxMoisCot.Items.Add(nomMois);
                comboBoxMoisDetailCot.Items.Add(nomMois);
            }

            DateTime currentMonth = DateTime.Today;
            String numMois = currentMonth.ToString("MM");
            MessageBox.Show(currentMonth.Month.ToString("MM"));
            string moisSelect = string.Empty;
            switch (numMois)
            {
                case "1":
                    moisSelect = "Janvier";
                    break;
                case "2":
                    moisSelect = "Février";
                    break;
                case "3":
                    moisSelect = "Mars";
                    break;
                case "4":
                    moisSelect = "Avril";
                    break;
                case "5":
                    moisSelect = "Mai";
                    break;
                case "6":
                    moisSelect = "Juin";
                    break;
                case "7":
                    moisSelect = "Juillet";
                    break;
                case "8":
                    moisSelect = "Août";
                    break;
                case "9":
                    moisSelect = "Septembre";
                    break;
                case "10":
                    moisSelect = "Octobre";
                    break;
                case "11":
                    moisSelect = "Novembre";
                    break;
                case "12":
                    moisSelect = "Décembre";
                    break;
                default:
                    moisSelect = DateTime.Now.Month.ToString("D2");
                    break;
            }

            comboBoxMoisCot.SelectedItem = moisSelect;
            comboBoxMoisDetailCot.SelectedItem = moisSelect;


            for (int annee = 2005; annee <= 2050; annee++)
            {
                comboBoxAnneeCot.Items.Add(annee);
            }

            int currentYear = DateTime.Today.Year;
            comboBoxAnneeCot.SelectedItem = currentYear;

            string moisActuelle = comboBoxMoisDetailCot.SelectedItem.ToString();
            AfficherInformationsCotisation(moisActuelle, currentYear);

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

        private void txtBoxNumeroMembre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBoxSommeCot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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

        private void btnValiderCotisation_Click(object sender, EventArgs e)
        {
            string numeroMembre = txtBoxNumeroMembre.Text;
            string moisDeCotisation = comboBoxMoisCot.SelectedItem.ToString();
            string montant = txtBoxSommeCot.Text;
            int annee = Convert.ToInt32(DateTime.Today.ToString("yyyy"));

            validerCotisation(numeroMembre, moisDeCotisation, montant, annee);
        }

        private void btnAnnulerCotisation_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void comboBoxMoisDetailCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            string month = comboBoxMoisDetailCot.SelectedItem.ToString();
            int year = 2023;

            if (comboBoxAnneeCot.SelectedItem != null && comboBoxMoisDetailCot.SelectedItem != null)
            {
                year = Convert.ToInt32(comboBoxAnneeCot.SelectedItem.ToString());
                month = comboBoxMoisDetailCot.SelectedItem.ToString();
            }

            AfficherInformationsCotisation(month, year);
        }

        private void comboBoxAnneeCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            string month = comboBoxMoisDetailCot.SelectedItem.ToString();
            int year = Convert.ToInt32(comboBoxAnneeCot.SelectedItem.ToString());

            AfficherInformationsCotisation(month, year);
        }

        private void btnChangeCotisation_Click(object sender, EventArgs e)
        {
            popUp.popUpChangeCotisation popup = new popUp.popUpChangeCotisation();

            popup.ShowDialog();
            popup.Dispose();
            txtBoxSommeCot.Text = getAmountCot();
        }

    }
}

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
    }
}

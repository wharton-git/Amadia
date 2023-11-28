using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic.ApplicationServices;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;

namespace AmadiaVente.Winforms.functionality
{
    public partial class rendu : Form
    {
        // Déclaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");
        int id_commande = 0;
        DateTime dateActuelle = DateTime.Today;

        List<string> mois = new List<string>
            {
                "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
                "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
            };

        // Constructeur
        public rendu()
        {
            InitializeComponent();

        }

        //Méthodes

        string[] compterCommandeAujourdhui(DateTime jour)
        {
            string[] result = new string[2];

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT(c.id_commande), SUM(lc.prix) FROM commande c INNER JOIN ligneCommande lc ON lc.id_commande = c.id_commande INNER JOIN user u ON u.id_user = c.id_responsable WHERE date_achat > @jour";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@jour", jour);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result[0] = reader[0].ToString();
                            result[1] = reader[1].ToString();
                        }
                    }
                }
            }

            return result;
        }

        string[] compterCommandeJour(string jour)
        {
            string[] result = new string[2];

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT(c.id_commande), SUM(lc.prix) FROM commande c INNER JOIN ligneCommande lc ON lc.id_commande = c.id_commande INNER JOIN user u ON u.id_user = c.id_responsable WHERE STRFTIME('%Y-%m-%d', date_achat) = @jour";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@jour", jour);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result[0] = reader[0].ToString();
                            result[1] = reader[1].ToString();
                        }
                    }
                }
            }

            return result;
        }

        string[] compterCommandeMois(string mois)
        {
            string[] result = new string[2]; // Pour stocker le nombre de commandes et la somme des prix

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT(c.id_commande), SUM(lc.prix) FROM commande c INNER JOIN ligneCommande lc ON lc.id_commande = c.id_commande INNER JOIN user u ON u.id_user = c.id_responsable WHERE strftime('%m', date_achat) = @mois";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@mois", mois);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result[0] = reader[0].ToString();
                            result[1] = reader[1].ToString();
                        }
                    }
                }
            }

            return result;
        }

        string[] compterCommandeAnnee(string annee)
        {
            string[] result = new string[2];

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT(c.id_commande), SUM(lc.prix) FROM commande c INNER JOIN ligneCommande lc ON lc.id_commande = c.id_commande INNER JOIN user u ON u.id_user = c.id_responsable WHERE strftime('%Y', date_achat) = @annee";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@annee", annee);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result[0] = reader[0].ToString();
                            result[1] = reader[1].ToString();
                        }
                    }
                }
            }

            return result;
        }

        string[] compterCommande2Date(DateTime debut, DateTime fin)
        {
            string[] result = new string[2];

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT COUNT(c.id_commande), SUM(lc.prix) FROM commande c INNER JOIN ligneCommande lc ON lc.id_commande = c.id_commande INNER JOIN user u ON u.id_user = c.id_responsable WHERE date_achat BETWEEN @debut AND @fin";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@debut", debut);
                    command.Parameters.AddWithValue("@fin", fin);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result[0] = reader[0].ToString();
                            result[1] = reader[1].ToString();
                        }
                    }
                }
            }

            return result;
        }

        void transitionVariable(SqliteDataReader reader)
        {
            // Créez des listes pour stocker les valeurs des colonnes
            List<int> idCommandes = new List<int>();
            List<string> nomsClients = new List<string>();
            List<string> prenomsClients = new List<string>();
            List<string> nomsResponsables = new List<string>();
            List<string> prenomsResponsables = new List<string>();
            List<string> dates = new List<string>();

            dataGridViewDashboard.Rows.Clear();

            dataGridViewDashboard.DataSource = null;
            // Lisez les données depuis le DataReader et stockez-les dans les listes
            while (reader.Read())
            {
                idCommandes.Add(reader.GetInt32(0));
                nomsClients.Add(reader.GetString(1));
                prenomsClients.Add(reader.GetString(2));
                nomsResponsables.Add(reader.GetString(3));
                prenomsResponsables.Add(reader.GetString(4));
                dates.Add(reader.GetString(5));
            }

            // Ajoutez les valeurs des listes au DataGridView à l'aide d'une boucle
            for (int i = 0; i < idCommandes.Count; i++)
            {
                dataGridViewDashboard.Rows.Add(idCommandes[i], nomsClients[i], prenomsClients[i], nomsResponsables[i], prenomsResponsables[i], dates[i]);
            }
        }

        void afficheRechercheNomEtPrenom(string nomPrenom)
        {
            String nom = nomPrenom;
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_commande AS ID_Commande, COALESCE(CAST(nom_membre AS TEXT), 'Non') AS Nom_Client, COALESCE(CAST(prenom_membre AS TEXT), 'Membre') AS Prénom_Client, nom_user AS Nom_Responsable, prenom_user AS Prénom_Responsable, strftime('%d/%m/%Y %H:%M', c.date_achat) AS Date FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE m.nom_membre LIKE '%" + nom + "%' OR m.prenom_membre LIKE '%" + nom + "%'  OR id_commande LIKE '%" + nom + "%'";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        transitionVariable(reader);
                    }
                }
            }
        }

        void afficherRecetteDuJour(DateTime date)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_commande AS ID_Commande, COALESCE(CAST(nom_membre AS TEXT), 'Non') AS Nom_Client, COALESCE(CAST(prenom_membre AS TEXT), 'Membre') AS Prénom_Client, nom_user AS Nom_Responsable, prenom_user AS Prénom_Responsable, strftime('%d/%m/%Y %H:%M', c.date_achat) AS Date FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE date_achat > @dateNow";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@dateNow", date);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        transitionVariable(reader);
                    }
                }
            }

        }

        void afficheRecetteAnnee(string annee)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_commande AS ID_Commande, COALESCE(CAST(nom_membre AS TEXT), 'Non') AS Nom_Client, COALESCE(CAST(prenom_membre AS TEXT), 'Membre') AS Prénom_Client, nom_user AS Nom_Responsable, prenom_user AS Prénom_Responsable, strftime('%d/%m/%Y %H:%M', c.date_achat) AS Date FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE strftime('%Y', date_achat) LIKE '%" + annee + "%'";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        transitionVariable(reader);
                    }
                }
            }
        }


        void afficheRecetteEntre2Dates(DateTime debut, DateTime fin)
        {
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();

                string query = "SELECT id_commande AS ID_Commande, COALESCE(CAST(nom_membre AS TEXT), 'Non') AS Nom_Client, COALESCE(CAST(prenom_membre AS TEXT), 'Membre') AS Prénom_Client, nom_user AS Nom_Responsable, prenom_user AS Prénom_Responsable, strftime('%d/%m/%Y %H:%M', c.date_achat) AS Date FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE date_achat BETWEEN @debut AND @fin";

                using (SqliteCommand command = new SqliteCommand(query, con))
                {
                    command.Parameters.AddWithValue("@debut", debut);
                    command.Parameters.AddWithValue("@fin", fin);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        transitionVariable(reader);
                    }
                }
            }
        }

        void afficheRecetteMois(string mois)
        {
            using (SqliteConnection con = new SqliteConnection(cs))
            {
                con.Open();

                string query = "SELECT id_commande AS ID_Commande, COALESCE(CAST(nom_membre AS TEXT), 'Non') AS Nom_Client, COALESCE(CAST(prenom_membre AS TEXT), 'Membre') AS Prénom_Client, nom_user AS Nom_Responsable, prenom_user AS Prénom_Responsable, strftime('%d/%m/%Y %H:%M', c.date_achat) AS Date FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE strftime('%m', date_achat) = @mois;";

                using (SqliteCommand command = new SqliteCommand(query, con))
                {
                    command.Parameters.AddWithValue("@mois", mois);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        transitionVariable(reader);
                    }
                }
            }
        }


        void afficheRecetteJour(string jour)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_commande AS ID_Commande, COALESCE(CAST(nom_membre AS TEXT), 'Non') AS Nom_Client, COALESCE(CAST(prenom_membre AS TEXT), 'Membre') AS Prénom_Client, nom_user AS Nom_Responsable, prenom_user AS Prénom_Responsable, strftime('%d/%m/%Y %H:%M', c.date_achat) AS Date FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE STRFTIME('%Y-%m-%d', date_achat) = @jour";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@jour", jour);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        transitionVariable(reader);
                    }
                }
            }
        }


        void change2date()
        {
            DateTime dateDebut = dateTimeDebut.Value;
            DateTime dateFin;

            if (dateDebut < dateTimeFin.Value)
            {
                dateFin = dateTimeFin.Value.AddDays(1);
            }
            else
            {
                dateFin = dateDebut.AddDays(3);
                string formattedDate = dateDebut.AddDays(2).ToString("yyyy-MMMM-dd");
                dateTimeFin.Text = formattedDate;
            }
            afficheRecetteEntre2Dates(dateDebut, dateFin);

            string[] val = compterCommande2Date(dateDebut, dateFin);
            int nbrCommande = dataGridViewDashboard.Rows.Count;
            labelCommande.Text = "Total commande : " + nbrCommande;
            labelSomme.Text = "Somme : " + val[1] + " Ar";
        }

        // Événements

        private void rendu_Load_1(object sender, EventArgs e)
        {

            btnInfoCommande.Enabled = false;
            afficherRecetteDuJour(dateActuelle);
            string[] val = compterCommandeAujourdhui(dateActuelle);
            int nbrCommande = dataGridViewDashboard.Rows.Count;
            labelCommande.Text = "Total commande : " + nbrCommande;
            labelSomme.Text = "Somme : " + val[1] + " Ar";

            //comboBox
            comboBoxPeriode.Items.Add("Aujourd'hui");
            comboBoxPeriode.Items.Add("Un jour");
            comboBoxPeriode.Items.Add("Un mois");
            comboBoxPeriode.Items.Add("Une année");
            comboBoxPeriode.Items.Add("Entre 2 dates");
            comboBoxPeriode.SelectedItem = "Aujourd'hui";

            foreach (string nomMois in mois)
            {
                comboBoxMois.Items.Add(nomMois);
            }

            if (dataGridViewDashboard.Rows.Count == 0)
            {
                //Important, ne pas modifier ou supprimer sans connaissance de cause
                //initialiserDataGridRendu();
                //afficherRecetteDuJour(dateActuelle);

            }


            //disable visibility
            comboBoxMois.Visible = txtBoxAnnee.Visible = dateTimeJour.Visible = panel2Date.Visible = false;
        }

        private void btnInfoCommande_Click(object sender, EventArgs e)
        {
            popUp.commandeDetails popup = new popUp.commandeDetails(id_commande);

            // Affichez la fenêtre contextuelle en mode modal (PopUp)
            popup.ShowDialog();

            // Libérez les ressources de la fenêtre contextuelle après qu'elle a été fermée
            popup.Dispose();
        }

        private void comboBoxPeriode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPeriode.SelectedItem == "Un jour")
            {
                comboBoxMois.Visible = txtBoxAnnee.Visible = panel2Date.Visible = false;
                dateTimeJour.Visible = true;
                btnInfoCommande.Enabled = false;
            }
            else if (comboBoxPeriode.SelectedItem == "Un mois")
            {
                dateTimeJour.Visible = txtBoxAnnee.Visible = panel2Date.Visible = false;
                comboBoxMois.Visible = true;
                btnInfoCommande.Enabled = false;
            }
            else if (comboBoxPeriode.SelectedItem == "Une année")
            {
                dateTimeJour.Visible = comboBoxMois.Visible = panel2Date.Visible = false;
                txtBoxAnnee.Visible = true;
                btnInfoCommande.Enabled = false;
            }
            else if (comboBoxPeriode.SelectedItem == "Entre 2 dates")
            {
                dateTimeJour.Visible = comboBoxMois.Visible = txtBoxAnnee.Visible = false;
                panel2Date.Visible = true;
                btnInfoCommande.Enabled = false;
            }
            else
            {
                comboBoxMois.Visible = txtBoxAnnee.Visible = dateTimeJour.Visible = panel2Date.Visible = false;

                afficherRecetteDuJour(dateActuelle);
                btnInfoCommande.Enabled = false;
            }
        }

        private void dateTimeDebut_ValueChanged(object sender, EventArgs e)
        {
            change2date();
        }

        private void dateTimeFin_ValueChanged(object sender, EventArgs e)
        {
            change2date();
        }

        private void txtBoxAnnee_TextChanged(object sender, EventArgs e)
        {
            string annee = txtBoxAnnee.Text;
            afficheRecetteAnnee(annee);
            string[] val = compterCommandeAnnee(annee);
            int nbrCommande = dataGridViewDashboard.Rows.Count;
            labelCommande.Text = "Total commande : " + nbrCommande;
            labelSomme.Text = "Somme : " + val[1] + " Ar";
        }

        private void txtBoxAnnee_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void comboBoxMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mois = "";

            switch (comboBoxMois.SelectedItem.ToString())
            {
                case "Janvier":
                    mois = "01";
                    break;
                case "Février":
                    mois = "02";
                    break;
                case "Mars":
                    mois = "03";
                    break;
                case "Avril":
                    mois = "04";
                    break;
                case "Mai":
                    mois = "05";
                    break;
                case "Juin":
                    mois = "06";
                    break;
                case "Juillet":
                    mois = "07";
                    break;
                case "Août":
                    mois = "08";
                    break;
                case "Septembre":
                    mois = "09";
                    break;
                case "Octobre":
                    mois = "10";
                    break;
                case "Novembre":
                    mois = "11";
                    break;
                case "Décembre":
                    mois = "12";
                    break;
                default:
                    mois = DateTime.Now.Month.ToString("D2");
                    break;
            }

            afficheRecetteMois(mois);

            string[] val = compterCommandeMois(mois);
            int nbrCommande = dataGridViewDashboard.Rows.Count;
            labelCommande.Text = "Total commande : " + nbrCommande;
            labelSomme.Text = "Somme : " + val[1] + " Ar";
        }

        private void dateTimeJour_ValueChanged(object sender, EventArgs e)
        {
            string jour = dateTimeJour.Value.ToString("yyy-MM-dd");
            afficheRecetteJour(jour);

            string[] val = compterCommandeJour(jour);
            int nbrCommande = dataGridViewDashboard.Rows.Count;
            labelCommande.Text = "Total commande : " + nbrCommande;
            labelSomme.Text = "Somme : " + val[1] + " Ar";
        }

        private void dataGridViewDashboard_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnInfoCommande.Enabled = true;
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dataGridViewDashboard.Rows[e.RowIndex];

                string idCommande = selectedRow.Cells["id"].Value.ToString();
                id_commande = Convert.ToInt32(idCommande);
            }
        }

        private void txtboxSearchRendu_TextChanged(object sender, EventArgs e)
        {
            String searchValue = txtboxSearchRendu.Text;

            if (!String.IsNullOrWhiteSpace(searchValue))
            {
                afficheRechercheNomEtPrenom(searchValue);
            }
            else
            {
                afficherRecetteDuJour(dateActuelle);
            }
        }

        private void dataGridViewDashboard_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Format Incorrect", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            Winforms.popUp.popUpRendu popup = new Winforms.popUp.popUpRendu();

            // Affichez la fenêtre contextuelle en mode modal (PopUp)
            popup.ShowDialog();

            // Libérez les ressources de la fenêtre contextuelle après qu'elle a été fermée
            popup.Dispose();

        }
    }
}

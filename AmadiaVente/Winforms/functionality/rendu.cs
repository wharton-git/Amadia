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
        int total = 0;
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

        private List<string> GetMedicIds(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Médicaments'";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listId.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return listId;
        }

        private string[] checkListIdMedicMembre(string id, string connectionString)
        {
            String[] result = new string[6];

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery2 = "SELECT lc.id_article, designation, 'M' as Membre, SUM(qte_acheter) AS QTE, CASE WHEN prix_membre = 0 THEN prix_article ELSE prix_membre END AS PU, SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article INNER JOIN commande c ON c.id_commande = lc.id_commande WHERE lc.id_article = @id AND c.id_membre IS NOT NULL";

                using (SqliteCommand command = new SqliteCommand(sqlQuery2, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                result[i] = reader.IsDBNull(i) ? null : reader[i].ToString();

                            }
                        }
                    }
                }
            }

            return result;
        }

        private string[] checkListIdMedicNonMembre(string id, string connectionString)
        {
            String[] result = new string[6];

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery2 = "SELECT lc.id_article, designation, 'NM' as 'Non Membre', SUM(qte_acheter) AS QTE, prix_article AS PU, SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article INNER JOIN commande c ON c.id_commande = lc.id_commande  WHERE lc.id_article = @id AND c.id_membre IS NULL";

                using (SqliteCommand command = new SqliteCommand(sqlQuery2, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                // Vérifiez si la valeur de la colonne est NULL avant de l'ajouter au tableau
                                result[i] = reader.IsDBNull(i) ? null : reader[i].ToString();
                            }
                        }
                    }
                }
            }

            return result;
        }

        private string totalPrixArticle(string id, string cs)
        {
            string result = null; // Initialisez result à une valeur par défaut, par exemple, null

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery2 = "SELECT SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE lc.id_article = @id";

                using (SqliteCommand command = new SqliteCommand(sqlQuery2, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assurez-vous de convertir la valeur en chaîne si nécessaire
                            result = reader["VALEUR"].ToString();
                        }
                    }
                }
            }
            return result;
        }


        static string FormatteSommeArgent(long montant)
        {
            // Utilisation de la classe NumberFormatInfo pour obtenir le séparateur de milliers
            NumberFormatInfo formatInfo = CultureInfo.CurrentCulture.NumberFormat;
            string separator = formatInfo.NumberGroupSeparator;

            // Convertir le montant en une chaîne formatée
            string formattedAmount = montant.ToString("N0");

            // Remplacer le séparateur de milliers par un espace
            formattedAmount = formattedAmount.Replace(separator, " ");

            return formattedAmount;
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
            total = 0;
            string dateDuJour = DateTime.Now.ToString("dd / MM / yyyy");

            string pdfLocation = System.IO.Path.Combine(Application.StartupPath, "Output/Recap_journalière_du_" + DateTime.Now.ToString("dd-MM-yyyy_HHmmss") + ".pdf");

            float largeurEnCm = 10f;
            float hauteurEnCm = 30f;

            float largeurEnPoints = largeurEnCm * 28.35f; // 1 cm ≈ 28.35 points
            float hauteurEnPoints = hauteurEnCm * 28.35f;

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(largeurEnPoints, hauteurEnPoints);
            Document doc = new Document(pageSize);

            // Définissez les marges du document (en points)
            float marginLeft = 5f; // Marge gauche (en points)
            float marginRight = 5f; // Marge droite (en points)
            float marginTop = 5f; // Marge supérieure (en points)
            float marginBottom = 5f; // Marge inférieure (en points)

            // Définissez les marges du document en utilisant la méthode SetMargins()
            doc.SetMargins(marginLeft, marginRight, marginTop, marginBottom);


            try
            {
                PdfWriter.GetInstance(doc, new FileStream(pdfLocation, FileMode.Create));
                doc.Open();

                Paragraph title = new Paragraph("AMADIA");
                title.Add("\n");
                title.Add("RECETTE JOURNEE DU : " + dateDuJour);
                title.Add("\n");

                doc.Add(title);

                // Créez un tableau avec 5 colonnes
                PdfPTable table = new PdfPTable(6);

                table.WidthPercentage = 98;

                float[] columnWidths = { 26f, 7f, 5f, 9f, 15f, 8f }; // La première colonne a une largeur de 100 points, les autres colonnes auront une largeur automatique
                table.SetWidths(columnWidths);

                PdfPCell cellVideTeteFirst = new PdfPCell(new Phrase(""));
                cellVideTeteFirst.BorderWidthRight = 0f;

                PdfPCell cellAdhesion = new PdfPCell(new Phrase("ADHESION"));
                cellAdhesion.BorderWidthRight = 0f;

                PdfPCell cellCotisation = new PdfPCell(new Phrase("COTISATION"));
                cellCotisation.BorderWidthRight = 0f;

                PdfPCell cellTU = new PdfPCell(new Phrase("T.U"));
                cellTU.BorderWidthRight = 0f;

                PdfPCell cellCarnet = new PdfPCell(new Phrase("CARNET"));
                cellCarnet.BorderWidthRight = 0f;

                PdfPCell cellTG = new PdfPCell(new Phrase("T.G"));
                cellTG.BorderWidthRight = 0f;

                PdfPCell cellPansement = new PdfPCell(new Phrase("PANSEMENT"));
                cellPansement.BorderWidthRight = 0f;

                PdfPCell cellECG = new PdfPCell(new Phrase("ECG"));
                cellECG.BorderWidthRight = 0f;

                PdfPCell cellVideTeteMnM = new PdfPCell(new Phrase(""));
                cellVideTeteMnM.BorderWidthLeft = 0f;

                PdfPCell cellGlycemie = new PdfPCell(new Phrase("GLYCEMIE"));
                cellGlycemie.BorderWidthBottom = 0f; // Enlève la bordure bas de la cellule

                PdfPCell cellGlucometre = new PdfPCell(new Phrase("GLUCOMETRE"));
                cellGlucometre.BorderWidthBottom = 0f;

                PdfPCell cellTensiometre = new PdfPCell(new Phrase("TENSIOMETRE"));
                cellTensiometre.BorderWidthBottom = 0f;

                PdfPCell cellBandelette = new PdfPCell(new Phrase("BANDELETTE"));
                cellBandelette.BorderWidthBottom = 0f;

                PdfPCell cellVideNonMembre = new PdfPCell(new Phrase(""));
                cellVideNonMembre.BorderWidthTop = 0f;

                PdfPCell cellConsultation = new PdfPCell(new Phrase("CONSULTATION"));
                cellConsultation.BorderWidthBottom = 0f; // Enlève la bordure bas de la cellule


                PdfPCell cellHB1AC = new PdfPCell(new Phrase("Hb1Ac"));
                cellHB1AC.BorderWidthBottom = 0f; // Enlève la bordure bas de la cellule


                table.AddCell(cellVideTeteFirst);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("Q");
                table.AddCell("P.U");
                table.AddCell("VALEUR");
                table.AddCell("OBS");

                table.AddCell(cellGlycemie);
                table.AddCell("M");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellVideNonMembre);
                table.AddCell("NM");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");


                table.AddCell(cellConsultation);
                table.AddCell("M");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellVideNonMembre);
                table.AddCell("NM");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellAdhesion);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellCotisation);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellTU);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellTG);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellPansement);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellHB1AC);
                table.AddCell("M");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellVideNonMembre);
                table.AddCell("NM");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                table.AddCell(cellECG);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                doc.Add(table);

                PdfPTable table_1 = new PdfPTable(2);

                table_1.WidthPercentage = 98;

                float[] columnWidths_1 = { 27f, 13f };
                table_1.SetWidths(columnWidths_1);

                table_1.AddCell("TOTAL CONSOMMABLE");
                table_1.AddCell("");

                table_1.SpacingAfter = 10f;

                doc.Add(table_1);

                table.SpacingBefore = 10f;

                //2nd partie Tableau

                PdfPTable table2 = new PdfPTable(6);

                table2.WidthPercentage = 98;

                float[] columnWidths2 = { 26f, 7f, 5f, 9f, 15f, 8f }; // La première colonne a une largeur de 100 points, les autres colonnes auront une largeur automatique
                table2.SetWidths(columnWidths2);

                table2.AddCell(cellGlucometre);
                table2.AddCell("M");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                table2.AddCell(cellVideNonMembre);
                table2.AddCell("NM");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                table2.AddCell(cellTensiometre);
                table2.AddCell("M");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                table2.AddCell(cellVideNonMembre);
                table2.AddCell("NM");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                table2.AddCell(cellBandelette);
                table2.AddCell("M");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                table2.AddCell(cellVideNonMembre);
                table2.AddCell("NM");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                table2.AddCell(cellCarnet);
                table2.AddCell(cellVideTeteMnM);
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");
                table2.AddCell("");

                doc.Add(table2);

                PdfPTable table2_1 = new PdfPTable(2);

                table2_1.WidthPercentage = 98;

                float[] columnWidths2_1 = { 27f, 13f };
                table2_1.SetWidths(columnWidths2_1);

                table2_1.AddCell("TOTAL CONSOMMABLE");
                table2_1.AddCell("");

                table2_1.SpacingAfter = 10f;

                doc.Add(table2_1);


                PdfPTable table3 = new PdfPTable(2);

                table3.WidthPercentage = 98;

                float[] columnWidths3 = { 27f, 13f };
                table3.SetWidths(columnWidths3);

               /* int totalPseudo = 0;
                List<String> idListPseudo = new List<String>();
                idListPseudo = GetMedicIds(cs);
                foreach (String id in idListPseudo)
                {
                    String[] infoDesign = checkListIdMedicMembre(id, cs);

                    // Vérifiez que la longueur de infoDesign est au moins de 5 éléments avant d'accéder à infoDesign[i+1]
                    if (infoDesign.Length >= 6)
                    {
                        int totalInter = 0;
                        for (int i = 1; i < 6; i++)
                        {
                            totalInter = int.Parse(infoDesign[6]);
                        }
                        totalPseudo += totalInter;
                    }
                    else
                    {
                        MessageBox.Show("Erreur de ligne pseudo");
                    }
                }*/

                table3.AddCell("TOTAL MEDICAMENTS");
                //table3.AddCell(FormatteSommeArgent(totalPseudo) + " AR");
                table3.AddCell(" ");
                table3.AddCell(" ");
                table3.AddCell(" ");

                table3.AddCell("TOTAL GENERAL");
                table3.AddCell("");

                table3.AddCell("DEPENSE");
                table3.AddCell("");

                table3.AddCell("VERSEMENT");
                table3.AddCell("");

                doc.Add(table3);

                doc.NewPage();

                Paragraph titlePg2 = new Paragraph("MEDICAMENTS");

                doc.Add(titlePg2);

                PdfPTable tablePg2 = new PdfPTable(6);

                tablePg2.WidthPercentage = 98;

                float[] columnWidthsPg2 = { 26f, 10f, 5f, 9f, 15f, 8f }; // La première colonne a une largeur de 100 points, les autres colonnes auront une largeur automatique
                tablePg2.SetWidths(columnWidthsPg2);

                tablePg2.AddCell("DESIGNATION");
                tablePg2.AddCell("");
                tablePg2.AddCell("Q");
                tablePg2.AddCell("P.U");
                tablePg2.AddCell("VALEUR");
                tablePg2.AddCell("OBS");

                List<String> idList = new List<String>();
                idList = GetMedicIds(cs);

                int totalMembre = 0;
                int totalNonMembre = 0;

                foreach (String id in idList)
                {
                    String[] infoDesign = checkListIdMedicMembre(id, cs);
                    String[] infoDesignNM = checkListIdMedicNonMembre(id, cs);
                    String totalPrixString = totalPrixArticle(id, cs);
                    int totalPrix = int.Parse(totalPrixString);

                    // Vérifiez que la longueur de infoDesign est au moins de 6 éléments avant d'accéder à infoDesign[i]
                    if (infoDesign.Length >= 6)
                    {
                        int totalInterMembre = 0;
                        int totalInterNM = 0;

                        for (int i = 1; i < 6; i++)
                        {
                            tablePg2.AddCell(infoDesign[i]);

                            // Utilisez int.TryParse pour gérer les valeurs potentiellement nulles
                            if (int.TryParse(infoDesign[5], out int parsedValue))
                            {
                                totalInterMembre = parsedValue;
                            }
                            else
                            {
                                totalInterMembre = 0;
                            }
                        }
                        totalMembre += totalInterMembre;
                        
                        tablePg2.AddCell(" ");

                        for (int i = 1; i < 6; i++)
                        {
                            tablePg2.AddCell(infoDesignNM[i]);

                            // Utilisez int.TryParse pour gérer les valeurs potentiellement nulles
                            if (int.TryParse(infoDesignNM[5], out int parsedValue))
                            {
                                totalInterNM = parsedValue;
                            }
                            else
                            {
                                totalInterNM = 0;
                            }
                        }

                        tablePg2.AddCell(" ");
                        totalNonMembre += totalInterNM;
                    }
                    else
                    {
                        MessageBox.Show("Erreur de ligne");
                    }
                }
                int totalPrixRendu = totalMembre + totalNonMembre;

                tablePg2.SpacingBefore = 10f;

                doc.Add(tablePg2);

                PdfPTable tablePg2_1 = new PdfPTable(2);

                tablePg2_1.WidthPercentage = 98;

                float[] columnWidthsPg2_1 = { 23f, 13f };
                tablePg2_1.SetWidths(columnWidthsPg2_1);

                tablePg2_1.AddCell("TOTAL MEDICAMENTS");
                tablePg2_1.AddCell(FormatteSommeArgent(totalPrixRendu) + " AR");

                doc.Add(tablePg2_1);

                doc.Close();
                MessageBox.Show("Generation succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Gérez les erreurs ici
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

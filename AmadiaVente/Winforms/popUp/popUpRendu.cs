using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.Data.Sqlite;
using System.Globalization;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Document = iTextSharp.text.Document;
using Paragraph = iTextSharp.text.Paragraph;

namespace AmadiaVente.Winforms.popUp
{
    public partial class popUpRendu : Form
    {
        //Declaration Globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        private bool isDragging = false;
        private Point lastCursorPos;
        private Point lastFormPos;
        String id = null;

        int total = 0;

        //Constructeur
        public popUpRendu()
        {
            InitializeComponent();

        }
        //Methodes
        private string prixTotalConsommable()
        {
            String result = null;

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery2 = "SELECT  SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.glycemie = 0 AND a.tg = 0 AND a.tu = 0";

                using (SqliteCommand command = new SqliteCommand(sqlQuery2, connection))
                {

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
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

        private List<String> GetHB1ACids(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.designation LIKE 'HB1AC'";

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

        private List<String> GetBandeletteIds(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.designation LIKE 'BANDELETTE%' AND a.glycemie = 0 AND a.tg = 0 AND a.tu = 0";

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

        private List<String> GetTesniometreIds(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.designation LIKE 'TENSIOMETRE%' AND a.glycemie = 0 AND a.tg = 0 AND a.tu = 0";

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

        private List<String> GetGlucometreIds(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.designation LIKE 'GLUCOMETRE%' AND a.glycemie = 0 AND a.tg = 0 AND a.tu = 0";

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

        private List<String> GetOtherConsommableIds(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.designation NOT LIKE 'GLUCOMETRE%' AND a.designation NOT LIKE 'TENSIOMETRE%' AND a.designation NOT LIKE 'BANDELETTE%'  AND a.glycemie = 0 AND a.tg = 0 AND a.tu = 0";

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

        private List<String> GetTU(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.glycemie = 0 AND a.tg = 0 AND a.tu = 1";

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

        private List<String> GetTG(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.glycemie = 0 AND a.tg = 1 AND a.tu = 0";

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

        private List<String> GetGlyc(string connectionString)
        {
            List<string> listId = new List<string>();

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery = "SELECT DISTINCT(lc.id_article), a.type_article FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE a.type_article = 'Equipements' AND a.glycemie = 1 AND a.tg = 0 AND a.tu = 0";

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

        private string[] checkListIdMedic(string id, string connectionString)
        {
            String[] result = new string[6];

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string sqlQuery2 = "SELECT lc.id_article, designation, 'NM' as 'Non Membre', SUM(qte_acheter) AS QTE, prix_article AS PU, SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article INNER JOIN commande c ON c.id_commande = lc.id_commande  WHERE lc.id_article = @id";

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

        private string totalPrixArticle(string cs)
        {
            string result = null; // Initialisez result à une valeur par défaut, par exemple, null

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery2 = "SELECT  SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article WHERE type_article = 'Médicaments'";

                using (SqliteCommand command = new SqliteCommand(sqlQuery2, connection))
                {

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

        //Evenements
        private void btnQuitList_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void popUpRendu_Load(object sender, EventArgs e)
        {

        }

        private void panelPopUpRendu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursorPos = Cursor.Position;
                lastFormPos = this.Location;
            }
        }

        private void panelPopUpRendu_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point delta = new Point(Cursor.Position.X - lastCursorPos.X, Cursor.Position.Y - lastCursorPos.Y);
                this.Location = new Point(lastFormPos.X + delta.X, lastFormPos.Y + delta.Y);
            }
        }

        private void panelPopUpRendu_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }

        private void btnGenererPdf_Click(object sender, EventArgs e)
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
                iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(pdfLocation, FileMode.Create));
                doc.Open();

                iTextSharp.text.Paragraph title = new iTextSharp.text.Paragraph("AMADIA");
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

                PdfPCell cellCarnet = new PdfPCell(new Phrase("CARNET"));
                cellCarnet.BorderWidthRight = 0f;

                PdfPCell cellPansement = new PdfPCell(new Phrase("PANSEMENT"));
                cellPansement.BorderWidthRight = 0f;

                PdfPCell cellECG = new PdfPCell(new Phrase("ECG"));
                cellECG.BorderWidthRight = 0f;

                PdfPCell cellVideTeteMnM = new PdfPCell(new Phrase(""));
                cellVideTeteMnM.BorderWidthLeft = 0f;

                PdfPCell cellGlycemie = new PdfPCell(new Phrase("GLYCEMIE"));
                cellGlycemie.BorderWidthBottom = 0f; // Enlève la bordure bas de la cellule

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

                List<String> idListGlycemie = new List<String>();
                idListGlycemie = GetGlyc(cs);

                foreach (String id in idListGlycemie)
                {
                    String[] infoConsommableMembre = checkListIdMedicMembre(id, cs);
                    String[] infoConsommableNonMembre = checkListIdMedicNonMembre(id, cs);

                    String Designation = "GLYCEMIE";
                    if (string.IsNullOrEmpty(Designation))
                    {
                        Designation = Designation = "GLYCEMIE";
                    }

                    var cellGlucometre = new PdfPCell(new Phrase(Designation));
                    cellGlucometre.Rowspan = 2;

                    table.AddCell(cellGlucometre);
                    table.AddCell("M");
                    table.AddCell(infoConsommableMembre[3]);
                    table.AddCell(infoConsommableMembre[4]);
                    table.AddCell(infoConsommableMembre[5]);
                    table.AddCell("");

                    table.AddCell("NM");
                    table.AddCell(infoConsommableNonMembre[3]);
                    table.AddCell(infoConsommableNonMembre[4]);
                    table.AddCell(infoConsommableNonMembre[5]);
                    table.AddCell("");

                }

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

                List<String> idListTu = new List<String>();
                idListTu = GetTU(cs);

                foreach (String id in idListTu)
                {
                    String[] infoTU = checkListIdMedic(id, cs);

                    String Designation = infoTU[1];

                    var cellTU = new PdfPCell(new Phrase(Designation));
                    cellTU.Colspan = 2;

                    table.AddCell(cellTU);
                    table.AddCell(infoTU[3]);
                    table.AddCell(infoTU[4]);
                    table.AddCell(infoTU[5]);
                    table.AddCell("");

                }

                List<String> idListTg = new List<String>();
                idListTg = GetTG(cs);

                foreach (String id in idListTg)
                {
                    String[] infoTg = checkListIdMedic(id, cs);

                    String Designation = infoTg[1];

                    var cellTG = new PdfPCell(new Phrase(Designation));
                    cellTG.Colspan = 2;

                    table.AddCell(cellTG);
                    table.AddCell(infoTg[3]);
                    table.AddCell(infoTg[4]);
                    table.AddCell(infoTg[5]);
                    table.AddCell("");

                }

                table.AddCell(cellPansement);
                table.AddCell(cellVideTeteMnM);
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");
                table.AddCell("");

                List<String> idListHB1AC = new List<String>();
                idListHB1AC = GetHB1ACids(cs);

                foreach (String id in idListHB1AC)
                {
                    String[] infoConsommableMembre = checkListIdMedicMembre(id, cs);
                    String[] infoConsommableNonMembre = checkListIdMedicNonMembre(id, cs);

                    String Designation = infoConsommableMembre[1];
                    if (string.IsNullOrEmpty(Designation))
                    {
                        Designation = infoConsommableNonMembre[1];
                    }

                    var cellBandelette = new PdfPCell(new Phrase(Designation));
                    cellBandelette.Rowspan = 2;

                    table.AddCell(cellBandelette);
                    table.AddCell("M");
                    table.AddCell(infoConsommableMembre[3]);
                    table.AddCell(infoConsommableMembre[4]);
                    table.AddCell(infoConsommableMembre[5]);
                    table.AddCell("");

                    //table2.AddCell(cellVideNonMembre);
                    table.AddCell("NM");
                    table.AddCell(infoConsommableNonMembre[3]);
                    table.AddCell(infoConsommableNonMembre[4]);
                    table.AddCell(infoConsommableNonMembre[5]);
                    table.AddCell("");
                }

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

                int totalConsomable = 0;

                table_1.AddCell("TOTAL CONSOMMABLE");
                table_1.AddCell("");

                table_1.SpacingAfter = 10f;

                doc.Add(table_1);

                table.SpacingBefore = 10f;

                //2nd partie Tableau

                PdfPTable table2 = new PdfPTable(6);

                table2.WidthPercentage = 98;

                float[] columnWidths2 = { 26f, 7f, 5f, 11f, 14f, 8f }; // La première colonne a une largeur de 100 points, les autres colonnes auront une largeur automatique
                table2.SetWidths(columnWidths2);

                List<String> idListGlucometre = new List<String>();
                idListGlucometre = GetGlucometreIds(cs);

                foreach (String id in idListGlucometre)
                {
                    String[] infoConsommableMembre = checkListIdMedicMembre(id, cs);
                    String[] infoConsommableNonMembre = checkListIdMedicNonMembre(id, cs);

                    String Designation = infoConsommableMembre[1];
                    if (string.IsNullOrEmpty(Designation))
                    {
                        Designation = infoConsommableNonMembre[1];
                    }

                    var cellGlucometre = new PdfPCell(new Phrase(Designation));
                    cellGlucometre.Rowspan = 2;

                    table2.AddCell(cellGlucometre);
                    table2.AddCell("M");
                    table2.AddCell(infoConsommableMembre[3]);
                    table2.AddCell(infoConsommableMembre[4]);
                    table2.AddCell(infoConsommableMembre[5]);
                    table2.AddCell("");

                    table2.AddCell("NM");
                    table2.AddCell(infoConsommableNonMembre[3]);
                    table2.AddCell(infoConsommableNonMembre[4]);
                    table2.AddCell(infoConsommableNonMembre[5]);
                    table2.AddCell("");

                }

                List<String> idListTensiometre = new List<String>();
                idListTensiometre = GetTesniometreIds(cs);

                foreach (String id in idListTensiometre)
                {
                    String[] infoConsommableMembre = checkListIdMedicMembre(id, cs);
                    String[] infoConsommableNonMembre = checkListIdMedicNonMembre(id, cs);

                    String Designation = infoConsommableMembre[1];
                    if (string.IsNullOrEmpty(Designation))
                    {
                        Designation = infoConsommableNonMembre[1];
                    }

                    var cellTensiometre = new PdfPCell(new Phrase(Designation));
                    cellTensiometre.Rowspan = 2;

                    table2.AddCell(cellTensiometre);
                    table2.AddCell("M");
                    table2.AddCell(infoConsommableMembre[3]);
                    table2.AddCell(infoConsommableMembre[4]);
                    table2.AddCell(infoConsommableMembre[5]);
                    table2.AddCell("");

                    table2.AddCell("NM");
                    table2.AddCell(infoConsommableNonMembre[3]);
                    table2.AddCell(infoConsommableNonMembre[4]);
                    table2.AddCell(infoConsommableNonMembre[5]);
                    table2.AddCell("");

                }

                List<String> idListBandelette = new List<String>();
                idListBandelette = GetBandeletteIds(cs);

                foreach (String id in idListBandelette)
                {
                    String[] infoConsommableMembre = checkListIdMedicMembre(id, cs);
                    String[] infoConsommableNonMembre = checkListIdMedicNonMembre(id, cs);

                    String Designation = infoConsommableMembre[1];
                    if (string.IsNullOrEmpty(Designation))
                    {
                        Designation = infoConsommableNonMembre[1];
                    }

                    var cellBandelette = new PdfPCell(new Phrase(Designation));
                    cellBandelette.Rowspan = 2;

                    table2.AddCell(cellBandelette);
                    table2.AddCell("M");
                    table2.AddCell(infoConsommableMembre[3]);
                    table2.AddCell(infoConsommableMembre[4]);
                    table2.AddCell(infoConsommableMembre[5]);
                    table2.AddCell("");

                    //table2.AddCell(cellVideNonMembre);
                    table2.AddCell("NM");
                    table2.AddCell(infoConsommableNonMembre[3]);
                    table2.AddCell(infoConsommableNonMembre[4]);
                    table2.AddCell(infoConsommableNonMembre[5]);
                    table2.AddCell("");
                }

                List<String> idListConsommable = new List<String>();
                idListConsommable = GetOtherConsommableIds(cs);

                foreach (String id in idListConsommable)
                {
                    String[] infoConsommable = checkListIdMedic(id, cs);

                    String Designation = infoConsommable[1];

                    var cellConsomable = new PdfPCell(new Phrase(Designation));
                    cellConsomable.Colspan = 2;

                    table2.AddCell(cellConsomable);
                    table2.AddCell(infoConsommable[3]);
                    table2.AddCell(infoConsommable[4]);
                    table2.AddCell(infoConsommable[5]);
                    table2.AddCell("");

                }

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

                int totalConsomable2 = 0;

                string prixConsomable = prixTotalConsommable();
                int totalConsommable = Convert.ToInt32(prixConsomable);

                table2_1.AddCell("TOTAL CONSOMMABLE");
                table2_1.AddCell(FormatteSommeArgent(totalConsommable) + " AR");

                table2_1.SpacingAfter = 10f;

                doc.Add(table2_1);


                PdfPTable table3 = new PdfPTable(2);

                table3.WidthPercentage = 98;

                float[] columnWidths3 = { 27f, 13f };
                table3.SetWidths(columnWidths3);

                String totalMedGen = totalPrixArticle(cs);
                int totalMedIntGen = Convert.ToInt32(totalMedGen);

                table3.AddCell("TOTAL MEDICAMENTS");
                table3.AddCell(FormatteSommeArgent(totalMedIntGen) + " AR");
                table3.AddCell(" ");
                table3.AddCell(" ");

                int totalGen = totalConsommable + totalConsomable2 + totalMedIntGen;

                table3.AddCell("TOTAL GENERAL");
                table3.AddCell(FormatteSommeArgent(totalGen) + " AR");

                int depense = 0; //à definir

                table3.AddCell("DEPENSE");
                table3.AddCell("");

                int versement = totalGen - depense;

                table3.AddCell("VERSEMENT");
                table3.AddCell(FormatteSommeArgent(versement) + " AR");

                doc.Add(table3);

                doc.NewPage();

                Paragraph titlePg2 = new Paragraph("MEDICAMENTS");

                doc.Add(titlePg2);

                PdfPTable tablePg2 = new PdfPTable(6);

                tablePg2.WidthPercentage = 98;

                float[] columnWidthsPg2 = { 26f, 10f, 5f, 9f, 15f, 8f };
                tablePg2.SetWidths(columnWidthsPg2);

                tablePg2.AddCell("DESIGNATION");
                tablePg2.AddCell("");
                tablePg2.AddCell("Q");
                tablePg2.AddCell("P.U");
                tablePg2.AddCell("VALEUR");
                tablePg2.AddCell("OBS");

                List<String> idList = new List<String>();
                idList = GetMedicIds(cs);

                foreach (String id in idList)
                {
                    String[] infoDesign = checkListIdMedicMembre(id, cs);
                    String[] infoDesignNM = checkListIdMedicNonMembre(id, cs);

                    String Designation = infoDesign[1];
                    if (string.IsNullOrEmpty(Designation))
                    {
                        Designation = infoDesignNM[1];
                    }

                    var cellMedic = new PdfPCell(new Phrase(Designation));
                    cellMedic.Rowspan = 2;

                    tablePg2.AddCell(cellMedic);
                    tablePg2.AddCell("M");
                    tablePg2.AddCell(infoDesign[3]);
                    tablePg2.AddCell(infoDesign[4]);
                    tablePg2.AddCell(infoDesign[5]);
                    tablePg2.AddCell("");

                    //table2.AddCell(cellVideNonMembre);
                    tablePg2.AddCell("NM");
                    tablePg2.AddCell(infoDesignNM[3]);
                    tablePg2.AddCell(infoDesignNM[4]);
                    tablePg2.AddCell(infoDesignNM[5]);
                    tablePg2.AddCell("");
                }

                tablePg2.SpacingBefore = 10f;

                doc.Add(tablePg2);

                PdfPTable tablePg2_1 = new PdfPTable(2);

                tablePg2_1.WidthPercentage = 98;

                float[] columnWidthsPg2_1 = { 23f, 13f };
                tablePg2_1.SetWidths(columnWidthsPg2_1);

                String totalMed = totalPrixArticle(cs);
                int totalMedInt = Convert.ToInt32(totalMed);

                tablePg2_1.AddCell("TOTAL MEDICAMENTS");
                tablePg2_1.AddCell(FormatteSommeArgent(totalMedInt) + " AR");

                doc.Add(tablePg2_1);

                doc.Close();
                MessageBox.Show("Generation succes", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuivant_Click(object sender, EventArgs e)
        {

        }

        private void btnRetour_Click(object sender, EventArgs e)
        {

        }
    }
}

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
        private string prixTotalMedic()
        {
            String result = null;

            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery2 = " SELECT SUM(prix) AS VALEUR FROM ligneCommande lc INNER JOIN article a ON a.id_article = lc.id_article";

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

                int totalConsomable = 0;

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

                int totalConsomable2 = 0;

                table2_1.AddCell("TOTAL CONSOMMABLE");
                table2_1.AddCell("");

                table2_1.SpacingAfter = 10f;

                doc.Add(table2_1);


                PdfPTable table3 = new PdfPTable(2);

                table3.WidthPercentage = 98;

                float[] columnWidths3 = { 27f, 13f };
                table3.SetWidths(columnWidths3);
                String prixMed = prixTotalMedic();
                int totalPrixMedic = int.Parse(prixMed);

                table3.AddCell("TOTAL MEDICAMENTS");
                table3.AddCell(FormatteSommeArgent(totalPrixMedic) + " AR");
                table3.AddCell(" ");
                table3.AddCell(" ");

                int totalGen = totalConsomable + totalConsomable2 + totalPrixMedic;

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

                int totalMembre = 0;
                int totalNonMembre = 0;

                foreach (String id in idList)
                {
                    String[] infoDesign = checkListIdMedicMembre(id, cs);
                    String[] infoDesignNM = checkListIdMedicNonMembre(id, cs);
                    String totalPrixString = totalPrixArticle(id, cs);
                    int totalPrix = int.Parse(totalPrixString);

                    if (infoDesign.Length >= 6)
                    {
                        int totalInterMembre = 0;
                        int totalInterNM = 0;

                        for (int i = 1; i < 6; i++)
                        {
                            tablePg2.AddCell(infoDesign[i]);

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

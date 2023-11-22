using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using static iTextSharp.text.pdf.PdfDocument;

namespace AmadiaVente.Winforms.functionality
{
    public partial class listeStock : Form
    {
        //Declaration Globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "sysCall.dll");

        //COnstructeur
        public listeStock()
        {
            InitializeComponent();
        }
        //Methodes 
        public void afficheListe()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT id_article AS Id, designation AS Désignation, prix_article AS Prix, prix_membre AS 'Prix (Membre)', type_article AS Type, nbr_stock AS 'Qte en Stock' FROM article";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewList.DataSource = dataTable;
                    }
                }
            }
        }

        private void removeList(string id)
        {
            try
            {
                using (SqliteConnection connection = new SqliteConnection(cs))
                {
                    connection.Open();
                    string query = "DELETE from article WHERE id_article = @id";

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

        //Evenements
        private void listeStock_Load(object sender, EventArgs e)
        {
            afficheListe();
        }

        private void btnAddNewList_Click(object sender, EventArgs e)
        {
            popUp.popUpAddList popup = new popUp.popUpAddList();

            popup.ShowDialog();
            popup.Dispose();
            afficheListe();
        }

        private void btnRemoveList_Click(object sender, EventArgs e)
        {
            if (dataGridViewList.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Confirmez-vous la suppression de cet article ?\nCette action est irréversible !", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {

                    DataGridViewRow selectedRow = dataGridViewList.SelectedRows[0];
                    String valeurCellule = selectedRow.Cells[0].Value.ToString();
                    removeList(valeurCellule);
                    afficheListe();
                }
            }
            else
            {
                MessageBox.Show("Veuillez Selectionner un article à Supprimer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
    }
}

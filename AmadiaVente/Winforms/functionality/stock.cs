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

namespace AmadiaVente.Winforms.functionality
{
    public partial class stock : Form
    {
        //declaration global
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");

        //Constructeur
        public stock()
        {
            InitializeComponent();
        }

        //Methodes
        public void afficheHistory()
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT a.designation AS Désignation, mouvement as Mouvement, quantite AS Quantité, 'Client' AS 'Source Commande', id_source AS Numero, strftime('%d/%m/%Y %H:%M', date) AS date FROM stockHistorySortie shs INNER JOIN article a ON a.id_article = shs.id_article UNION ALL SELECT a.designation AS Désignation, mouvement AS Mouvement, quantite AS Quantité, 'Fournisseur' AS 'Source Commande', id_source AS Numero, strftime('%d/%m/%Y %H:%M', date) AS date FROM stockHistoryEntrer she INNER JOIN article a ON a.id_article = she.id_article ORDER BY date DESC";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewStock.DataSource = dataTable;
                    }
                }
            }

            dataGridViewStock.CellFormatting += (sender, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewStock.Columns["mouvement"].Index)
                {
                    string sourceValue = dataGridViewStock.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    // Colorier les cellules différemment en fonction de la valeur de la colonne "Source"
                    if (sourceValue == "Sortie")
                    {
                        e.CellStyle.BackColor = Color.LightBlue;
                    }
                    else if (sourceValue == "Entrer")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                    }
                }
            };
        }





        //Evenements

        private void stock_Load(object sender, EventArgs e)
        {
            afficheHistory();
        }
    }
}

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

                string sqlQuery = "SELECT a.designation, mouvement, quantite, 'Commande' AS Source, id_source AS Numero, strftime('%d/%m/%Y %H:%M', date) AS date FROM stockHistorySortie shs INNER JOIN article a ON a.id_article = shs.id_article UNION ALL SELECT a.designation, mouvement, quantite, 'Fournisseur' AS Source, id_source AS Numero, strftime('%d/%m/%Y %H:%M', date) AS date FROM stockHistoryEntrer she INNER JOIN article a ON a.id_article = she.id_article ORDER BY date DESC";

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
        }

        //Evenements
        private void stock_Load(object sender, EventArgs e)
        {
            afficheHistory();
        }
    }
}

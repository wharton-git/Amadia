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
    public partial class listeStock : Form
    {
        //Declaration Globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");

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

                string sqlQuery = "SELECT id_article AS Id, designation AS Désignation, prix_article AS Prix, type_article AS Type, nbr_stock AS 'Qte en Stock' FROM article";

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

        //Evenements
        private void listeStock_Load(object sender, EventArgs e)
        {
            afficheListe();
        }

    }
}

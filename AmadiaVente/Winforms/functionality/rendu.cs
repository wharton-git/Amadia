using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Data.SQLite;

namespace AmadiaVente.Winforms.functionality
{
    public partial class rendu : Form
    {
        // Déclaration globale
        private string cs = "Data Source=" + System.IO.Path.Combine(Application.StartupPath, "../../../database.db");

        // Constructeur
        public rendu()
        {
            InitializeComponent();
        }

        //Méthodes
        void afficherRecetteDuJour(DateTime date)
        {
            using (SqliteConnection connection = new SqliteConnection(cs))
            {
                connection.Open();

                string sqlQuery = "SELECT COALESCE(nom_membre, 'Non'),COALESCE(prenom_membre, 'Membre'), nom_user, prenom_user, c.date_achat FROM commande c LEFT JOIN membre m ON m.id_membre = c.id_membre INNER JOIN user u ON u.id_user = c.id_responsable WHERE date_achat > @dateNow";

                using (SqliteCommand command = new SqliteCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@dateNow", date);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);

                        dataGridViewDashboard.DataSource = dataTable;
                    }
                }
            }
        }

        // Événements

        private void rendu_Load(object sender, EventArgs e)
        {
            DateTime dateActuelle = DateTime.Today;
            afficherRecetteDuJour(dateActuelle);
        }
    }
}
